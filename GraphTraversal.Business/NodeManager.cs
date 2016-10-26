﻿using GraphTraversal.Business.Interfaces;
using GraphTraversal.Business.Models;
using GraphTraversal.Data.Entities;
using GraphTraversal.Data.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Business
{
    /// <summary>
    /// Handles data shuffling regarding nodes.
    /// </summary>
    public class NodeManager : BaseManager, INodeManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeManager"/> class.
        /// </summary>
        /// <param name="dbClient">Database context.</param>
        public NodeManager(IClient dbClient)
            : base(dbClient)
        {
        }

        /// <summary>
        /// Relates the root node with adjacent ones.
        /// </summary>
        /// <param name="node">The root node of a subtree.</param>
        /// <returns>The task</returns>
        public async Task RelatePath(NodeModel node)
        {
            await this.AddOrUpdateAsync(node);
            await this.AdjacentNodesIfDoNotExistAsync(node.Id, node.AdjacentNodes);
        }

        /// <summary>
        /// Adds new node if it doesn't exist.
        /// </summary>
        /// <param name="node">New node entity.</param>
        /// <returns>The Task.</returns>
        private async Task<NodeEntity> AddOrUpdateAsync(NodeModel nodeModel)
        {
            try
            {
                var node = AutoMapper.Mapper.Map<NodeEntity>(nodeModel);
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

                //update label
                await this.DbContext.Cypher
                    .Match("(n:NodeEntity)")
                    .Where((NodeEntity n) => n.Id == node.Id)
                    .Set("n.Label = {label}")
                    .WithParam("label", node.Label)
                    .ExecuteWithoutResultsAsync();

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
        private async Task AdjacentNodesIfDoNotExistAsync(string rootId, IEnumerable<NodeModel> adjacentNodes)
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
