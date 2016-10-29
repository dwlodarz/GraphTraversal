using System;
namespace GraphTraversal.DataLoader.DirectoryScanning
{
    /// <summary>
    /// Gets Files from the directory.
    /// </summary>
    public interface IFileDownloader
    {
        /// <summary>
        /// Gets all the file names from the directory.
        /// </summary>
        /// <returns>List of files.</returns>
        System.Collections.Generic.IEnumerable<string> GetAllTheFileNames();

        /// <summary>
        /// Retrieves file content deserialized.
        /// </summary>
        /// <typeparam name="T">Contained type.</typeparam>
        /// <param name="fileName">The file name.</param>
        /// <returns>Typed content.</returns>
        T GetFileContentSerialized<T>(string fileName) where T : class, new();
    }
}
