using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_Core_API.Data.Entities;
using NET_Core_API.Data.Repositories.Repos_Contracts;
using System;




namespace NET_Core_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]

        public ActionResult Get()
        {
            try
            {
                var products = _productRepo.GetAll();

                return Ok(products);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database failure");
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var product = _productRepo.Get(id);

                return Ok(product);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database failure");
            }
        }


        [HttpPost]
        public ActionResult Post(Product product)
        {
            try
            {
                var existing = _productRepo.Get(product.ID);

                if (existing != null)
                    return BadRequest("Product in Use");

                _productRepo.Save(product);

                return Created($"api/Product/{product.ID}", product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database failure");
            }


        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Product product)
        {
            try
            {
                _productRepo.Save(product);

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database failure");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var productFound = _productRepo.Get(id);

                if (productFound == null)
                    return NotFound("couldn't find resource");

                var result = _productRepo.Delete(productFound);
                if (result > 0)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode
                        (StatusCodes.Status500InternalServerError,
                        "Database Failure");
                }
            }
            catch (Exception)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    "Database failure");
            }
        }
    }
}
