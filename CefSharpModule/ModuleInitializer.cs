using CefSharp;
using CefSharp.Wpf;
using CefSharpModule.ViewModels;
using Contracts;
using System;
using System.IO;

namespace CefSharpModule
{
    public class ModuleInitializer : IModule
    {
        private readonly IShell _shell;
        private readonly CefSharpViewModel _cefViewModel;

        public ModuleInitializer(IShell shell, CefSharpViewModel cefViewModel)
        {
            _shell = shell;
            _cefViewModel = cefViewModel;
        }

        public void Init()
        {
            if (!Cef.IsInitialized)
            {
                var settings = new CefSettings()
                {
                    //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                    CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp.Caliburn.Micro.DemoApp\\Cache")
                };

                Cef.Initialize(settings);
            }
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "CefSharp Module", ScreenViewModel = _cefViewModel });
        }
    }
}
