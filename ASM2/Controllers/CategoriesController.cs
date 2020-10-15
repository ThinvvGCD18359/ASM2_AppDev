using ASM2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASM2.Controllers
{
	public class CategoriesController : Controller
	{
	private ApplicationDbContext _context;

		public CategoriesController()
		{
		_context = new ApplicationDbContext();
		}
		// GET: Categories
		public ActionResult Index(string searchString)
		{
			var categories = _context.Categories.ToList();
			if (!String.IsNullOrEmpty(searchString))
			{
				categories = categories.FindAll(s => s.Name.Contains(searchString));
			}
			return View(categories);
		}

		// Create: Categories
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			if (_context.Categories.Any(c => c.Name.Contains(category.Name)))
			{
				ModelState.AddModelError("Name", "Category Name Already Exists.");
				return View();
			}
			var newCategory = new Category
			{
				Name = category.Name,
				Description = category.Description
			};
			_context.Categories.Add(newCategory);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
			if (categoryInDb == null)
			{
				return HttpNotFound();
			}
			_context.Categories.Remove(categoryInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
			if (categoryInDb == null)
			{
				return HttpNotFound();
			}
			return View(categoryInDb);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == category.Id);
			if (categoryInDb == null)
			{
				return HttpNotFound();
			}
			categoryInDb.Name = category.Name;
			categoryInDb.Description = category.Description;
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}