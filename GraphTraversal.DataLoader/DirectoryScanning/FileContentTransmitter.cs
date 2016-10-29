using GraphTraversal.DataLoader.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphTraversal.DataLoader.Entities;
using System.IO;
using System.Configuration;

namespace GraphTraversal.DataLoader.DirectoryScanning
{
    /// <summary>
    /// Downloads files from the directory and sends them to WCFservice.
    /// </summary>
    public class FileContentTransmitter : GraphTraversal.DataLoader.DirectoryScanning.IFileContentTransmitter
    {
        /// <summary>
        /// Workaround for a well know filewatcher bug.
        /// </summary>
        private IList<string> uploadedFiles = new List<string>();

        /// <summary>
        /// FileSystem watcher.
        /// </summary>
        private FileSystemWatcher watcher;

        /// <summary>
        /// Filesystem event handler.
        /// </summary>
        private FileSystemEventHandler fileSystemEventHandler;

        /// <summary>
        /// Path to the content directory.
        /// </summary>
        private readonly string dirPath = ConfigurationManager.AppSettings["dirPath"];

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
                    foreach (var item in allAvailableFiles.Except(uploadedFiles))
                    {
                        GetFileContentAndPostToEndpoint(item);
                    }
                }
                this.WatchDirectory();
            }
            catch (Exception e)
            {
                Console.WriteLine("There has been an error thrown: " + e.GetBaseException().Message);
                throw;
            }
        }

        /// <summary>
        /// Gets the file content and posts to Web Service.
        /// </summary>
        /// <param name="fileName">File name.</param>
        private void GetFileContentAndPostToEndpoint(string fileName)
        {
            var node = this.fileDownloader.GetFileContentSerialized<NodeEntity>(fileName);
            if (node != null)
            {
                this.fileServiceUploader.UploadGraphData(node).Wait();

                //Just to prevent filewatcher from firing twice - Known bug in framework.
                uploadedFiles.Add(fileName);
                Console.WriteLine(string.Format("Node id {0} successfully uploaded", node.Id));
            }
        }

        /// <summary>
        /// Subscribes to file system to watch for changes.
        /// </summary>
        private void WatchDirectory()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = dirPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            this.fileSystemEventHandler = new FileSystemEventHandler(OnFileSystemChanged);
            watcher.Changed += fileSystemEventHandler;
            watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Event handling Directory changes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">File system event arguments.</param>
        private void OnFileSystemChanged(object source, FileSystemEventArgs e)
        {
            if (e != null && !string.IsNullOrEmpty(e.Name) && !uploadedFiles.Contains(e.Name))
            {
                this.GetFileContentAndPostToEndpoint(e.Name);
            }
        }

        /// <summary>
        /// Implemenation of IDisposable.
        /// </summary>
        public void Dispose()
        {
            if(this.fileSystemEventHandler != null)
            {
                watcher.Changed -= this.fileSystemEventHandler;
            }
        }
    }
}
