using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GraphTraversal.WebServices.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDomainSpecificService" in both code and config file together.
    [ServiceContract]
    public interface IDomainSpecificService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "dswork")]
        void DoWork();
    }
}
