using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Encryption.Startup))]
namespace Encryption
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
