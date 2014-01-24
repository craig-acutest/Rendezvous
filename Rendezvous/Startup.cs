using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rendezvous.Startup))]
namespace Rendezvous
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
