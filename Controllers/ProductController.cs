using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EstoqueWebApp.Data;
using EstoqueWebApp.Models;
using EstoqueWebApp.ViewModels;

namespace EstoqueWebApp.Controllers;
public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Product
    public async Task<IActionResult> Index()
    {
        if (_context.ProductModel is null) 
            return Problem("Entity set 'ApplicationDbContext.ProductModel'  is null.");

        var productModel = await _context.ProductModel.ToListAsync();
        return View(productModel);
    }

    // GET: Product/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.ProductModel == null)
        {
            return NotFound();
        }

        var productModel = await _context.ProductModel
            .FirstOrDefaultAsync(m => m.Id == id);
        if (productModel == null)
        {
            return NotFound();
        }

        return View(productModel);
    }

    // GET: Product/Create
    public IActionResult Create()
    {
        var categories = _context.CategoryModel.ToList();
        var suppliers = _context.SupplierModel.ToList();
        var viewModel = new ProductCreateViewModel(suppliers, categories);
        return View(viewModel);
    }

    // POST: Product/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Quantity,Price,CategoryId,SupplierId")] ProductCreateViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            var productModel = new ProductModel() 
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                Quantity = productViewModel.Quantity,
                Category = _context.CategoryModel.FirstOrDefault(o => o.Id == productViewModel.CategoryId),
                Supplier = _context.SupplierModel.FirstOrDefault(o => o.Id == productViewModel.SupplierId)
            };

            var log = new LogModel()
            {
                Type = "Produto",
                Message = $"Criação: {productModel.Name}"
            };

            _context.Add(log);

            _context.Add(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(productViewModel);
    }

    // GET: Product/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.ProductModel == null)
        {
            return NotFound();
        }

        var productModel = await _context.ProductModel.FindAsync(id);
        if (productModel == null)
        {
            return NotFound();
        }

        var categories = _context.CategoryModel.ToList();
        var suppliers = _context.SupplierModel.ToList();
        var viewModel = new ProductCreateViewModel(suppliers, categories)
        {
            CategoryId = productModel.Category.Id,
            SupplierId = productModel.Category.Id,
            Description = productModel.Description,
            Name = productModel.Name,
            Price = productModel.Price,
            Quantity = productModel.Quantity
        };
        return View(viewModel);
    }

    // POST: Product/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Quantity,Price,CategoryId,SupplierId")] ProductCreateViewModel productViewModel)
    {
        if (id != productViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var productModel = new ProductModel()
                {
                    Id = productViewModel.Id,
                    Name = productViewModel.Name,
                    Description = productViewModel.Description,
                    Price = productViewModel.Price,
                    Quantity = productViewModel.Quantity,
                    Category = _context.CategoryModel.FirstOrDefault(o => o.Id == productViewModel.CategoryId),
                    Supplier = _context.SupplierModel.FirstOrDefault(o => o.Id == productViewModel.SupplierId)
                };

                var log = new LogModel()
                {
                    Type = "Produto",
                    Message = $"Edição: {productModel.Name}"
                };

                _context.Add(log);
                _context.Update(productModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(productViewModel.Id))
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
        return View(productViewModel);
    }

    // GET: Product/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.ProductModel == null)
        {
            return NotFound();
        }

        var productModel = await _context.ProductModel
            .FirstOrDefaultAsync(m => m.Id == id);
        if (productModel == null)
        {
            return NotFound();
        }

        return View(productModel);
    }

    // POST: Product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.ProductModel == null)
        {
            return Problem("Entity set 'ApplicationDbContext.ProductModel'  is null.");
        }
        var productModel = await _context.ProductModel.FindAsync(id);
        if (productModel != null)
        {
            _context.ProductModel.Remove(productModel);
        }

        var log = new LogModel()
        {
            Type = "Produto",
            Message = $"Remoção: {productModel.Name}"
        };

        _context.Add(log);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductModelExists(int id)
    {
      return (_context.ProductModel?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
