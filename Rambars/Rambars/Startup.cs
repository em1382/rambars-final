using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rambars.Startup))]
namespace Rambars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
