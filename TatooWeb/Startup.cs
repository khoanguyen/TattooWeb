using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TatooWeb.Startup))]
namespace TatooWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
