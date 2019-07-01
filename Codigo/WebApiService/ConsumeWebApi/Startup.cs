using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConsumeWebApi.Startup))]
namespace ConsumeWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
