using GraphTraversal.DataLoader.DirectoryScanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.DataLoader
{
    /// <summary>
    /// Main program class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main program method.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //init of DI.
            Bootstrap.Start();

            var fileContentTransmitter = Bootstrap.container.GetInstance<IFileContentTransmitter>();
            fileContentTransmitter.GetFilesAndPushThemToServiceEndpoint();

            Console.WriteLine("Press any key to quit...");
            Console.ReadLine();
        }
    }
}
