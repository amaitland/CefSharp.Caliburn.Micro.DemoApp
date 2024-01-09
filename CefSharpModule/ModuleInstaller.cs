using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CefSharpModule.ViewModels;
using Contracts;

namespace CefSharpModule
{
    public class ModuleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(Component.For<CefSharpViewModel>())
                .Register(Component.For<IModule>().ImplementedBy<ModuleInitializer>());
        }
    }
}
