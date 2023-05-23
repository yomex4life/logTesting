using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logTesting.Data;
using logTesting.Models;
using Microsoft.EntityFrameworkCore;

namespace logTesting.Repo
{
    public class ArticleRepo : IArticleRepo
    {
        private readonly AppDbContext _context;

        public ArticleRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool AddArticle(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            _context.Articles.Add(article);
            return SaveChanges();
        }

        public bool ArticleExists(int id)
        {
            return _context.Articles.Any(a => a.Id == id);
        }

        public Task<Article> DeleteArticle(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> GetArticle(int id)
        {
            return await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        public Task<IEnumerable<Article>> GetArticlesByAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Task<Article> UpdateArticle(Article article)
        {
            throw new NotImplementedException();
        }
    }
}