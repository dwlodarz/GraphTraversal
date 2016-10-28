using GraphTraversal.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.WebServices.Contracts
{
    /// <summary>
    /// Service used for the front-end application.
    /// </summary>
    [ServiceContract]
    public interface IFrontEndService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "graphdata")]
        Task<GraphViewModel> GetGraphData();
    }
}
