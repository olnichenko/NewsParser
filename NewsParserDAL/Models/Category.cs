using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NewsParserDAL.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : BaseEntityAbstract<int>
    {
        public string Name { get; set; }
        public List<Article>? Articles { get; set; } = new();
    }
}
