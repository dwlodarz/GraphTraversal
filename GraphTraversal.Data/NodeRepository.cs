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
                                .Return((node, child) => new SubTreeEntity
                                {
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

        /// <summary>
        /// Adds new node if it doesn't exist.
        /// </summary>
        /// <param name="node">New node entity.</param>
        /// <returns>The Task.</returns>
        public async Task<NodeEntity> AddOrUpdateAsync(NodeEntity node)
        {
            try
            {
                node.Cost = 1;

                await this.DbContext.Cypher
                    .Merge("(n:NodeEntity { Id: {id} })")
                    .OnCreate()
                    .Set("n = {node}")
                    .WithParams(new
                    {
                        id = node.Id,
                        node
                    })
                    .ExecuteWithoutResultsAsync();

                if (!string.IsNullOrEmpty(node.Label))
                {
                    await this.DbContext.Cypher
                        .Match("(n:NodeEntity)")
                        .Where((NodeEntity n) => n.Id == node.Id)
                        .Set("n.Label = {label}")
                        .WithParam("label", node.Label)
                        .ExecuteWithoutResultsAsync();
                }
                return node;
            }
            catch (Exception e)
            {
                log.ErrorFormat("Add new node entity failed: {0}", e);
                throw;
            }
        }

        /// <summary>
        /// Add adjacent node if there are non existent.
        /// </summary>
        /// <param name="adjacentNodes">Adjacent node.</param>
        /// <param name="rootId">Root id.</param>
        /// <returns></returns>
        public async Task AdjacentNodesIfDoNotExistAsync(string rootId, IEnumerable<NodeEntity> adjacentNodes)
        {
            try
            {
                if (adjacentNodes != null && adjacentNodes.Any())
                {
                    adjacentNodes.ToList().ForEach(async nodeModel =>
                    {
                        var node = await this.AddOrUpdateAsync(nodeModel);

                        //relate bidirectional root with adjacent node
                        await this.DbContext.Cypher
                        .Match("(root:NodeEntity)", "(childNode:NodeEntity)")
                        .Where((NodeEntity root) => root.Id == rootId)
                        .AndWhere((NodeEntity childNode) => childNode.Id == node.Id)
                        .CreateUnique("(root)-[:CONNECTED]->(childNode)")
                        .CreateUnique("(childNode)-[:CONNECTED]->(root)")
                        .ExecuteWithoutResultsAsync();
                    });
                }
            }
            catch (Exception e)
            {
                log.ErrorFormat("Add adjacent node entity failed: {0}", e);
                throw;
            }
        }
    }
}
