using Autofac;
using jQueryDataTables.App_Start;
using jQueryDataTables.Migrations;
using jQueryDataTables.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace jQueryDataTables
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.Register();
            Database.SetInitializer<ApplicationDbContext>(
                new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}
