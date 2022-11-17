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
    public class CategoriesRepository : BaseRepository<Category, int>
    {
        public CategoriesRepository(DbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
