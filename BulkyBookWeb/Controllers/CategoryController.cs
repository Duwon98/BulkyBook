using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // var objCategoryList = _db.Categories.ToList();
            IEnumerable < Category > objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
           
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
            // It will automatically find the same Id property and update them with given new values. which is pretty cool!
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(categoryFromDb);
            TempData["success"] = "Category Deleted successfully";
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
