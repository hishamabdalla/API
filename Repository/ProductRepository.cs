using API.Data;
using API.Models;

namespace API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public Product? GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }


        public void Create(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }
        public void Update(int id, Product entity)
        {
            Product? oldProduct = GetById(id);
            oldProduct.Name = entity.Name;
            oldProduct.Description = entity.Description;
            oldProduct.Price = entity.Price;
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Product product = GetById(id);

            _context.Products.Remove(product);
            _context.SaveChanges();

        }
    }
}