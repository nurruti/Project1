using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Urrutia_Project1.Models;
using Microsoft.Extensions.Logging;

namespace Urrutia_Project1.Controllers
{
    public class ProductDetailsController : Controller
    {
        ProductDetailsModel model = new ProductDetailsModel();

        private readonly ILogger<ProductDetailsController> _logger;

        public ProductDetailsController(ILogger<ProductDetailsController> logger)
        {
            _logger = logger;
        }

        //CRUD
        #region POST
        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct(ProductDetailsModel newProduct)
        {
            try
            {
                return Created("", model.AddProduct(newProduct));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        #endregion

        #region GET List
        [HttpGet]
        [Route("pList")]
        public IActionResult Productlist()
        {
            return Ok(model.GetProductList());
        }
        #endregion

        #region GET by ID
        [HttpGet]
        [Route("ProductDetails")]
        public IActionResult GetProductById(int pId)
        {
            try
            {
                return Ok(model.GetProductDetails(pId));
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("UpdateProduct")]
        public IActionResult UpdateProduct(ProductDetailsModel updates)
        {
            try
            {
                return Accepted(model.UpdateProduct(updates));
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                return Accepted(model.DeleteProduct(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}
