using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pidev.Startup))]
namespace Pidev
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
