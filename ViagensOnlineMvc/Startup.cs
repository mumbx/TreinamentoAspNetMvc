using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(ViagensOnlineMvc.Startup))]

namespace ViagensOnlineMvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(
            new CookieAuthenticationOptions()
            {
                 AuthenticationType = "AppViagensOnLineCookie",
                 LoginPath = new PathString("/Admin/Login")
            });
        }
    }
}
