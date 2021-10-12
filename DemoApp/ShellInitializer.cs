using Contracts;
using DemoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    public class ShellInitializer : IShell
    {
        private readonly AppModuleLoader _loader;
        private readonly ShellViewModel _shellViewModel;

        public ShellInitializer(AppModuleLoader loader, ShellViewModel shellViewModel)
        {
            _loader = loader;
            _shellViewModel = shellViewModel;
        }

        public IList<ShellMenuItem> MenuItems { get { return _shellViewModel.MenuItems; } }

        public IModule LoadModule(Assembly assembly)
        {
            return _loader.LoadModule(assembly);
        }
    }
}
