using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWebSitePractice1.Startup))]
namespace MyWebSitePractice1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
