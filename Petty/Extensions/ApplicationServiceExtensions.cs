using Microsoft.EntityFrameworkCore;
using Petty.Data;
using Petty.Helpers;
using Petty.Interfaces;
using Petty.Services;

namespace Petty.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<LogUserActivity>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddSignalR();
        services.AddDbContext<DataContext>(opt =>
            opt.UseSqlite(config.GetConnectionString("DefaultConnection")));
        services.AddCors();
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        return services;
    }
}