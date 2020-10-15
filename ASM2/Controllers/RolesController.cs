using ASM2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASM2.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext _context;

		    public RolesController()
		    {
			      _context = new ApplicationDbContext();
		    }
        // GET: Roles
        public ActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }
    }
}