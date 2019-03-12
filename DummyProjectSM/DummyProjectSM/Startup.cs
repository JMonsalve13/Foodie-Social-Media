using DummyProjectSM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DummyProjectSM.Startup))]
namespace DummyProjectSM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com"
                };

                string userPWD = "Testword@3";

                var checkUser = userManager.Create(user, userPWD);

                if (checkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
