using CefModule.ViewModels;
using Contracts;

namespace CefModule
{
    public class ModuleInitializer : IModule
    {
        private readonly IShell _shell;
        private readonly CefViewModel _cefViewModel;

        public ModuleInitializer(IShell shell, CefViewModel cefViewModel)
        {
            _shell = shell;
            _cefViewModel = cefViewModel;
        }

        public void Init()
        {
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "Cef Module", ScreenViewModel = _cefViewModel });
        }
    }
}
