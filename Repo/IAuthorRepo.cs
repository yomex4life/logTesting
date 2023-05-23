using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logTesting.Models;

namespace logTesting.Repo
{
    public interface IAuthorRepo
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthor(int id);
        bool AddAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task<Author> DeleteAuthor(int id);
        Task<IEnumerable<Article>> GetArticlesByAuthor(int id);
        bool AuthorExists(int id);
        bool SaveChanges();
    }
}