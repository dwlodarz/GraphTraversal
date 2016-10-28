using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.DataLoader
{
    /// <summary>
    /// Bootstrap class for injecting dependencies.
    /// </summary>
    internal class Bootstrap
    {
        public static Container container;

        /// <summary>
        /// Starts the DI mapping.
        /// </summary>
        public static void Start()
        {
            container = new Container();

            // Register your types, for instance:
            //container.Register<IUserContext, WinFormsUserContext>();
            // Optionally verify the container.
            container.Verify();
        }
    }
}
