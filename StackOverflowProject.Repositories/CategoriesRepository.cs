using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface ICategoriesRepository
    {
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryID);
        List<Category> GetCategories();
        List<Category> GetCategoryByCategoryID(int categoryID);
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        StackOverflowDatabaseDbContext db;

        public CategoriesRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void DeleteCategory(int categoryID)
        {
            var category = db.Categories.Where(temp => temp.CategoryID == categoryID).FirstOrDefault();
            if(category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        public List<Category> GetCategoryByCategoryID(int categoryID)
        {
            return db.Categories.Where(temp => temp.CategoryID == categoryID).ToList();
        }

        public void InsertCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            Category ct = db.Categories.Where(temp => temp.CategoryID == category.CategoryID).FirstOrDefault();
            if(ct != null)
            {
                ct.CategoryName = category.CategoryName;
                db.SaveChanges();
            }
        }
    }
}
