using Neo4jClient;
using Neo4jClient.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Data.Interfaces
{
    public interface IClient : IRawGraphClient, IGraphClient, ICypherGraphClient, IDisposable, ITransactionalGraphClient
    {
    }
}
