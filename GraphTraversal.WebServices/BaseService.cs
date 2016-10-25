using GraphTraversal.Business;
using GraphTraversal.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphTraversal.WebServices
{
    /// <summary>
    /// Base service class for WCF endpoints.
    /// </summary>
    public abstract class BaseService<T>
    {
        /// <summary>
        /// Gets or sets the manager.
        /// </summary>
        protected T Manager { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="manager">Database manager.</param>
        protected BaseService(T manager)
        {
            this.Manager = manager;
        }
    }
}