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
    public class ManagerFillsController : BaseController<ManagerFill, ManagerFillRepository, int>
    {
        private readonly ManagerFillRepository repository;
        public ManagerFillsController(ManagerFillRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetManagerFills")]
        public async Task<JsonResult> GetManagerFills()
        {
            var result = await repository.Get();
            return Json(result);
        }

        [HttpPut("UpdateManagerFill")]
        public JsonResult UpdateManagerFill([FromBody] ManagerFill entity)
        {
            var result = repository.UpdateManagerFill(entity);
            return Json(result);
        }
    }
}