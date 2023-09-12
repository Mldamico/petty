using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Petty.DTO;
using Petty.Entities;
using Petty.Helpers;
using Petty.Interfaces;

namespace Petty.Data;

public class PetRepository : IPetRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PetRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Update(Pet pet)
    {
        _context.Entry(pet).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<PetDto> GetPetByIdAsync(int id)
    {
        var pet = await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);
        return _mapper.Map<PetDto>(pet);
    }

    public async Task<PagedList<PetDto>> GetPetsAsync(PetParams petParams)
    {
        var query = _context.Pets.AsQueryable();

        query = query.Where(pet => pet.Animal == petParams.Animal);
        query = query.Where(pet => pet.Breed == petParams.Breed);
        query = query.Where(pet => pet.IsPermanentCare == petParams.IsPermanentCare);

        return await PagedList<PetDto>.CreateAsync(
            query.AsNoTracking().ProjectTo<PetDto>(_mapper.ConfigurationProvider), petParams.PageNumber,
            petParams.PageSize);
    }

    
}