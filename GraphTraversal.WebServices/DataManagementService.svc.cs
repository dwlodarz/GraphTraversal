using GraphTraversal.WebServices.Contracts;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataManagementService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataManagementService.svc or DataManagementService.svc.cs at the Solution Explorer and start debugging.
    public class DataManagementService : IDataManagementService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<string> DoWork()
        {
            Log.Error("test");
            return "test";
        }
    }
}
