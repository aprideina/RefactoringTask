using RefactorReportService.Configuration;

namespace RefactorReportService
{
	public static class ServiceCollectionExtensions
	{
		public static void ConfigureDatabase(this IServiceCollection services, IConfigurationManager configuration)
		{
			services.Configure<DatabaseConfiguration>(configuration.GetSection(DatabaseConfiguration.ConfigSectionName));
		}
	}
}
