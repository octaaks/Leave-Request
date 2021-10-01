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
    public class JobsController : BaseController<Job, JobRepository, int>
    {
        public JobsController(JobRepository repository) : base(repository)
        {

        }
    }
}
