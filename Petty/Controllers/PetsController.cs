using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Petty.Data;
using Petty.DTO;
using Petty.Entities;
using Petty.Extensions;
using Petty.Helpers;
using Petty.Interfaces;

namespace Petty.Controllers;

public class PetsController : BaseController
{
    private readonly IUserRepository _userRepository;
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public PetsController(IUserRepository userRepository, IPetRepository petRepository, IMapper mapper,
        IPhotoService photoService)
    {
        _userRepository = userRepository;
        _petRepository = petRepository;
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<PetDto>>> GetPets([FromQuery] PetParams petParams)
    {
        var currentUser = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        if (string.IsNullOrEmpty(petParams.Animal) && currentUser.LookingFor != null)
        {
            petParams.Animal = currentUser.LookingFor;
        }

        var pets = await _petRepository.GetPetsAsync(petParams);
        Response.AddPaginationHeader(new PaginationHeader(pets.CurrentPage, pets.PageSize, pets.TotalCount,
            pets.TotalPages));
        return Ok(pets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PetDto>> GetPetById(int id)
    {
        return await _petRepository.GetPetByIdAsync(id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePet(int id, PetUpdateDto petUpdateDto)
    {
        var pet = await _petRepository.GetPetByIdAsync(id);

        if (pet == null) return NotFound();

        _mapper.Map(petUpdateDto, pet);
        if (await _petRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update pet");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(UploadPhotoPetDto uploadPhotoPetDto)
    {
        var pet = await _petRepository.GetPetByIdAsync(uploadPhotoPetDto.Id);

        if (pet == null) return NotFound();

        var result = await _photoService.AddPhotoAsync(uploadPhotoPetDto.File);

        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        if (pet.Photos.Count == 0) photo.IsMain = true;

        pet.Photos.Add(photo);

        if (await _petRepository.SaveAllAsync())
        {
            return CreatedAtAction(nameof(GetPetById), new {id = uploadPhotoPetDto.Id},
                _mapper.Map<PhotoDto>(photo));
        }

        return BadRequest("Problem adding photo");
    }

    [HttpDelete("delete-photo/{id}/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int id, int photoId)
    {
        var pet = await _petRepository.GetPetByIdAsync(id);

        var photo = pet.Photos.FirstOrDefault(x => x.Id == photoId);
        if (photo == null) return NotFound();
        if (photo.IsMain) return BadRequest("Cannot delete main photo");
        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);
        }

        pet.Photos.Remove(photo);
        if (await _petRepository.SaveAllAsync()) return Ok();

        return BadRequest("Problem deleting photo");
    }
}