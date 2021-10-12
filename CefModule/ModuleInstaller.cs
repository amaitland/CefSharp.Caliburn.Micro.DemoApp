using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CefModule.ViewModels;
using Contracts;

namespace CefModule
{
    public class ModuleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(Component.For<CefViewModel>())
                .Register(Component.For<IModule>().ImplementedBy<ModuleInitializer>());
        }
    }
}
