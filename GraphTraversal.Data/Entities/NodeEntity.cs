using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Data.Entities
{
    /// <summary>
    /// Stores a node with related data.
    /// </summary>
    public class NodeEntity
    {
        /// <summary>
        /// A unique string identifying the node.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Human readable text to be displayed in UI.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// IDs of nodes adjacent to current.
        /// </summary>
        public IList<NodeEntity> AdjacentNodes { get; set; }
    }
}
