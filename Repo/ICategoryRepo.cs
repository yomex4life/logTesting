using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logTesting.Models;

namespace logTesting.Repo
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        bool AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(int id);
        bool CategoryExists(int id);

        bool CategoryExists(string name);
        bool SaveChanges();
        Task<IEnumerable<Article>> GetArticlesByCategory(int id);
    }
}