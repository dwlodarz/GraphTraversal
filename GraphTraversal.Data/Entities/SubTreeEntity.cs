using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Data.Entities
{
    /// <summary>
    /// Stores a subtree with related data.
    /// </summary>
    public class SubTreeEntity
    {
        /// <summary>
        /// Gets or sets root node.
        /// </summary>
        public NodeEntity Root { get; set; }

        /// <summary>
        /// Gets or sets children nodes.
        /// </summary>
        public IEnumerable<NodeEntity> Children { get; set; }
    }
}
