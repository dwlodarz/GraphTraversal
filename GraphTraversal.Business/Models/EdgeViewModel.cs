using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Business.Models
{
    /// <summary>
    /// Entity designed to stored edges between nodes.
    /// </summary>
    [DataContract]
    public class EdgeViewModel
    {
        /// <summary>
        /// Edge source node id.
        /// </summary>
        [DataMember(Name = "source")]
        public string Source { get; set; }

        /// <summary>
        /// Edge target node id.
        /// </summary>
        [DataMember(Name = "target")]
        public string Target { get; set; }

        /// <summary>
        /// Human readable text to be displayed in UI.
        /// </summary>
        [DataMember(Name = "caption")]
        public string Label { get; set; }
    }
}
