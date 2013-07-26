using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace MvcWindsorIntegration.Classes.Factory
{
    public class WindsorControllerFactory: DefaultControllerFactory
    {
        private readonly IWindsorContainer _windsorContainer;

        public WindsorControllerFactory(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;

            return _windsorContainer.Resolve(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;

            if (disposableController != null)
            {
                disposableController.Dispose();
            }

            _windsorContainer.Release(controller);
        }
    }
}
