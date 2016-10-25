using GraphTraversal.Business.Interfaces;
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
        /// Adds new node if it doesn't exist.
        /// </summary>
        /// <param name="node">New node entity.</param>
        /// <returns>The Task.</returns>
        public async Task AddAsync(NodeEntity node)
        {
            try
            {
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
            }
            catch (Exception e)
            {
                log.ErrorFormat("Add new node entity failed: {0}", e);
            }
        }
    }
}
