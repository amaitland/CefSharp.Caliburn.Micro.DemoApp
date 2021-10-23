using Caliburn.Micro;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Contracts;
using System.Reflection;

namespace DemoApp
{
    public class AppModuleLoader
    {
        private readonly IWindsorContainer _mainContainer;

        public AppModuleLoader(IWindsorContainer mainContainer)
        {
            _mainContainer = mainContainer;
        }

        public IModule LoadModule(Assembly assembly)
        {
            try
            {
                var moduleInstaller = FromAssembly.Instance(assembly);

                var modulecontainer = new WindsorContainer();

                _mainContainer.AddChildContainer(modulecontainer);

                modulecontainer.Install(moduleInstaller);

                var module = modulecontainer.Resolve<IModule>();

                if (!AssemblySource.Instance.Contains(assembly))
                    AssemblySource.Instance.Add(assembly);

                return module;
            }
            catch
            {
                //TODO: good exception handling 
                throw;
            }
        }
    }
}
