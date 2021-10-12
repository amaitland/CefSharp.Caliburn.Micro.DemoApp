using Contracts;
using HelloModule.ViewModels;
using System;

namespace HelloModule
{
    public class ModuleInitializer : IModule
    {
        private readonly IShell _shell;
        private readonly HelloViewModel _helloViewModel;

        public ModuleInitializer(IShell shell, HelloViewModel helloViewModel)
        {
            _shell = shell;
            _helloViewModel = helloViewModel;
        }

        public void Init()
        {
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "Hello Module", ScreenViewModel = _helloViewModel });
        }
    }
}
