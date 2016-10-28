using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Business.Models
{
    /// <summary>
    /// Represents the Shortest path.
    /// </summary>
    public class ShortestPathModel
    {
        /// <summary>
        /// Gets or sets the ordered list of concurrent nodes.
        /// </summary>
        public List<NodeViewModel> Path { get; set; }
    }
}
