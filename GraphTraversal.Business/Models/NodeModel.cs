using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphTraversal.Business.Models
{
    /// <summary>
    /// Stores a node with related data.
    /// </summary>
    public class NodeModel
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
        public IList<NodeModel> AdjacentNodes { get; set; }
    }
}