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
    public class ReligionsController : BaseController<Religion, ReligionRepository, int>
    {
        private readonly ReligionRepository repository;
        public ReligionsController(ReligionRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetReligion/{id}")]
        public async Task<JsonResult> GetReligion(int id)
        {
            var result = await repository.GetReligionById(id);
            return Json(result);
        }
    }
}
