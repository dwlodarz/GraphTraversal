using GraphTraversal.Data.Interfaces;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Data
{
    /// <summary>
    /// DB client class.
    /// </summary>
    public class Client : GraphClient, IClient
    {
        /// <summary>
        /// Initializes the instance of Client.
        /// </summary>
        public Client()
            : base(new Uri(ConfigurationManager.AppSettings["GraphDBUrl"])
            , ConfigurationManager.AppSettings["GraphDBUser"]
            , ConfigurationManager.AppSettings["GraphDBPassword"])
        {
            this.Connect();
            //var r = (this as IGraphClient);
            //var a = r.Cypher.Match("(m:client)").Return((m => new { mm = m.As<object>() }));
            //var data = a.Results.ToList();
        }

        /// <summary>
        /// Marks the object as ready to be disposed.
        /// </summary>
        /// <param name="disposing">If it's already disposing.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
