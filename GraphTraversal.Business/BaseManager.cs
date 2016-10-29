using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GraphTraversal.Data.Interfaces;
using GraphTraversal.Business.Interfaces;
using GraphTraversal.Business.Models;
using GraphTraversal.Data.Entities;

namespace GraphTraversal.Business
{
    /// <summary>
    /// Base class for data management.
    /// </summary>
    public abstract class BaseManager
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseManager"/> class.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        protected BaseManager()
        {
            this.CreateMaps();
        }

        /// <summary>
        /// Creates mapping.
        /// </summary>
        protected virtual void CreateMaps()
        {
            AutoMapper.Mapper.CreateMap<NodeModel, NodeEntity>().ReverseMap();
        }
    }
}
