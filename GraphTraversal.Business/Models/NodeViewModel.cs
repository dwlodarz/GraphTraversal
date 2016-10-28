using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GraphTraversal.Business.Models
{
    /// <summary>
    /// Stores a node with related data.
    /// </summary>
    [DataContract]
    public class NodeViewModel
    {
        /// <summary>
        /// A unique string identifying the node.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Human readable text to be displayed in UI.
        /// </summary>
        [DataMember(Name = "caption")]
        public string Label { get; set; }

        /// <summary>
        /// IDs of nodes adjacent to current.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}