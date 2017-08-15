using Autofac;
using Autofac.Integration.Mvc;
using jQueryDataTables.Controllers;
using jQueryDataTables.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jQueryDataTables.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
                .AsImplementedInterfaces();

            builder.RegisterModule(new AutofacWebTypesModule());

            builder.RegisterType<ApplicationDbContext>().InstancePerHttpRequest();

            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>();

            builder.RegisterType<UserManager<ApplicationUser>>();


            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}