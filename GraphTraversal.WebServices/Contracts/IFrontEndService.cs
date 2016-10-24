using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GraphTraversal.WebServices.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFrontEndService" in both code and config file together.
    [ServiceContract]
    public interface IFrontEndService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "fedowork")]
        void DoWork();
    }
}
