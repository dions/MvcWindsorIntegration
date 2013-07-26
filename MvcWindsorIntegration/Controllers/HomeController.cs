using System.Web.Mvc;
using MvcWindsorIntegration.Classes.Interfaces;

namespace MvcWindsorIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService _testService;

        public HomeController(ITestService testService)
        {
            _testService = testService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = _testService.GotIt("test");

            return View();
        }

    }
}
