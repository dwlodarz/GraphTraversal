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

        /// <summary>
        /// Checks the happy path of RelatePath method.
        /// </summary>
        /// <returns>The task.</returns>
        [Test]
        public async Task RelatePath_SuccesfullyCallsAddAndRelate()
        {
            var nodeModel = this.fixture.Build<NodeModel>().Without(x => x.AdjacentNodes).Create();
            var adjacentNodes = this.fixture.Build<NodeModel>().Without(x => x.AdjacentNodes).CreateMany(2);
            nodeModel.AdjacentNodes = adjacentNodes.ToList() ;

            var nodeEntity = new NodeEntity { Id = nodeModel.Id, Label = nodeModel.Label, Cost = 1 };


            this.repository.AddOrUpdateAsync(Arg.Any<NodeEntity>()).Returns(Task.FromResult<NodeEntity>(nodeEntity));
            this.repository.AdjacentNodesIfDoNotExistAsync(Arg.Any<string>(), Arg.Any<IEnumerable<NodeEntity>>()).Returns(Task.FromResult(0));

            await this.nodeManager.RelatePath(nodeModel);

            await this.repository.Received(1).AddOrUpdateAsync(Arg.Is<NodeEntity>(x => x.Id == nodeEntity.Id));
            await this.repository.Received(1).AdjacentNodesIfDoNotExistAsync(Arg.Is<string>(x => x == nodeEntity.Id), Arg.Is<IList<NodeEntity>>(a => a.Count() == adjacentNodes.Count()));
        }

        /// <summary>
        /// Tests for getting graph data for Front-end.
        /// </summary>
        /// <returns>The Task</returns>
        [Test]
        public async Task GetGraphDataForDisplaying_ReturnAllNodes()
        {
            var wholeTree = fixture.Create<IEnumerable<SubTreeEntity>>();

            this.repository.GetWholeTree().Returns(Task.FromResult<IEnumerable<SubTreeEntity>>(wholeTree));

            var viewModel = await this.nodeManager.GetGraphDataForDisplaying();

            await this.repository.Received(1).GetWholeTree();
            Assert.That(viewModel.Nodes.Count() == wholeTree.Count());
        }

        /// <summary>
        /// Tests for getting graph data for Front-end - DbContext Exception.
        /// </summary>
        /// <returns>The Task</returns>
        [Test]
        public async Task GetGraphDataForDisplaying_DbContextThrowsAnException()
        {
            var wholeTree = fixture.Create<IEnumerable<SubTreeEntity>>();

            this.repository.When(x => x.GetWholeTree()).Do(e => { throw new Exception(); });

            Assert.Throws<Exception>(async() => await this.nodeManager.GetGraphDataForDisplaying());
            await this.repository.Received(1).GetWholeTree();
        }
    }
}
