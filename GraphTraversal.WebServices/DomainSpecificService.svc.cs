using GraphTraversal.Business;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DomainSpecificService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DomainSpecificService.svc or DomainSpecificService.svc.cs at the Solution Explorer and start debugging.
    public class DomainSpecificService : BaseService<ITraversingManager>, IDomainSpecificService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainSpecificService"/> class.
        /// </summary>
        /// <param name="traversingManager">Database traversing manager.</param>
        public DomainSpecificService(ITraversingManager traversingManager)
            : base(traversingManager)
        {
        }

        /// <summary>
        /// Calculates the shortest path.
        /// </summary>
        /// <param name="startId">Id of the start node.</param>
        /// <param name="endId">Id of the end node.</param>
        public async Task<ShortestPathModel> ShortestPath(string startId, string endId)
        {
            try
            {
                return await this.Manager.ShortestPath(startId, endId);
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
