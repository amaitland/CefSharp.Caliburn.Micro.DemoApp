using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Contracts;
using DemoApp.ViewModels;

namespace DemoApp
{
    public class ShellInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(Component.For<IWindsorContainer>().Instance(container))
                .Register(Component.For<ShellViewModel>() /*.LifeStyle.Singleton*/)
                .Register(Component.For<AppModuleLoader>())
                .Register(Component.For<IShell>().ImplementedBy<ShellInitializer>());

        }
    }
}
