using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Contracts;
using HelloModule.ViewModels;

namespace HelloModule
{
    public class ModuleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(Component.For<HelloViewModel>())
                .Register(Component.For<IModule>().ImplementedBy<ModuleInitializer>());
        }
    }
}
