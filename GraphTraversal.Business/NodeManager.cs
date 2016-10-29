using GraphTraversal.Business.Interfaces;
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
        /// Noge repository instance.
        /// </summary>
        private readonly INodeRepository nodeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeManager"/> class.
        /// </summary>
        /// <param name="dbClient">Database context.</param>
        public NodeManager(INodeRepository nodeRepository)
            : base()
        {
            this.nodeRepository = nodeRepository;
        }

        /// <summary>
        /// Relates the root node with adjacent ones.
        /// </summary>
        /// <param name="node">The root node of a subtree.</param>
        /// <returns>The task</returns>
        public async Task RelatePath(NodeModel nodeModel)
        {
            var node = AutoMapper.Mapper.Map<NodeEntity>(nodeModel);
            var adjacentNodes = AutoMapper.Mapper.Map<List<NodeEntity>>(nodeModel.AdjacentNodes);

            await nodeRepository.AddOrUpdateAsync(node);
            await nodeRepository.AdjacentNodesIfDoNotExistAsync(nodeModel.Id, adjacentNodes);
        }

        /// <summary>
        /// Retrieves the graph and converts it into displayable data type.
        /// </summary>
        /// <returns>Graph view model.</returns>
        public async Task<GraphViewModel> GetGraphDataForDisplaying()
        {
            IEnumerable<SubTreeEntity> wholeTree = await this.nodeRepository.GetWholeTree();
            GraphViewModel viewModel = new GraphViewModel();
            if (wholeTree != null && wholeTree.Any())
            {
                viewModel.Nodes = wholeTree
                    .Select(wte => new NodeViewModel
                    {
                        Id = wte.Root.Id,
                        Label = wte.Root.Label,
                        Type = "Connected"
                    });

                List<EdgeViewModel> listOfEdges = new List<EdgeViewModel>();
                wholeTree.ToList().ForEach(st =>
                {
                    foreach (var child in st.Children)
                    {
                        listOfEdges.Add(new EdgeViewModel { Source = st.Root.Id, Target = child.Id, Label="Connected1" });
                    }
                });

                viewModel.Edges = listOfEdges;
            }

            return viewModel;
        }
    }
}
