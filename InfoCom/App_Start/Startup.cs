using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using Owin;

namespace InfoCom.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "InfoComCookie",
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}