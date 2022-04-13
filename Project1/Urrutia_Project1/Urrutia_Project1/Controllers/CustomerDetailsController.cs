using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Urrutia_Project1.Models;
using Microsoft.Extensions.Logging;

namespace Urrutia_Project1.Controllers
{
    public class CustomerDetailsController : Controller
    {
        CustomerDetailsModel model = new CustomerDetailsModel();

        private readonly ILogger<CustomerDetailsController> _logger;

        public CustomerDetailsController(ILogger<CustomerDetailsController> logger)
        {
            _logger = logger;
        }

        //CRUD
        #region POST
        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer(CustomerDetailsModel newCustomer)
        {
            try
            {
                return Created("", model.AddCustomer(newCustomer));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        #endregion

        #region GET List
        [HttpGet]
        [Route("cList")]
        public IActionResult Customerlist()
        {
            return Ok(model.GetCustomerList());
        }
        #endregion

        #region GET by ID
        [HttpGet]
        [Route("CustomerDetails")]
        public IActionResult GetCustomertById(int cId)
        {
            try
            {
                return Ok(model.GetCustomerDetails(cId));
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer(CustomerDetailsModel updates)
        {
            try
            {
                return Accepted(model.UpdateCustomer(updates));
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("DeleteCustomer")]
        public IActionResult DeleteCustomer(int cId)
        {
            try
            {
                return Accepted(model.DeleteCustomer(cId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}
