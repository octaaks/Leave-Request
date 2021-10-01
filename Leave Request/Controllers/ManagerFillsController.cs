using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using Leave_Request.Models;
using Leave_Request.Repositories.Data;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerFillsController : BaseController<ManagerFill, ManagerFillRepository, int>
    {
        public ManagerFillsController(ManagerFillRepository repository) : base(repository)
        {

        }
    }
}