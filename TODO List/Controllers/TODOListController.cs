using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TODO_List.Data;
using TODO_List.Models;

namespace TODO_List.Controllers
{
    public class TODOListController : Controller
    {
        private readonly TODOListContext _context;

        public TODOListController(TODOListContext context)
        {
            _context = context;
        }

        // GET: TODOList
        public async Task<IActionResult> Index()
        {
              return View(await _context.TODOList.ToListAsync());
        }

        // GET: TODOList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TODOList == null)
            {
                return NotFound();
            }

            var tODOList = await _context.TODOList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tODOList == null)
            {
                return NotFound();
            }

            return View(tODOList);
        }

        // GET: TODOList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TODOList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Descripation,Createddate")] TODOList tODOList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tODOList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tODOList);
        }

        // GET: TODOList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TODOList == null)
            {
                return NotFound();
            }

            var tODOList = await _context.TODOList.FindAsync(id);
            if (tODOList == null)
            {
                return NotFound();
            }
            return View(tODOList);
        }

        // POST: TODOList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,Descripation,Createddate")] TODOList tODOList)
        {
            if (id != tODOList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tODOList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TODOListExists(tODOList.Id))
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
            return View(tODOList);
        }

        // GET: TODOList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TODOList == null)
            {
                return NotFound();
            }

            var tODOList = await _context.TODOList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tODOList == null)
            {
                return NotFound();
            }

            return View(tODOList);
        }

        // POST: TODOList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TODOList == null)
            {
                return Problem("Entity set 'TODOListContext.TODOList'  is null.");
            }
            var tODOList = await _context.TODOList.FindAsync(id);
            if (tODOList != null)
            {
                _context.TODOList.Remove(tODOList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TODOListExists(int id)
        {
          return _context.TODOList.Any(e => e.Id == id);
        }
    }
}
