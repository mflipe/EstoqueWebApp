using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EstoqueWebApp.Data;
using EstoqueWebApp.Models;

namespace EstoqueWebApp.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Category
    public async Task<IActionResult> Index()
    {
          return _context.CategoryModel != null ? 
                      View(await _context.CategoryModel.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.CategoryModel'  is null.");
    }

    // GET: Category/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.CategoryModel == null)
        {
            return NotFound();
        }

        var categoryModel = await _context.CategoryModel.Include(supplier => supplier.Products)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (categoryModel == null)
        {
            return NotFound();
        }

        return View(categoryModel);
    }

    // GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Category/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description")] CategoryModel categoryModel)
    {
        if (ModelState.IsValid)
        {
            var log = new LogModel()
            {
                Type = "Category",
                Message = $"Criação: {categoryModel.Name}"
            };
            _context.Add(log);

            _context.Add(categoryModel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categoryModel);
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.CategoryModel == null)
        {
            return NotFound();
        }

        var categoryModel = await _context.CategoryModel.FindAsync(id);
        if (categoryModel == null)
        {
            return NotFound();
        }
        return View(categoryModel);
    }

    // POST: Category/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] CategoryModel categoryModel)
    {
        if (id != categoryModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var log = new LogModel()
                {
                    Type = "Category",
                    Message = $"Edição: {categoryModel.Name}"
                };

                _context.Add(log);
                _context.Update(categoryModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryModelExists(categoryModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(categoryModel);
    }

    // GET: Category/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.CategoryModel == null)
        {
            return NotFound();
        }

        var categoryModel = await _context.CategoryModel
            .FirstOrDefaultAsync(m => m.Id == id);
        if (categoryModel == null)
        {
            return NotFound();
        }

        return View(categoryModel);
    }

    // POST: Category/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.CategoryModel == null)
        {
            return Problem("Entity set 'ApplicationDbContext.CategoryModel'  is null.");
        }
        var categoryModel = await _context.CategoryModel.FindAsync(id);
        if (categoryModel != null)
        {
            _context.CategoryModel.Remove(categoryModel);
        }

        var log = new LogModel()
        {
            Type = "Category",
            Message = $"Remoção: {categoryModel.Name}"
        };

        _context.Add(log);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryModelExists(int id)
    {
      return (_context.CategoryModel?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
