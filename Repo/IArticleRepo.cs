using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logTesting.Models;

namespace logTesting.Repo
{
    public interface IArticleRepo
    {
        Task<IEnumerable<Article>> GetArticles();
        Task<Article> GetArticle(int id);
        bool AddArticle(Article article);
        Task<Article> UpdateArticle(Article article);
        Task<Article> DeleteArticle(int id);
        bool ArticleExists(int id);
        bool SaveChanges();
        Task<IEnumerable<Article>> GetArticlesByAuthor(int id);
    }
}