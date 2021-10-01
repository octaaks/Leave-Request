using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using Leave_Request.Models;
using Leave_Request.Repositories.Data;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, int>
    {
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {

        }
    }
}