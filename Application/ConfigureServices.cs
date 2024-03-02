using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
	public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
	{
		services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		return services;
	}
}