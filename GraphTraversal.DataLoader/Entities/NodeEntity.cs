using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.DataLoader.Entities
{
    /// <summary>
    /// Class used for serialization and deserialization of messages.
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("node")]
    public class NodeEntity
    {
        /// <summary>
        /// A unique string identifying the node.
        /// </summary>
        [System.Xml.Serialization.XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// Human readable text to be displayed in UI.
        /// </summary>
        [System.Xml.Serialization.XmlElement("label")]
        public string Label { get; set; }

        /// <summary>
        /// IDs of nodes adjacent to current.
        /// </summary>
        [System.Xml.Serialization.XmlArray("adjacentNodes")]
        public IList<NodeEntity> AdjacentNodes { get; set; }
    }
}
