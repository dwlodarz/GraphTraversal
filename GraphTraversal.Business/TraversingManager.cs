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
    public class TraversingManager : BaseManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraversingManager"/> class.
        /// </summary>
        /// <param name="dbClient">Database context.</param>
        public TraversingManager(IClient dbClient)
            : base(dbClient)
        {
        }

        /// <summary>
        /// Searches for the quickest path.
        /// </summary>
        /// <param name="selectedNodeModels">Selected node models.</param>
        /// <returns></returns>
        public async Task ShortestPath(IEnumerable<NodeModel> selectedNodeModels)
        {
            var selectedNodes = AutoMapper.Mapper.Map<IEnumerable<NodeEntity>>(selectedNodeModels);


        }
    }
}
