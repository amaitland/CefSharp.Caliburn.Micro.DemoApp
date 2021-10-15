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

            var moduleDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pattern = "*.deps.json";

            var assemblyLoadContext = new PluginAssemblyLoadContext(moduleDir);

            //We only want dlls that have a .deps.json file
            var deps = Directory
                .GetFiles(moduleDir, pattern);

            //Exclude the current assembly
            var files = deps
                .Select(x => Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(x)) + ".dll")
                .Where(x => x != Path.GetFileName(Assembly.GetExecutingAssembly().Location));

            var assemblies = files
                .Select(x => Path.Combine(moduleDir, x))
                .Select(assemblyLoadContext.LoadFromAssemblyPath);

            foreach(var assembly in assemblies)
            {
                var module = loader.LoadModule(assembly);

                module?.Init();
            }

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
