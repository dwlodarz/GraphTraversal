using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Business.Models
{
    /// <summary>
    /// Graph view model - meant for presenting the data.
    /// </summary>
    [DataContract]
    public class GraphViewModel
    {
        /// <summary>
        /// Additional graph info
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Nodes to be displayed.
        /// </summary>
        [DataMember(Name = "nodes")]
        public IEnumerable<NodeViewModel> Nodes { get; set; }

        /// <summary>
        /// Edges that connect nodes.
        /// </summary>
        [DataMember(Name = "edges")]
        public IEnumerable<EdgeViewModel> Edges { get; set; }
    }
}
