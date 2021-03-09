using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

using Mef.Abstraction;

namespace Mef.WebClient.Providers
{
    public class PrinterProvider : IPrinterProvider
    {
        public PrinterProvider()
        {
            Compose();
        }

        [ImportMany]
        public IEnumerable<IPrinter> printers { get; set; }
        public IPrinter printer { get; set; }

        public IPrinter GetPrinter()
        {
            return printer;
        }

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
    }
}
