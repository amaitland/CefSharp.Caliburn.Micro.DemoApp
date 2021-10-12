using Caliburn.Micro;
using Castle.Windsor;
using DemoApp.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace DemoApp
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly IWindsorContainer _container = new WindsorContainer();

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var loader = _container.Resolve<AppModuleLoader>();

            var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pattern = "*.dll";

            Directory
                .GetFiles(exeDir, pattern)
                .Select(Assembly.LoadFrom)
                .Select(loader.LoadModule)
                .Where(module => module != null)
                .ToList()
                .ForEach(module => module.Init());
            //foreach (IModule module in modules)
            //module.Init();

            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            _container.Install(new ShellInstaller());
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)

                ? _container.Kernel.HasComponent(service)
                    ? _container.Resolve(service)
                    : base.GetInstance(service, key)

                : _container.Kernel.HasComponent(key)
                    ? _container.Resolve(key, service)
                    : base.GetInstance(service, key);
        }
    }
}
