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
    }
}
