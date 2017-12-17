using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OpenPWCL.Controllers
{
    public class TaskRunnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}