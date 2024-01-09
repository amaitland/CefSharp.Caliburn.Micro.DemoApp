using Caliburn.Micro;
using Castle.Windsor;
using Contracts;
using System.Collections.ObjectModel;

namespace DemoApp.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        private readonly IWindsorContainer _container;
        public ObservableCollection<ShellMenuItem> MenuItems { get; private set; }

        private ShellMenuItem _selectedMenuItem;
        public ShellMenuItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                if (_selectedMenuItem == value)
                    return;
                _selectedMenuItem = value;
                NotifyOfPropertyChange(() => SelectedMenuItem);
                NotifyOfPropertyChange(() => CurrentView);
            }
        }
        public object CurrentView
        {
            get => _selectedMenuItem?.ScreenViewModel;
        }
        public ShellViewModel(IWindsorContainer container)
        {
            _container = container;
            MenuItems = new ObservableCollection<ShellMenuItem>();
        }

        public void Load()
        {
            var loader = _container.Resolve<AppModuleLoader>();
            var dlg = new Microsoft.Win32.OpenFileDialog();

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                var asm = System.Reflection.Assembly.LoadFrom(dlg.FileName);
                var module = loader.LoadModule(asm);
                if (module != null)
                    module.Init();
            }
        }
        public void Show()
        {
            System.Windows.MessageBox.Show(MenuItems.Count.ToString());
        }
    }
}
