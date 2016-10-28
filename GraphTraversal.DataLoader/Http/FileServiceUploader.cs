using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.DataLoader.Http
{
    public class FileServiceUploader
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
                this.PostMessage(new Uri(WebServiceUrl), jsonData);
            }
        }

        /// <summary>
        /// Uploads the data via WebService
        /// </summary>
        /// <param name="uri">The webService's uri.</param>
        /// <param name="data">Data to be transferred.</param>
        private void PostMessage(Uri uri, string data)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);

            request.Method = "POST";
            request.ContentType = "text/plain;charset=utf-8";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] bytes = encoding.GetBytes(data);

            request.ContentLength = bytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                // Send the data.
                requestStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
