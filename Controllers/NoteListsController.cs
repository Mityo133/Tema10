using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Themes.Data;
using Themes.Models;

namespace Themes.Controllers
{
    public class NoteListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoteListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NoteLists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NoteLists.Include(n => n.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NoteLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteList = await _context.NoteLists
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noteList == null)
            {
                return NotFound();
            }

            return View(noteList);
        }

        // GET: NoteLists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "LastName");
            return View();
        }

        // POST: NoteLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserId")] NoteList noteList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noteList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "LastName", noteList.UserId);
            return View(noteList);
        }

        // GET: NoteLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteList = await _context.NoteLists.FindAsync(id);
            if (noteList == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "LastName", noteList.UserId);
            return View(noteList);
        }

        // POST: NoteLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId")] NoteList noteList)
        {
            if (id != noteList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noteList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteListExists(noteList.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "LastName", noteList.UserId);
            return View(noteList);
        }

        // GET: NoteLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteList = await _context.NoteLists
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noteList == null)
            {
                return NotFound();
            }

            return View(noteList);
        }

        // POST: NoteLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noteList = await _context.NoteLists.FindAsync(id);
            if (noteList != null)
            {
                _context.NoteLists.Remove(noteList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteListExists(int id)
        {
            return _context.NoteLists.Any(e => e.Id == id);
        }
    }
}
