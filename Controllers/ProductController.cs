using CRUDwithImage.Data;
using CRUDwithImage.Models;
using CRUDwithImage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDwithImage.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ProductListVM> productListVMList = new List<ProductListVM>();
            var productList = _context.Product;
            if(productList != null)
            {
                foreach(var item in productList)
                {
                    ProductListVM productListVM = new ProductListVM();
                    productListVM.Id = item.Id;
                    productListVM.Name = item.Name;
                    productListVM.Description = item.Description;
                    productListVM.Price = item.Price;                    
                    productListVM.CategoryId = item.CategoryId;
                    productListVM.Image = item.Image;
                    productListVM.CategoryName = _context.Category.Where(c=>c.CategoryId == item.CategoryId).Select(c=>c.CategoryName).FirstOrDefault();
                    productListVMList.Add(productListVM);
                }
            }
            return View(productListVMList);
        }

        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM();
            productVM.Category = (IEnumerable<SelectListItem>)_context.Category.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
            productVM.Category = (IEnumerable<SelectListItem>)_context.Category.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });
            var product = new Product()
            {
                Name = productVM.Name,
                Description = productVM.Description,
                Price = productVM.Price,
                Color = productVM.Color,
                CategoryId = productVM.CategoryId,
                Image = productVM.Image
            };
            ModelState.Remove("Category");
            if(ModelState.IsValid)
            {
                _context.Product.Add(product);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Product (" + product.Name + ") added successfuly.";
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        public IActionResult Edit(int? id)
        {
            var product = _context.Product.Find(id);
            if (product != null)
            {
                var productVM = new ProductVM()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    Color = product.Color,
                    Image = product.Image,
                    Category = (IEnumerable<SelectListItem>)_context.Category.Select(c => new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    })
                };
                return View(productVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM productViewModel)
        {
            productViewModel.Category = (IEnumerable<SelectListItem>)_context.Category.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });
            var product = new Product()
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                Color = productViewModel.Color,
                CategoryId = productViewModel.CategoryId,
                Image = productViewModel.Image
            };
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                _context.Product.Update(product);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Product (" + product.Name + ") updated successfully !";
                return RedirectToAction("Index");
            }

            return View(productViewModel);
        }
        public IActionResult Delete(int? id)
        {
            var productToEdit = _context.Product.Find(id);
            if (productToEdit != null)
            {
                var productViewModel = new ProductVM()
                {
                    Id = productToEdit.Id,
                    Name = productToEdit.Name,
                    Description = productToEdit.Description,
                    Price = productToEdit.Price,
                    CategoryId = productToEdit.CategoryId,
                    Color = productToEdit.Color,
                    Image = productToEdit.Image,
                    Category = (IEnumerable<SelectListItem>)_context.Category.Select(c => new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    })
                };
                return View(productViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Product.Remove(product);
            _context.SaveChanges();
            TempData["SuccessMsg"] = "Product (" + product.Name + ") deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
