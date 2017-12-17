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