using GraphTraversal.DataLoader.DirectoryScanning;
using GraphTraversal.DataLoader.Http;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.DataLoader
{
    /// <summary>
    /// Bootstrap class for injecting dependencies.
    /// </summary>
    internal class Bootstrap
    {
        public static Container container;

        /// <summary>
        /// Starts the DI mapping.
        /// </summary>
        public static void Start()
        {
            container = new Container();

            container.Register<IFileDownloader, FileDownloader>(Lifestyle.Transient);
            container.Register<IFileContentTransmitter, FileContentTransmitter>(Lifestyle.Transient);
            container.Register<IFileServiceUploader, FileServiceUploader>(Lifestyle.Transient);

            container.Verify();
        }
    }
}
