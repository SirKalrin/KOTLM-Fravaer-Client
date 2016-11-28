using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fravaer_WebApp_Client.Startup))]
namespace Fravaer_WebApp_Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
