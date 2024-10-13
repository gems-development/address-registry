using Microsoft.Extensions.DependencyInjection;
using Gems.ApplicationServices.Services;

namespace Gems.ApplicationServices.DependencyInjection
{
	public static class ApplicationServicesServiceCollectionExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
			services
				.AddScoped<DataImportService>();
	}
}
