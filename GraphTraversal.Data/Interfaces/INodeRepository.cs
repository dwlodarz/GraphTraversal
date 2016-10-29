using GraphTraversal.Data.Entities;
using System;
using System.Collections.Generic;
namespace GraphTraversal.Data.Interfaces
{
    /// <summary>
    /// Database access repository for nodes.
    /// </summary>
    public interface INodeRepository
    {
        /// <summary>
        /// Gets connected nodes.
        /// </summary>
        /// <returns>List of connected nodes.</returns>
        System.Threading.Tasks.Task<IEnumerable<SubTreeEntity>> GetWholeTree();

        /// <summary>
        /// Adds new node if it doesn't exist.
        /// </summary>
        /// <param name="node">New node entity.</param>
        /// <returns>The Task.</returns>
        System.Threading.Tasks.Task<NodeEntity> AddOrUpdateAsync(NodeEntity node);

        /// <summary>
        /// Add adjacent node if there are non existent.
        /// </summary>
        /// <param name="adjacentNodes">Adjacent node.</param>
        /// <param name="rootId">Root id.</param>
        /// <returns></returns>
        System.Threading.Tasks.Task AdjacentNodesIfDoNotExistAsync(string rootId, IEnumerable<NodeEntity> adjacentNodes);
    }
}
