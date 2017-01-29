using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Anonco.Startup))]
namespace Anonco
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
