using Leave_Request.Models;
using Leave_Request.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : BaseController<LeaveType, LeaveTypeRepository, int>
    {
        public LeaveTypesController(LeaveTypeRepository repository) : base(repository)
        {

        }
    }
}
