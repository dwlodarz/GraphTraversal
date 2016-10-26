using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;
using GraphTraversal.Business.Interfaces;
using GraphTraversal.Business;
using GraphTraversal.Data.Interfaces;
using GraphTraversal.Data;

namespace GraphTraversal.WebServices.App_Code
{
    /// <summary>
    /// WCF initializer.
    /// </summary>
    public class Initializer
    {
        /// <summary>
        /// First method to be called upon startup.
        /// </summary>
        public static void AppInitialize()
        {
            log4net.Config.XmlConfigurator.Configure();

            // register DI.
            var container = SimpleInjectorRegistartion();
            SimpleInjectorServiceHostFactory.SetContainer(container);
        }

        /// <summary>
        /// Contract registration (DI).
        /// </summary>
        /// <returns>The Container.</returns>
        private static Container SimpleInjectorRegistartion()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();
            container.Register<IClient, Client>(Lifestyle.Scoped);
            container.Register<INodeManager, NodeManager>(Lifestyle.Scoped);
            container.Register<INodeRepository, NodeRepository>(Lifestyle.Scoped);
            container.Register<ITraversingManager, TraversingManager>(Lifestyle.Scoped);
            container.Verify();

            return container;
        }
    }
}