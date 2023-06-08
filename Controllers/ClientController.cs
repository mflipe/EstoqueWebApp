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

public class ClientController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClientController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Client
    public async Task<IActionResult> Index()
    {
          return _context.ClientModel != null ? 
                      View(await _context.ClientModel.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.ClientModel'  is null.");
    }

    // GET: Client/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.ClientModel == null)
        {
            return NotFound();
        }

        var clientModel = await _context.ClientModel
            .FirstOrDefaultAsync(m => m.Id == id);
        if (clientModel == null)
        {
            return NotFound();
        }

        return View(clientModel);
    }

    // GET: Client/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Client/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] ClientModel clientModel)
    {
        if (ModelState.IsValid)
        {
            var log = new LogModel()
            {
                Type = "Cliente",
                Message = $"Criação: {clientModel.Name}"
            };

            _context.Add(log);

            _context.Add(clientModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(clientModel);
    }

    // GET: Client/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.ClientModel == null)
        {
            return NotFound();
        }

        var clientModel = await _context.ClientModel.FindAsync(id);
        if (clientModel == null)
        {
            return NotFound();
        }
        return View(clientModel);
    }

    // POST: Client/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ClientModel clientModel)
    {
        if (id != clientModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var log = new LogModel()
                {
                    Type = "Cliente",
                    Message = $"Edição: {clientModel.Name}"
                };
                _context.Add(log);

                _context.Update(clientModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientModelExists(clientModel.Id))
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
        return View(clientModel);
    }

    // GET: Client/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.ClientModel == null)
        {
            return NotFound();
        }

        var clientModel = await _context.ClientModel
            .FirstOrDefaultAsync(m => m.Id == id);
        if (clientModel == null)
        {
            return NotFound();
        }

        return View(clientModel);
    }

    // POST: Client/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.ClientModel == null)
        {
            return Problem("Entity set 'ApplicationDbContext.ClientModel'  is null.");
        }
        var clientModel = await _context.ClientModel.FindAsync(id);
        if (clientModel != null)
        {
            _context.ClientModel.Remove(clientModel);
        }

        var log = new LogModel()
        {
            Type = "Cliente",
            Message = $"Remoção: {clientModel.Name}"
        };
        _context.Add(log);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClientModelExists(int id)
    {
      return (_context.ClientModel?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
