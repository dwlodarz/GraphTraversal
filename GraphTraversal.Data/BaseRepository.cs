using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GraphTraversal.Data.Interfaces;
using GraphTraversal.Data.Entities;

namespace GraphTraversal.Data
{
    /// <summary>
    /// Base class for data repository.
    /// </summary>
    public abstract class BaseRepository
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets or sets database context.
        /// </summary>
        protected IClient DbContext { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        protected BaseRepository(IClient dbContext)
        {
            this.DbContext = dbContext;
        }
    }
}
