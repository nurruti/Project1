using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Urrutia_Project1.Models;
using Microsoft.Extensions.Logging;

namespace Urrutia_Project1.Controllers
{
    public class OrderDetailsController : Controller
    {
        OrderDetailsModel model = new OrderDetailsModel();

        private readonly ILogger<OrderDetailsController> _logger;

        public OrderDetailsController(ILogger<OrderDetailsController> logger)
        {
            _logger = logger;
        }

        //CRUD
        #region POST
        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder(OrderDetailsModel newOrder)
        {
            try
            {
                return Created("", model.AddOrder(newOrder));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        #endregion

        #region GET List
        [HttpGet]
        [Route("oList")]
        public IActionResult Orderlist()
        {
            return Ok(model.GetOrderList());
        }
        #endregion

        #region GET by ID
        [HttpGet]
        [Route("OrderDetails")]
        public IActionResult GetOrdertById(int cId)
        {
            try
            {
                return Ok(model.GetOrderDetails(cId));
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("UpdateOrder")]
        public IActionResult UpdateOrder(OrderDetailsModel updates)
        {
            try
            {
                return Accepted(model.UpdateOrder(updates));
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(int cId)
        {
            try
            {
                return Accepted(model.DeleteOrder(cId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}
