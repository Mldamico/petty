using Microsoft.AspNetCore.Identity;
using Petty.Extensions;

namespace Petty.Entities;

public class AppUser : IdentityUser<int>
{
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string LookingFor { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public List<Photo> Photos { get; set; } = new List<Photo>();

    public ICollection<AppUserRole> UserRoles { get; set; }

    public int GetAge()
    {
        return DateOfBirth.CalculateAge();
    }
}