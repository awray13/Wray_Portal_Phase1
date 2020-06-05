using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wray_Portal_Phase1.Startup))]
namespace Wray_Portal_Phase1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
