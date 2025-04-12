using Microsoft.AspNetCore.Mvc;
using PP_BC_C_H_2.Attributes;
using PP_BC_C_H_2.Entity;
using PP_BC_C_H_2.Services;

namespace PP_BC_C_H_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("list")]
        [SessionAuthentication]
        public IActionResult List(string name = null, string sortBy = null, string sortOrder = "asc")
        {

            var products = _productService.List(name, sortBy, sortOrder);
            return Ok(new { status = 200, data = products });
        }

        [HttpGet("{id}")]
        [SessionAuthentication]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound(new { status = 404, error = "Product not found" });

            return Ok(new { status = 200, data = product });
        }

        [HttpPost]
        [SessionAuthentication]
        public IActionResult Create([FromBody] Product product)
        {
            try
            {
                var createdProduct = _productService.Create(product);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, new { status = 201, data = createdProduct });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { status = 400, error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [SessionAuthentication]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            try
            {
                var updatedProduct = _productService.Update(id, product);
                return Ok(new { status = 200, data = updatedProduct });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { status = 404, error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { status = 400, error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SessionAuthentication]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { status = 404, error = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        [SessionAuthentication]
        public IActionResult Patch(int id, [FromBody] Product product)
        {
            try
            {
                var patchedProduct = _productService.Patch(id, product);
                return Ok(new { status = 200, data = patchedProduct });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { status = 404, error = ex.Message });
            }
        }
    }
}
