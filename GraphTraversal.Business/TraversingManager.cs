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
        /// <returns>The shortest path.</returns>
        public async Task<ShortestPathModel> ShortestPath(string startId, string endId)
        {
            if (string.IsNullOrEmpty(startId) || string.IsNullOrEmpty(endId))
            {
                log.Error("Provided node pair for shortest path calculation is empty");
                return null;
            }

            var wholeTree = await this.NodeRepository.GetWholeTree();
            var shortestPath = this.PerformCalculation(startId, endId, wholeTree);

            if (shortestPath == null || !shortestPath.Any())
            {
                return null;
            }

            return new ShortestPathModel { Path = shortestPath.Select(sp => new NodeViewModel { Id = sp }).ToList() };
        }

        /// <summary>
        /// Performs Dijkstra's shortest path algorithm.
        /// </summary>
        /// <param name="startId">Id of the initial node.</param>
        /// <param name="endId">Id of the final node.</param>
        /// <param name="wholeTree">The whole tree structure.</param>
        /// <returns>Cheapest path.</returns>
        private List<string> PerformCalculation(string startId, string endId, IEnumerable<SubTreeEntity> wholeTree)
        {
            var visited = new Dictionary<string, string>();
            List<string> path = null;

            //Dictionary for storing sums of distances between nodes.
            var distances = wholeTree.ToDictionary(k => k.Root.Id, v => int.MaxValue);
            distances[startId] = 0;
            var nodes = wholeTree.Select(x => x.Root.Id).ToList();

            while (nodes.Any())
            {
                //sort remaining nodes in order to pick the cheapest route.
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var cheapestStep = nodes[0];
                nodes.Remove(cheapestStep);

                if (cheapestStep == endId)
                {
                    //final result
                    path = GatherFinalPathInfo(visited, endId, startId);
                    break;
                }

                if (distances[cheapestStep] == int.MaxValue)
                {
                    break;
                }

                //Define which child of the cheapest step is the most suitable to be picked.
                DefineChildWithTheSmallestDistance(wholeTree, visited, distances, cheapestStep);
            }

            return path;
        }

        /// <summary>
        /// Gathers the final path data.
        /// </summary>
        /// <param name="visited">Already visited nodes.</param>
        /// <param name="endId">Id of the final step.</param>
        /// <param name="startId">Id of the initial step.</param>
        /// <returns></returns>
        private static List<string> GatherFinalPathInfo(Dictionary<string, string> visited, string endId, string startId)
        {
            var path = new List<string>();
            while (visited.ContainsKey(endId))
            {
                path.Add(endId);
                endId = visited[endId];
            }

            path.Add(startId);
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Defines which child will give the smallest overall distance.
        /// </summary>
        /// <param name="wholeTree">The whole tree.</param>
        /// <param name="visited">Already visited nodes.</param>
        /// <param name="distances"><List of distances./param>
        /// <param name="cheapestStep">Cheapest performed step.</param>
        private static void DefineChildWithTheSmallestDistance(IEnumerable<SubTreeEntity> wholeTree, Dictionary<string, string> visited, Dictionary<string, int> distances, string cheapestStep)
        {
            if (wholeTree.Any(x => x.Root.Id == cheapestStep))
            {
                var currentChildren = wholeTree.SingleOrDefault(x => x.Root.Id == cheapestStep).Children;
                if (currentChildren != null && currentChildren.Any())
                {
                    currentChildren.ToList().ForEach(c =>
                    {
                        var distanceSum = distances[cheapestStep] + c.Cost;
                        if (distanceSum < distances[c.Id])
                        {
                            distances[c.Id] = distanceSum;
                            visited[c.Id] = cheapestStep;
                        }
                    });
                }
            }
        }
    }
}
