using GraphTraversal.Business.Models;
using System;
using System.Threading.Tasks;
namespace GraphTraversal.Business.Interfaces
{
    /// <summary>
    /// Handles data shuffling regarding nodes.
    /// </summary>
    public interface INodeManager
    {
        /// <summary>
        /// Relates the root node with adjacent ones.
        /// </summary>
        /// <param name="node">The root node of a subtree.</param>
        /// <returns>The task</returns>
        System.Threading.Tasks.Task RelatePath(NodeModel node);

        /// <summary>
        /// Retrieves the graph and converts it into displayable data type.
        /// </summary>
        /// <returns>Graph view model.</returns>
        Task<GraphViewModel> GetGraphDataForDisplaying();
    }
}
