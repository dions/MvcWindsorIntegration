using MvcWindsorIntegration.Classes.Interfaces;

namespace MvcWindsorIntegration.Classes.Services
{
    public class TestService : ITestService
    {
        public string GotIt(string what)
        {
            if (string.IsNullOrEmpty(what))
                what = "nothing";

            return "Got " + what;
        }
    }
}