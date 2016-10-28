using GraphTraversal.Business.Interfaces;
using GraphTraversal.Business.Models;
using GraphTraversal.WebServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.WebServices
{
    /// <summary>
    /// Service used for the front-end application.
    /// </summary>
    public class FrontEndService : BaseService<INodeManager>, IFrontEndService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrontEndService"/> class.
        /// </summary>
        /// <param name="nodeManager">Database node manager.</param>
        public FrontEndService(INodeManager nodeManager)
            : base(nodeManager)
        {
        }

        /// <summary>
        /// Gets the data for displaying.
        /// </summary>
        /// <returns>Graph view model</returns>
        public async Task<GraphViewModel> GetGraphData()
        {
            try
            {
                return await this.Manager.GetGraphDataForDisplaying();
            }
            catch (Exception e)
            {
                WebOperationContext ctx = WebOperationContext.Current;
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return null;
            }
        }
    }
}
