using GraphTraversal.Data.Entities;
using GraphTraversal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Data
{
    /// <summary>
    /// Database access repository for nodes.
    /// </summary>
    public class NodeRepository : BaseRepository, INodeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeRepository"/> class.
        /// </summary>
        /// <param name="dbClient">Database context.</param>
        public NodeRepository(IClient dbClient)
            : base(dbClient)
        {
        }

        /// <summary>
        /// Gets connected nodes.
        /// </summary>
        /// <returns>List of connected nodes.</returns>
        public async Task<IEnumerable<SubTreeEntity>> GetWholeTree()
        {
            try
            {
                return await this.DbContext.Cypher
                                .OptionalMatch("(node:NodeEntity)-[CONNECTED]-(child:NodeEntity)")
                                .Return((node, child) => new SubTreeEntity {
                                   Root = node.As<NodeEntity>(),
                                   Children = child.CollectAsDistinct<NodeEntity>(),
                                }).ResultsAsync;
            }
            catch (Exception e)
            {
                log.ErrorFormat("Error while retrieving node's children {0}", e);
                throw;
            }
        }
    }
}
