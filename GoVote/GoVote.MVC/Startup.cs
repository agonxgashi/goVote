using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoVote.MVC.Startup))]
namespace GoVote.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
