using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(projetoesw3rd.Startup))]
namespace projetoesw3rd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
