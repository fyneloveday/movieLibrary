using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(movieLibrary.Startup))]
namespace movieLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
