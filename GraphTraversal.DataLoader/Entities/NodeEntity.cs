using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.DataLoader.Entities
{
    /// <summary>
    /// Class used for serialization and deserialization of messages.
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("node")]
    [DataContract(Name="node")]

    public class NodeEntity
    {
        /// <summary>
        /// A unique string identifying the node.
        /// </summary>
        [System.Xml.Serialization.XmlElement("id")]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Human readable text to be displayed in UI.
        /// </summary>
        [System.Xml.Serialization.XmlElement("label")]
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// IDs of nodes adjacent to current.
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("adjacentNodes")]
        [System.Xml.Serialization.XmlArrayItemAttribute("id", IsNullable = false)]
        [System.Runtime.Serialization.IgnoreDataMember]
        public List<string> AdjacentNodeIds { get; set; }

        /// <summary>
        /// Adjacent Nodes.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        [DataMember]
        public IEnumerable<NodeEntity> AdjacentNodes
        {
            get
            {
                if (AdjacentNodeIds == null)
                {
                    return null;
                }

                return this.AdjacentNodeIds
                    .Select(an =>
                        new NodeEntity
                        {
                            Id = an
                        });
            }
        }
    }
}