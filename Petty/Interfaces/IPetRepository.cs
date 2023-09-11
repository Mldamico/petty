using Petty.DTO;
using Petty.Entities;
using Petty.Helpers;

namespace Petty.Interfaces;

public interface IPetRepository
{
    void Update(Pet pet);
    Task<bool> SaveAllAsync();
    Task<PetDto> GetPetByIdAsync(int id);
    Task<PagedList<PetDto>> GetPetsAsync(PetParams petParams);
 
}