using Microsoft.EntityFrameworkCore;
using NewsParserDAL.Models;
using NewsParserDAL.Repositories;

namespace NewsParser.Services
{
    public class ArticleService
    {
        private readonly ArticlesRepository _articlesRepository;
        private readonly CategoriesRepository _categoriesRepository;
        public ArticleService(ArticlesRepository articlesRepository, CategoriesRepository categoriesRepository) {
            _articlesRepository = articlesRepository;
            _categoriesRepository = categoriesRepository;
        }
        public async Task<Category> CreateIfNotExistAndGetCategoryByNameAsync(string name)
        {
            var category = _categoriesRepository.Get(x => x.Name == name).FirstOrDefault();
            if (category == null)
            {
                category = new Category { Name = name };
                category = await _categoriesRepository.Create(category);
            }
            return category;
        }
        public bool IsExist(string title)
        {
            var article = _articlesRepository.Get(x => x.Title == title).FirstOrDefault();
            return article != null;
        }
        public async Task<IEnumerable<Article>> GetArticlesAfterDateSavedAsync(DateTime dateSaved)
        {
            var article = await _articlesRepository.GetAll().Where(x => x.DateSaved >= dateSaved).ToListAsync();
            return article;
        }
        public async Task<Article> UpdateOrCreateArticleAsync(Article article, Category category)
        {
            return await _articlesRepository.CreateOrUpdateArticleAsync(category, article);
        }
    }
}
