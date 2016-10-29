using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.DataLoader.Http
{
    /// <summary>
    /// File service uploader.
    /// </summary>
    public class FileServiceUploader : GraphTraversal.DataLoader.Http.IFileServiceUploader
    {
        /// <summary>
        /// Url to the webservice.
        /// </summary>
        private readonly string WebServiceUrl = ConfigurationManager.AppSettings["endpoint"];

        /// <summary>
        /// Uplaod the graph data.
        /// </summary>
        public async Task UploadGraphData<T>(T content) where T : class
        {
            var jsonData = SerializationHelper.Serialize<T>(content);
            if(!string.IsNullOrEmpty(jsonData))
            {
                await Task.Run(async () => await this.PostMessage(new Uri(WebServiceUrl), jsonData));
            }
        }

        /// <summary>
        /// Uploads the data via WebService
        /// </summary>
        /// <param name="uri">The webService's uri.</param>
        /// <param name="data">Data to be transferred.</param>
        private async Task<bool> PostMessage(Uri uri, string data)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                var response = await client.PostAsync(WebServiceUrl, content);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
