using Petty.Entities;

namespace Petty.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}