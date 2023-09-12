using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petty.Entities;

namespace Petty.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
        if (users == null) return;

        var roles = new List<AppRole>
        {
            new AppRole {Name = "Member"},
            new AppRole {Name = "Admin"},
            new AppRole {Name = "Moderator"}
        };
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        foreach (var user in users)
        {
            user.UserName = user.UserName.ToLower();
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");
        }

        var admin = new AppUser
        {
            UserName = "admin"
        };

        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});
    }

    public static async Task InitializeAnimals(DataContext context)
    {
        if (context.Animals.Any()) return;

        var animals = new List<Animal>
        {
            new Animal
            {
                AnimalName = "Cat"
            },
            new Animal
            {
                AnimalName = "Dog"
            },
            new Animal
            {
                AnimalName = "Parrot"
            }
        };
        foreach (var animal in animals)
        {
            context.Animals.Add(animal);
        }

        await context.SaveChangesAsync();
    }

    public static async Task InitializePets(DataContext context)
    {
        if (context.Pets.Any()) return;
        var pets = new List<Pet>
        {
            new Pet
            {
                Animal = "Cat",
                Breed = "Siames",
                Age = 1,
                IsPermanentCare = false
            },
            new Pet
            {
                Animal = "Cat",
                Breed = "Maine Coon",
                Age = 3,
                IsPermanentCare = false
            },
            new Pet
            {
                Animal = "Cat",
                Breed = "Ragdoll",
                Age = 7,
                IsPermanentCare = false
            },
            new Pet
            {
                Animal = "Cat",
                Breed = "Perian",
                Age = 5,
                IsPermanentCare = true
            },
            new Pet
            {
                Animal = "Dog",
                Breed = "Beagle",
                Age = 6,
                IsPermanentCare = false
            },
            new Pet
            {
                Animal = "Dog",
                Breed = "Labrador",
                Age = 2,
                IsPermanentCare = true
            },
            new Pet
            {
                Animal = "Dog",
                Breed = "German Shepherd",
                Age = 9,
                IsPermanentCare = false
            },
        };
        foreach (var pet in pets)
        {
            context.Pets.Add(pet);
        }

        await context.SaveChangesAsync();
    }
}