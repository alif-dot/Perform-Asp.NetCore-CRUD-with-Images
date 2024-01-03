using CRUDwithImage.Data;
using CRUDwithImage.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDwithImage.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ProductContext _context;
        public CategoryController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoriesList = _context.Category;
            return View(categoriesList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                _context.Category.Add(category);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") added Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? categoryId)
        {
            var category = _context.Category.Find(categoryId);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Category.Update(category);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") Update Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? categoryId)
        {
            var category = _context.Category.Find(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? categoryId)
        {
            var category = _context.Category.Find(categoryId);
            if( category == null)
            {
                return NotFound();
            }
            _context.Category.Remove(category);
            _context.SaveChanges();
            TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") delete successfully.";
            return RedirectToAction("Index");
        }
    }
}
