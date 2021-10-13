using Client.Base.Controllers;
using Client.Repositories.Data;
using Leave_Request.Models;
using Leave_Request.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("[controller]")]
    public class LeaveRequestsController : BaseController<LeaveRequest, LeaveRequestRepository, int>
    {
        private readonly LeaveRequestRepository repository;
        public LeaveRequestsController(LeaveRequestRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("AddLeaveRequest")]
        public JsonResult AddLeaveRequest([FromBody] LeaveRequest entity)
        {
            var result = repository.AddLeaveRequest(entity);
            return Json(result);
        }

        [HttpGet("GetLeaveRequests")]
        public async Task<JsonResult> GetLeaveRequests()
        {
            var result = await repository.GetLR();
            return Json(result);
        }

        [HttpGet("GetEmployeeLeaveRequests/{id}")]
        public async Task<JsonResult> GetEmployeeLeaveRequests(int id)
        {
            var result = await repository.GetEmployeeLR(id);
            return Json(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<JsonResult> GetById(int id)
        {
            var result = await repository.GetById(id);
            return Json(result);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
