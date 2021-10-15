using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace DemoApp
{
    public class PluginAssemblyLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;
        private string _pluginPath;

        public PluginAssemblyLoadContext(string pluginPath)
        {
            _pluginPath = pluginPath;
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath == null)
            {
                if(assemblyName.Name.StartsWith("CefSharp"))
                {
                    assemblyPath = Path.Combine(_pluginPath, assemblyName.Name + ".dll");
                }

                if (assemblyPath == null || !File.Exists(assemblyPath))
                {
                    return null;
                }
            }

            return LoadFromAssemblyPath(assemblyPath);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}
