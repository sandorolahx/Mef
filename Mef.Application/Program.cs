using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

using Mef.Abstraction;

namespace Mef.Application
{
    internal class Program
    {
        [ImportMany]
        public IEnumerable<IPrinter> printers { get; set; }
        public IPrinter printer { get; set; }
        private void Compose()
        {
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var path = Path.Combine(Path.GetDirectoryName(executableLocation), "Plugins");
            var assemblies = Directory
                        .GetFiles(path, "*.dll", SearchOption.AllDirectories)
                        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                        .ToList();
            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);



            using (var container = configuration.CreateContainer())
            {
                //printers = container.GetExports<IPrinter>();
                printer = container.GetExport<IPrinter>();
            }
        }

        public void Run()
        {
            Compose();
        }

        private static void Main(string[] args)
        {
            var program = new Program();
            program.Run();

            Console.WriteLine(program.printer.Print());

            Console.Read();
        }
    }
}
