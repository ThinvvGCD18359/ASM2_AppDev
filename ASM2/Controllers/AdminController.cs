using ASM2.Models;
using ASM2.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASM2.Controllers
{
	public class AdminController : Controller
	{
		private ApplicationDbContext _context;
    private ApplicationUserManager _userManager;
    private ApplicationSignInManager _signInManager;

    public ApplicationSignInManager SignInManager
    {
      get
      {
        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
      }
      private set
      {
        _signInManager = value;
      }
    }
    public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
		public AdminController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: Admin
		[HttpGet]
		public ActionResult Index()
		{
      var userInfor = (from user in _context.Users
                       select new
                       {
                         UserId = user.Id,
                         Username = user.UserName,
                         EmailAddress = user.Email,
                         RoleName = (from userRole in user.Roles
                                     join role in _context.Roles
                                     on userRole.RoleId
                                     equals role.Id
                                     select role.Name).ToList()
                       }
                       ).ToList().Select(p => new UserRoleViewModel()
                                    {
                                     UserId = p.UserId,
                                     Username = p.Username,
                                     Email = p.EmailAddress,
                                     Role = string.Join(",", p.RoleName)
                                    }
                       );
      return View(userInfor);
		}
    //DELETE ACCOUNT
    [HttpGet]
    public ActionResult Delete(string id)
    {
      var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == id);

      if (AccountInDB == null)
      {
        return HttpNotFound();
      }

      _context.Users.Remove(AccountInDB);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }

    //Edit 
    [HttpGet]
    public ActionResult Edit(string id)
    {
      var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == id);
      if (AccountInDB == null)
      {
        return HttpNotFound();
      }
      return View(AccountInDB);
    }

    //EDIT
    [HttpPost]
    public ActionResult Edit(ApplicationUser user)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == user.Id);

      if (AccountInDB == null)
      {
        return HttpNotFound();
      }

      AccountInDB.UserName = user.UserName;
      _context.SaveChanges();

      return RedirectToAction("Index");
    }

    public async Task<ActionResult> ChangePassword(ChangePasswordViewModel usermodel, ApplicationUser _user)
    {
      ApplicationUser user = await UserManager.FindByIdAsync(_user.Id);
      if (user == null)
      {
        return HttpNotFound();
      }
      user.PasswordHash = UserManager.PasswordHasher.HashPassword(usermodel.NewPassword);
      var result = await UserManager.UpdateAsync(user);
      if (!result.Succeeded)
      {
        //throw exception......
      }
      return RedirectToAction("Index");
    }
  }
}