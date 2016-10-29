using System;
namespace GraphTraversal.DataLoader.DirectoryScanning
{
    /// <summary>
    /// Downloads files from the directory and sends them to WCFservice.
    /// </summary>
    public interface IFileContentTransmitter
    {
        /// <summary>
        /// Gets file by file and pushes them to the WebService.
        /// </summary>
        void GetFilesAndPushThemToServiceEndpoint();
    }
}
