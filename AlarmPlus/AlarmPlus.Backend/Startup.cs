using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AlarmPlus.Backend.Startup))]

namespace AlarmPlus.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}