using Microsoft.EntityFrameworkCore;
using NewsParser.Models;
using System.Runtime;
using NewsParserDAL;

namespace NewsParser
{
    public static class Startup
    {
        private static DbSettingsModel _dbSettings;
        public static void MapSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSettingsModel>(configuration.GetSection(DbSettingsModel.SectionName));

            _dbSettings = configuration.GetSection(DbSettingsModel.SectionName).Get<DbSettingsModel>();
            services.AddScoped((_) => new NewsParserDbContext(_dbSettings.ConnectionString));
        }
        public static void MapRepositories(this IServiceCollection services)
        {
            //services.AddScoped((_) => new BlogsRepository(new BpDbContext(_dbSettings.ConnectionString)));
        }
    }
}
