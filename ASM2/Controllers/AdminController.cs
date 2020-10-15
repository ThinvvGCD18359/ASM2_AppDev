using ASM2.Models;
using Microsoft.AspNet.Identity;
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

		public AdminController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: Admin
		[HttpGet]
		public ActionResult Index()
		{
			var users = _context.Users.ToList();
			return View(users);
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
  }
}