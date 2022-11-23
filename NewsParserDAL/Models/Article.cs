using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsParserDAL.Models
{
    public class Article : BaseEntityAbstract<long>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime DateSaved {get;set;}
        public string DatePublished { get; set; }
        public bool IsTelegramSended { get; set; }
    }
}
