using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphTraversal.DataLoader;

namespace GraphTraversal.DataLoader.DirectoryScanning
{
    /// <summary>
    /// Gets Files from the directory.
    /// </summary>
    public class FileDownloader : GraphTraversal.DataLoader.DirectoryScanning.IFileDownloader
    {
        /// <summary>
        /// Path to the content directory.
        /// </summary>
        private readonly string dirPath = ConfigurationManager.AppSettings["dirPath"];

        /// <summary>
        /// Gets all the file names from the directory.
        /// </summary>
        /// <returns>List of files.</returns>
        public IEnumerable<string> GetAllTheFileNames()
        {
            return Directory.EnumerateFiles(dirPath, "*.xml");
        }

        /// <summary>
        /// Retrieves file content deserialized.
        /// </summary>
        /// <typeparam name="T">Contained type.</typeparam>
        /// <param name="fileName">The file name.</param>
        /// <returns>Typed content.</returns>
        public T GetFileContentSerialized<T>(string fileName, int noOfTries = 0) where T : class, new()
        {
            try
            {
                if (noOfTries > 10)
                {
                    return null;
                }

                string content = File.ReadAllText(Path.Combine(dirPath, fileName));
                if (!string.IsNullOrEmpty(content))
                {
                    return content.XmlDeserializeFromString<T>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error while reading the file: " + e.GetBaseException().Message);
                return this.GetFileContentSerialized<T>(fileName, noOfTries++);
            }
            return null;
        }
    }
}
