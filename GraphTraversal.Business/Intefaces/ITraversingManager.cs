using System;
using System.Threading.Tasks;
namespace GraphTraversal.Business.Interfaces
{
    /// <summary>
    /// Class meant for traversing the graph.
    /// </summary>
    public interface ITraversingManager
    {
        /// <summary>
        /// Searches for the quickest path.
        /// </summary>
        /// <param name="startId">Start id.</param>
        /// <param name="endId">End id.</param>
        /// <returns></returns>
        Task ShortestPath(string startId, string endId);
    }
}
