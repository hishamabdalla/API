using API.DTO;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository category)
        {
            this._categoryRepo = category;
        }

        [HttpGet("products/{id}")]
        public IActionResult GetCategoryWithProducts(int id)
        {
            Category category = _categoryRepo.GetCategoryWithProducts(id);
            CategoryWithProducts categoryWithProducts = new CategoryWithProducts();
            categoryWithProducts.Id = category.Id;
            categoryWithProducts.Name = category.Name;
            foreach (var item in category.Products)
            {
                categoryWithProducts.Products.Add(new ProductDTO() { Name = item.Name, Price = item.Price });
            }
            return Ok(categoryWithProducts);
        }
            
    }
}    

    

