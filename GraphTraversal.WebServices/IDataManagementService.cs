﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GraphTraversal.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataManagementService" in both code and config file together.
    [ServiceContract]
    public interface IDataManagementService
    {
        [OperationContract]
        [WebInvoke(Method="GET", ResponseFormat=WebMessageFormat.Json, BodyStyle=WebMessageBodyStyle.Wrapped, UriTemplate="dowork")]
        void DoWork();
    }
}
