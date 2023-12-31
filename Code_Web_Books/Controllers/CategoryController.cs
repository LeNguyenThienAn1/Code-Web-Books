﻿using Code_Web_Books.Data;
using Code_Web_Books.Models;
using Microsoft.AspNetCore.Mvc;


namespace Code_Web_Books.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }
        public IActionResult Index()
        {
            List<Category> categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(Category category)
        {
            if (category.Name == category.Description)
            {
                ModelState.AddModelError("Name", "Name can not be equal to Description");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index");
            }

            return View();

        }
        public IActionResult Edit(int? idb)
        {
            if (idb == null || idb == 0)
            {
                return NotFound();
            }
            Category? category = _dbContext.Categories.Find(idb);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category updated succesfully";
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Delete(int? idb)
        {
            if (idb == null || idb == 0)
            {
                return NotFound();
            }
            Category? category = _dbContext.Categories.Find(idb);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
