using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) 
        {
            this._context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
        public Category? GetById(int id)
        {
            return _context.Categories.FirstOrDefault(p => p.Id == id);
        }

        public Category? GetCategoryWithProducts(int id)
        {
            return _context.Categories.Include(c=>c.Products).FirstOrDefault(p => p.Id == id);
        }

        public void Create(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }
        public void Update(int id, Category entity)
        {
            Category? category = GetById(id);
            category.Name = entity.Name;
    
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Category category = GetById(id);

            _context.Remove(category);
            _context.SaveChanges();

        }
    }
}
