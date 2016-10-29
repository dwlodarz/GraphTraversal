using GraphTraversal.Business.Interfaces;
using GraphTraversal.Data.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using GraphTraversal.Business.Models;
using Ploeh.AutoFixture;
using GraphTraversal.Data.Entities;

namespace GraphTraversal.Business.Tests
{
    /// <summary>
    /// Test c;ass for NodeManager.
    /// </summary>
    [TestFixture]
    public class NodeManagerTests
    {
        /// <summary>
        /// Auto fixture for populating data.
        /// </summary>
        private Fixture fixture = new Fixture();

        /// <summary>
        /// Node Repository.
        /// </summary>
        private INodeRepository repository;

        /// <summary>
        /// Node manager instance.
        /// </summary>
        private INodeManager nodeManager;

        /// <summary>
        /// Setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.repository = Substitute.For<INodeRepository>();
            this.nodeManager = new NodeManager(repository);
        }

        [Test]
        public async Task RelatePath_SuccesfullyCallsAddAndRelate()
        {
            var nodeModel = this.fixture.Create<NodeModel>();
            var adjacentNodes = this.fixture.CreateMany<NodeEntity>(2);
            var nodeEntity = new NodeEntity{Id = nodeModel.Id, Label = nodeModel.Label, Cost = 1};


            this.repository.AddOrUpdateAsync(Arg.Any<NodeEntity>()).Returns(Task.FromResult<NodeEntity>(nodeEntity));
            this.repository.AdjacentNodesIfDoNotExistAsync(nodeModel.Id, adjacentNodes).Returns(Task.FromResult(0));

            await this.nodeManager.RelatePath(nodeModel);

            //calculator.Received().Add(Arg.Any<int>(), 2);
            await this.repository.Received(1).AddOrUpdateAsync(Arg.Is<NodeEntity>(x => x.Id == nodeEntity.Id));
            await this.repository.Received(1).AdjacentNodesIfDoNotExistAsync(Arg.Is<string>(x => x == nodeEntity.Id), Arg.Is<IEnumerable<NodeEntity>>(a => a.Count() == adjacentNodes.Count()));
        }
    }
}
