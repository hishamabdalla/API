using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            this._productRepo = productRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _productRepo.GetAll().ToList();
            return Ok(products);
        }

        [HttpGet("{id}",Name ="ProductRouteName")]
        public IActionResult GetById(int id)
        {
            Product product = _productRepo.GetById(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Product product)
        {
            if(ModelState.IsValid)
            {
                _productRepo.Create(product);
                //string url = Url.Link("ProductRouteName", new { Id = product.Id });
                //return Created(url, product);
                return CreatedAtAction(nameof(GetById), new { Id = product.Id }, product); 
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Product newProduct)
        {
            if (ModelState.IsValid)
            {
                if (_productRepo.GetById(id) is null)
                {
                    return BadRequest();
                }

                _productRepo.Update(id, newProduct);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _productRepo.Delete(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}

