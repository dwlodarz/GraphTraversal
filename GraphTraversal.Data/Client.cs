using GraphTraversal.Data.Interfaces;
using log4net;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
        /// Logger instance.
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes the instance of Client.
        /// </summary>
        public Client()
            : base(new Uri(ConfigurationManager.AppSettings["GraphDBUrl"])
            , ConfigurationManager.AppSettings["GraphDBUser"]
            , ConfigurationManager.AppSettings["GraphDBPassword"])
        {
            this.Initialize();
        }

        /// <summary>
        /// Marks the object as ready to be disposed.
        /// </summary>
        /// <param name="disposing">If it's already disposing.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the connection.
        /// </summary>
        private void Initialize()
        {
            try
            {
                this.Connect();
            }
            catch (Exception e)
            {
                log.ErrorFormat("The connection with database cannot be initiated {0}", e);
            }
        }
    }
}
