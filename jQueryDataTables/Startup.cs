using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jQueryDataTables.Startup))]
namespace jQueryDataTables
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
