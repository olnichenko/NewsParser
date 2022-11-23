using GenericRepository;
using Microsoft.EntityFrameworkCore;
using NewsParserDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsParserDAL.Repositories
{
    public class ArticlesRepository : BaseRepository<Article, long>
    {
        public ArticlesRepository(DbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Article> CreateOrUpdateArticleAsync(Category category, Article article)
        {
            var cat = await _dbContext.Set<Category>().FindAsync(category.Id);
            article.Category = cat;
            if (article.Id > 0)
            {
                _dbContext.Entry(article).State = EntityState.Modified;
            }
            else
            {
                _dbContext.Add(article);
            }
            await _dbContext.SaveChangesAsync();
            return article;
        }
    }
}
