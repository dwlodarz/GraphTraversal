using GraphTraversal.Business.Models;
using System;
namespace GraphTraversal.Business.Interfaces
{
    /// <summary>
    /// Handles data shuffling regarding nodes.
    /// </summary>
    public interface INodeManager
    {
        System.Threading.Tasks.Task RelatePath(NodeModel node);
    }
}
