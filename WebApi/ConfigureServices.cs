using System.Reflection;
using Application;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;

namespace WebApi;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
    {
		services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {       
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<IAppDbContext, AppDbContext>(options => 
        {
            options.UseNpgsql(connectionString);
        });
        
        return services;
    }
    
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
	    services.AddScoped<DbSeeder>();
	    
        services.AddIdentity<User, Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddEntityFrameworkStores<AppDbContext>();
        
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        });
        return services;
    }
    
    public static IServiceCollection ConfigureCors(this IServiceCollection services, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            services.AddCors(options =>
            {
                options.AddPolicy("cors", builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
                });
            }); 
        }
        else
        {
            services.AddCors(options =>
            {
                options.AddPolicy("cors", builder =>
                {
                    builder.WithOrigins("").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }
          
        return services;
    }
    
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MyDevHub API",
                Version = "v1",
            });
        });

        return services;
    }
}