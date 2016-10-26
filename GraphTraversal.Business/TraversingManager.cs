using GraphTraversal.Business.Interfaces;
using GraphTraversal.Business.Models;
using GraphTraversal.Data.Entities;
using GraphTraversal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.Business
{
    /// <summary>
    /// Class meant for traversing the graph.
    /// </summary>
    public class TraversingManager : BaseManager, ITraversingManager
    {
        /// <summary>
        /// Gets or set the node repository.
        /// </summary>
        private INodeRepository NodeRepository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraversingManager"/> class.
        /// </summary>
        /// <param name="dbClient">Database context.</param>
        public TraversingManager(IClient dbClient, INodeRepository nodeRepository)
            : base(dbClient)
        {
            this.NodeRepository = nodeRepository;
        }

        /// <summary>
        /// Searches for the quickest path.
        /// </summary>
        /// <param name="startId">Start id.</param>
        /// <param name="endId">End id.</param>
        /// <returns></returns>
        public async Task ShortestPath(string startId, string endId)
        {
            if (string.IsNullOrEmpty(startId) || string.IsNullOrEmpty(endId))
            {
                log.Error("Provided node pair for shortest path calculation is empty");
                return;
            }

            var wholeTree = await this.NodeRepository.GetConnectedNodes(startId);
        }
    }
}
