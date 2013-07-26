using System.Linq;
using System.Web.Mvc;
using MvcWindsorIntegration.Classes.Interfaces;

namespace MvcWindsorIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService _testService;
        private readonly IRepository<User> _userRepository;

        public HomeController(ITestService testService, IRepository<User> userRepository)
        {
            _testService = testService;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Message = _testService.GotIt("test");

            var userTest = _userRepository.AsQueryable().FirstOrDefault(x => x.UserId == 1);
            ViewBag.Username = userTest.UserName;

            return View();
        }

    }
}
