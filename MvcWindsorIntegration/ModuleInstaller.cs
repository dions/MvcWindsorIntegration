using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LM.AM.Core.Infrastructure.Session;
using MvcWindsorIntegration.Classes.Factory;
using MvcWindsorIntegration.Classes.Interfaces;
using MvcWindsorIntegration.Classes.Repository;
using MvcWindsorIntegration.Classes.Services;
using MvcWindsorIntegration.Classes.Session;

namespace MvcWindsorIntegration
{
    public class ModuleInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Castle.MicroKernel.Registration.Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestylePerWebRequest()
                .Configure(x => x.Named(x.Implementation.FullName)));
            
            container.Register(
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<WindsorControllerFactory>(),
                Component.For<ITestService>().ImplementedBy<TestService>()
            );

            container.Register(Component.For<IUserSession>().UsingFactoryMethod(k => 
                new DictionaryAdapterFactory().GetAdapter<IUserSession>(new SessionDictionary(HttpContext.Current.Session)))
                .LifestylePerWebRequest());

            container.Register(Component.For<DbContext>()
                .ImplementedBy<DatabaseModelContainer>()
                .LifestylePerWebRequest());

            container.Register(Castle.MicroKernel.Registration.Classes.FromThisAssembly()
                                     .Where(x => x.Name.EndsWith("Repository"))
                                     .WithServiceDefaultInterfaces()
                                     .Configure(x => x.LifestylePerWebRequest()));

            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(RepositoryBase<>)).LifestylePerWebRequest());

            var controllerFactory = container.Resolve<WindsorControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}