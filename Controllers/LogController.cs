using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EstoqueWebApp.Data;
using EstoqueWebApp.Models;

namespace EstoqueWebApp.Controllers
{
    public class LogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Log
        public async Task<IActionResult> Index()
        {
              return _context.LogModel != null ? 
                          View(await _context.LogModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LogModel'  is null.");
        }

        // GET: Log/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LogModel == null)
            {
                return NotFound();
            }

            var logModel = await _context.LogModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logModel == null)
            {
                return NotFound();
            }

            return View(logModel);
        }

        // GET: Log/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Log/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Message,CreatedDate")] LogModel logModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logModel);
        }

        // GET: Log/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LogModel == null)
            {
                return NotFound();
            }

            var logModel = await _context.LogModel.FindAsync(id);
            if (logModel == null)
            {
                return NotFound();
            }
            return View(logModel);
        }

        // POST: Log/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Message,CreatedDate")] LogModel logModel)
        {
            if (id != logModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogModelExists(logModel.Id))
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
            return View(logModel);
        }

        // GET: Log/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LogModel == null)
            {
                return NotFound();
            }

            var logModel = await _context.LogModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logModel == null)
            {
                return NotFound();
            }

            return View(logModel);
        }

        // POST: Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LogModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LogModel'  is null.");
            }
            var logModel = await _context.LogModel.FindAsync(id);
            if (logModel != null)
            {
                _context.LogModel.Remove(logModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogModelExists(int id)
        {
          return (_context.LogModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
