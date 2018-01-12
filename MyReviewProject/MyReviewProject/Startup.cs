using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyReviewProject.Startup))]
namespace MyReviewProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
