using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using SimpleInjector;
using GraphTraversal.Data.Interfaces;
using GraphTraversal.Data;
using SimpleInjector.Integration.Wcf;

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
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();
            container.Register<IClient, Client>(Lifestyle.Scoped);
            container.Verify();
            SimpleInjectorServiceHostFactory.SetContainer(container);
        } 
    }
}