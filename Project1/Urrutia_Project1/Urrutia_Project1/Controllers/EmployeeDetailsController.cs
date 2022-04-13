using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Urrutia_Project1.Models;
using Microsoft.Extensions.Logging;

namespace Urrutia_Project1.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        EmployeeDetailsModel model = new EmployeeDetailsModel();

        private readonly ILogger<EmployeeDetailsController> _logger;

        public EmployeeDetailsController(ILogger<EmployeeDetailsController> logger)
        {
            _logger = logger;
        }

        //CRUD
        #region POST
        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee(EmployeeDetailsModel newEmployee)
        {
            try
            {
                return Created("", model.AddEmployee(newEmployee));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        #endregion

        #region GET List
        [HttpGet]
        [Route("empList")]
        public IActionResult Employeelist()
        {
            return Ok(model.GetEmployeeList());
        }
        #endregion

        #region GET by ID
        [HttpGet]
        [Route("EmployeeDetails")]
        public IActionResult GetEmployeetById(int empId)
        {
            try
            {
                return Ok(model.GetEmployeeDetails(empId));
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult UpdateEmployee(EmployeeDetailsModel updates)
        {
            try
            {
                return Accepted(model.UpdateEmployee(updates));
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult DeleteEmployee(int empId)
        {
            try
            {
                return Accepted(model.DeleteEmployee(empId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}
