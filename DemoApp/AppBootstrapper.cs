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

        protected async override void OnStartup(object sender, StartupEventArgs e)
        {
            var loader = _container.Resolve<AppModuleLoader>();

            var moduleDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pluginsFolder = Directory.GetDirectories(Path.Combine(moduleDir, "plugins"));

            foreach (var pluginFolder in pluginsFolder)
            {
                const string depsJsonExtensionPattern = "*.deps.json";

                //We only want dlls that have a .deps.json file
                var deps = Directory
                    .GetFiles(pluginFolder, depsJsonExtensionPattern);

                var files = deps
                    .Select(x => Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(x)) + ".dll");

                foreach (var file in files)
                {
                    var filePath = Path.Combine(pluginFolder, file);

                    var assemblyLoadContext = new PluginAssemblyLoadContext(filePath);
                    var assemblyName = AssemblyName.GetAssemblyName(filePath);

                    var assembly = assemblyLoadContext.LoadFromAssemblyName(assemblyName);

                    var module = loader.LoadModule(assembly);

                    module?.Init();
                }
            }

            await DisplayRootViewForAsync<ShellViewModel>();
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
