using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pymes_project.Startup))]
namespace Pymes_project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
