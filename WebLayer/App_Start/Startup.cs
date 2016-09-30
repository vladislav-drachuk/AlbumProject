using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using AlbumProject.BusinessLogicLayer.Servises;
using Microsoft.AspNet.Identity;
using AlbumProject.BusinessLogicLayer.Interfaces;



[assembly: OwinStartup(typeof(WebLayer.App_Start.Startup))]

namespace WebLayer.App_Start
{

    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection"); 
        }
    }
}