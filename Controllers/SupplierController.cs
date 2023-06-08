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

public class SupplierController : Controller
{
    private readonly ApplicationDbContext _context;

    public SupplierController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Supplier
    public async Task<IActionResult> Index()
    {
          return _context.SupplierModel != null ? 
                      View(await _context.SupplierModel.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.SupplierModel'  is null.");
    }

    // GET: Supplier/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.SupplierModel == null)
        {
            return NotFound();
        }

        var supplierModel = await _context.SupplierModel.Include(supplier => supplier.Products)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (supplierModel == null)
        {
            return NotFound();
        }

        return View(supplierModel);
    }

    // GET: Supplier/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Supplier/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] SupplierModel supplierModel)
    {
        if (ModelState.IsValid)
        {
            var log = new LogModel()
            {
                Type = "Fornecedor",
                Message = $"Criação: {supplierModel.Name}"
            };

            _context.Add(log);

            _context.Add(supplierModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(supplierModel);
    }

    // GET: Supplier/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.SupplierModel == null)
        {
            return NotFound();
        }

        var supplierModel = await _context.SupplierModel.FindAsync(id);
        if (supplierModel == null)
        {
            return NotFound();
        }
        return View(supplierModel);
    }

    // POST: Supplier/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SupplierModel supplierModel)
    {
        if (id != supplierModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var log = new LogModel()
                {
                    Type = "Fornecedor",
                    Message = $"Edição: {supplierModel.Name}"
                };

                _context.Add(log);

                _context.Update(supplierModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierModelExists(supplierModel.Id))
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
        return View(supplierModel);
    }

    // GET: Supplier/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.SupplierModel == null)
        {
            return NotFound();
        }

        var supplierModel = await _context.SupplierModel
            .FirstOrDefaultAsync(m => m.Id == id);
        if (supplierModel == null)
        {
            return NotFound();
        }

        return View(supplierModel);
    }

    // POST: Supplier/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.SupplierModel == null)
        {
            return Problem("Entity set 'ApplicationDbContext.SupplierModel'  is null.");
        }
        var supplierModel = await _context.SupplierModel.FindAsync(id);
        if (supplierModel != null)
        {
            _context.SupplierModel.Remove(supplierModel);
        }

        var log = new LogModel()
        {
            Type = "Fornecedor",
            Message = $"Remoção: {supplierModel.Name}"
        };

        _context.Add(log);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SupplierModelExists(int id)
    {
      return (_context.SupplierModel?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
