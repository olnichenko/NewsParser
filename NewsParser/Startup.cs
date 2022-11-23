using Microsoft.EntityFrameworkCore;
using NewsParser.Models;
using System.Runtime;
using NewsParserDAL;
using NewsParserDAL.Repositories;
using NewsParser.Services;

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
            // services.AddScoped((_) => new ArticlesRepository(new NewsParserDbContext(_dbSettings.ConnectionString)));
            // services.AddScoped((_) => new CategoriesRepository(new NewsParserDbContext(_dbSettings.ConnectionString)));
            var context = new NewsParserDbContext(_dbSettings.ConnectionString);
            var articlesRepo = new ArticlesRepository(new NewsParserDbContext(_dbSettings.ConnectionString));
            var categoriesRepo = new CategoriesRepository(new NewsParserDbContext(_dbSettings.ConnectionString));

            services.AddScoped((_) => new TelegramService());
            services.AddScoped((_) => new ArticleService(articlesRepo, categoriesRepo));
        }
    }
}
