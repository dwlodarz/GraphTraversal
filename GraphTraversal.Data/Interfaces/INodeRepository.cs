using System;
namespace GraphTraversal.Data.Interfaces
{
    /// <summary>
    /// Database access repository for nodes.
    /// </summary>
    public interface INodeRepository
    {
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<GraphTraversal.Data.Entities.NodeEntity>> GetConnectedNodes(string id);
    }
}
