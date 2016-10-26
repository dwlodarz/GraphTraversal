using GraphTraversal.Business.Models;
using System;
using System.Collections.Generic;
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
        /// <returns>The shortest path.</returns>
        Task<ShortestPathModel> ShortestPath(string startId, string endId);
    }
}
