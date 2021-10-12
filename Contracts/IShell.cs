using System.Collections.Generic;
using System.Reflection;

namespace Contracts
{
    public interface IShell
    {
        IList<ShellMenuItem> MenuItems { get; }

        IModule LoadModule(Assembly assembly);
    }
}
