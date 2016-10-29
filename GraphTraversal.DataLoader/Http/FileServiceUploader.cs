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
        public void UploadGraphData<T>(T content) where T : class
        {
            var jsonData = SerializationHelper.Serialize<T>(content);
            if(!string.IsNullOrEmpty(jsonData))
            {
                //await Task.Run(() => ...);
                this.PostMessage(new Uri(WebServiceUrl), jsonData);
            }
        }

        /// <summary>
        /// Uploads the data via WebService
        /// </summary>
        /// <param name="uri">The webService's uri.</param>
        /// <param name="data">Data to be transferred.</param>
        private async void PostMessage(Uri uri, string data)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                var response = client.PostAsync(WebServiceUrl, content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
            }
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);

            //request.Method = "POST";
            //request.ContentType = "text/plain;charset=utf-8";
            //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            //byte[] bytes = encoding.GetBytes(data);

            //request.ContentLength = bytes.Length;

            //using (Stream requestStream = request.GetRequestStream())
            //{
            //    // Send the data.
            //    requestStream.Write(bytes, 0, bytes.Length);
            //    request.GetResponse();
            //}
        }
    }
}
