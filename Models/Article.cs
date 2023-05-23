using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace logTesting.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}