using Leave_Request.Models;
using Leave_Request.Repositories.Data;
using Leave_Request.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using NETCore.Base;

namespace Leave_Request.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : BaseController<LeaveRequest, LeaveRequestRepository, int>
    {
        private readonly LeaveRequestRepository repository;
        public LeaveRequestsController(LeaveRequestRepository repository) : base(repository)
        {

            this.repository = repository;
        }

        [HttpGet("GetLeaveRequests")]
        public ActionResult GetLeaveRequests()
        {
            var getPerson = repository.GetLeaveRequestVMs();
            if (getPerson != null)
            {

                return Ok(getPerson);
            }
            else
            {
                var get = NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    result = getPerson,
                    message = "Data Empty"
                });
                return get;
            }
        }

        [HttpGet("GetEmployeeLeaveRequests/{id}")]
        public ActionResult GetEmployeeLeaveRequestVMs(int id)
        {
            var getPerson = repository.GetEmployeeLeaveRequestVMs(id);
            if (getPerson != null)
            {

                return Ok(getPerson);
            }
            else
            {
                var get = NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    result = getPerson,
                    message = "No Data"
                });
                return get;
            }
        }

        [HttpPost("Register")]
        public ActionResult Insert(LeaveRequestVM lrVM)
        {
            try
            {
                int output = repository.Insert(lrVM);

                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    message = "Success"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Error!",
                });
            }
        }


        [HttpPost("AddLeaveRequest")]
        public ActionResult AddLeaveRequest(LeaveRequest lrVM)
        {
            try
            {
                int output = repository.AddLeaveRequest(lrVM);

                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    message = "Success"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Error!",
                });
            }
        }
    }
}
