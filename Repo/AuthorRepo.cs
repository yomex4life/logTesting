using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logTesting.Data;
using logTesting.Models;
using Microsoft.EntityFrameworkCore;

namespace logTesting.Repo
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly AppDbContext _context;

        public AuthorRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Add(author);
            return SaveChanges();
        }

        public bool AuthorExists(int id)
        {
            return _context.Authors.Any(a => a.Id == id);
        }

        public Task<Author> DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Article>> GetArticlesByAuthor(int id)
        {
            return await _context.Articles.Where(a => a.AuthorId == id).ToListAsync();
        }

        public async Task<Author> GetAuthor(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Task<Author> UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }
    }
}