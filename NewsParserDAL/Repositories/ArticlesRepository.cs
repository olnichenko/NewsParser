using GenericRepository;
using Microsoft.EntityFrameworkCore;
using NewsParserDAL.Models;
using System;
using System.Collections.Generic;
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
    }
}
