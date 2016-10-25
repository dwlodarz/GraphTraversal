using GraphTraversal.Business;
using GraphTraversal.Business.Interfaces;
using GraphTraversal.Business.Models;
using GraphTraversal.WebServices.Contracts;
using log4net;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataManagementService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataManagementService.svc or DataManagementService.svc.cs at the Solution Explorer and start debugging.
    public class DataManagementService : BaseService<INodeManager>, IDataManagementService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="manager">Database node manager.</param>
        public DataManagementService(INodeManager nodeManager)
            :base(nodeManager)
        {
        }

        /// <summary>
        /// Adds a node to db.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public async Task AddNode(NodeModel node)
        {
            try
            {
                await this.Manager.RelatePath(node);
            }
            catch (Exception e)
            {
                WebOperationContext ctx = WebOperationContext.Current;
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
        }
    }
}
