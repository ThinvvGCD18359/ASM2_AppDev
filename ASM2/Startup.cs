﻿using ASM2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASM2.Startup))]
namespace ASM2
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
      createRolesandUsers();
    }
    // In this method we will create default User roles and Admin user for login
    private void createRolesandUsers()
    {
      ApplicationDbContext context = new ApplicationDbContext();

      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
      var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

      if (!roleManager.RoleExists("Admin"))
      {

        // first we create Admin rool    
        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        role.Name = "Admin";
        roleManager.Create(role);

        //Here we create a Admin super user who will maintain the website                   

        var user = new ApplicationUser();
        user.UserName = "Admin";
        user.Email = "Admin@gmail.com";

        string userPWD = "Abc@123";

        var chkUser = UserManager.Create(user, userPWD);

        //Add default User to Role Admin    
        if (chkUser.Succeeded)
        {
          var result1 = UserManager.AddToRole(user.Id, "Admin");
        }
      }
      // Creating Training Staff role     
      if (!roleManager.RoleExists("Training Staff"))
      {
        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        role.Name = "Training Staff";
        roleManager.Create(role);

      }

      // Creating Trainer role     
      if (!roleManager.RoleExists("Trainer"))
      {
        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        role.Name = "Trainer";
        roleManager.Create(role);

      }

      // Creating Trainee role
      if (!roleManager.RoleExists("Trainee"))
      {
        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        role.Name = "Trainee";
        roleManager.Create(role);

      }
    }
  }
}
