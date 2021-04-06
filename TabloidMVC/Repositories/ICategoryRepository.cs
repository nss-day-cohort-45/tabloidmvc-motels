using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        void AddCat(Category category);
        void DeleteCat(int categoryId);
        Category GetCatById(int id);
        void UpdateCat(Category category);

    }
}