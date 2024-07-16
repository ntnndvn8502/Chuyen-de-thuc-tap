using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;
using Simple.Identity;

[assembly: OwinStartup(typeof(Simple.Startup))]

namespace Simple
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            this.CreateRolesAndUser();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
        public void CreateRolesAndUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AppDbContext()));
            var appDbContext = new AppDbContext();
            var appUserStore = new AppUserStore(appDbContext);
            var appUserManager = new AppUserManager(appUserStore);

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (appUserManager.FindByName("Admin") == null)
            {
                var user = new AppUser();
                user.UserName = "nghia123";
                user.Email = "tnghia8502@gmail.com";
                string Password = "852002Tnghia";
                var WUser = appUserManager.Create(user, Password);
                if (WUser.Succeeded)
                {
                    appUserManager.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
            if (appUserManager.FindByName("User") == null)
            {
                var user = new AppUser();
                user.UserName = "user123";
                user.Email = "user123@gmail.com";
                string Password = "user123";
                var WUser = appUserManager.Create(user, Password);
                if (WUser.Succeeded)
                {
                    appUserManager.AddToRole(user.Id, "User");
                }
            }
            if (!roleManager.RoleExists("Restaurant"))
            {
                var role = new IdentityRole();
                role.Name = "Restaurant";
                roleManager.Create(role);
            }
            if (appUserManager.FindByName("Restaurant") == null)
            {
                var user = new AppUser();
                user.UserName = "restaurant123";
                user.Email = "rtr123@gmail.com";
                string Password = "rtr123";
                var WUser = appUserManager.Create(user, Password);
                if (WUser.Succeeded)
                {
                    appUserManager.AddToRole(user.Id, "Restaurant");
                }
            }
        }
    }
}
