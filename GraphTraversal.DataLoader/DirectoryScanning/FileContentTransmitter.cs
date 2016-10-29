using GraphTraversal.DataLoader.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphTraversal.DataLoader.Entities;

namespace GraphTraversal.DataLoader.DirectoryScanning
{
    /// <summary>
    /// Downloads files from the directory and sends them to WCFservice.
    /// </summary>
    public class FileContentTransmitter : GraphTraversal.DataLoader.DirectoryScanning.IFileContentTransmitter
    {
        /// <summary>
        /// File downloader instance.
        /// </summary>
        private readonly IFileDownloader fileDownloader;

        /// <summary>
        /// File service uploader.
        /// </summary>
        private readonly IFileServiceUploader fileServiceUploader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileContentTransmitter"/> class.
        /// </summary>
        /// <param name="fileServiceUploader">File uploader instance.</param>
        /// <param name="fileDownloader">File downloader instance.</param>
        public FileContentTransmitter(IFileServiceUploader fileServiceUploader, IFileDownloader fileDownloader)
        {
            this.fileDownloader = fileDownloader;
            this.fileServiceUploader = fileServiceUploader;
        }

        /// <summary>
        /// Gets file by file and pushes them to the WebService.
        /// </summary>
        public void GetFilesAndPushThemToServiceEndpoint()
        {
            try
            {
                var allAvailableFiles = this.fileDownloader.GetAllTheFileNames();
                if (allAvailableFiles != null && allAvailableFiles.Any())
                {
                    foreach (var item in allAvailableFiles)
                    {
                        var node = this.fileDownloader.GetFileContentSerialized<NodeEntity>(item);
                        if (node != null)
                        {
                            this.fileServiceUploader.UploadGraphData(node);
                            Console.WriteLine(string.Format("Node id {0} successfully uploaded", node.Id));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There has been an error thrown: " + e.GetBaseException().Message);
                throw;
            }
        }
    }
}
