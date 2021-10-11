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
    public class LeaveTypesController : BaseController<LeaveType, LeaveTypeRepository, int>
    {
        private readonly LeaveTypeRepository repository;
        public LeaveTypesController(LeaveTypeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}