using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NewsParserDAL.Models
{
    public class Category : BaseEntityAbstract<int>
    {
        public string Name { get; set; }
        public List<Article> Articles { get; set; }
    }
}
