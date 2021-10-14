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
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : BaseController<Employee, EmployeeRepository, int>
    {

        private readonly EmployeeRepository repository;
        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<JsonResult> GetEmployeeById(int id)
        {
            var result = await repository.GetEmploById(id);
            return Json(result);
        }
    }
}
