﻿using GraphTraversal.DataLoader.DirectoryScanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            //init of DI.
            Bootstrap.Start();

            var fileContentTransmitter = Bootstrap.container.GetInstance<IFileContentTransmitter>();
            fileContentTransmitter.GetFilesAndPushThemToServiceEndpoint();
            Console.ReadLine();
        }
    }
}