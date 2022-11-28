using BasicMvcWithGenericRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using BasicMvcWithGenericRepository.Models;

namespace BasicMvcWithGenericRepository.Controllers;

public class CategoryController : Controller
{
    private readonly IUnitOfWork<Category> uow;

    public CategoryController(IUnitOfWork<Category> uow)
    {
        this.uow = uow;
    }

    public IActionResult Index()
    {
        var categoryList = uow.GetGenericRepository.Get();
        return View(categoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            uow.GetGenericRepository.Insert(category);
            uow.Save();
            TempData["success"] = "category created successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "please enter correct values";
        return View(category);
    }

    public IActionResult Update(int? id)
    {
        if (id is null || id is 0)
        {
            return NotFound();
        }

        var category = uow.GetGenericRepository.Get(x => x.Id == id).FirstOrDefault();

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Category category)
    {
        if (ModelState.IsValid)
        {
            uow.GetGenericRepository.Update(category);
            uow.Save();
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Get(int? id)
    {
        if (id is null || id is 0)
        {
            return NotFound();
        }

        var category = uow.GetGenericRepository.Get(x => x.Id == id).FirstOrDefault();

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category category)
    {
        uow.GetGenericRepository.Delete(category);
        uow.Save();

        return RedirectToAction("Index");
    }
}
