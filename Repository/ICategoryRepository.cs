using API.Models;

namespace API.Repository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        public Category? GetCategoryWithProducts(int id);
    }
}
