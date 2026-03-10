using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Themes.Data;
using Themes.Models;

namespace Themes.Controllers
{
    [Authorize]
    public class NoteListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NoteListsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: NoteLists
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var noteLists = await _context.NoteLists
                .Where(n => n.UserId == userId)
                .ToListAsync();

            return View(noteLists);
        }

        // GET: NoteLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);

            var noteList = await _context.NoteLists
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (noteList == null)
                return NotFound();

            return View(noteList);
        }

        // GET: NoteLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NoteLists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] NoteList noteList)
        {
            var userId = _userManager.GetUserId(User);
            noteList.UserId = userId;

            if (ModelState.IsValid)
            {
                _context.Add(noteList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(noteList);
        }

        // GET: NoteLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);

            var noteList = await _context.NoteLists
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (noteList == null)
                return NotFound();

            return View(noteList);
        }

        // POST: NoteLists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] NoteList noteList)
        {
            if (id != noteList.Id)
                return NotFound();

            var userId = _userManager.GetUserId(User);

            var existingNoteList = await _context.NoteLists
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (existingNoteList == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    existingNoteList.Name = noteList.Name;

                    _context.Update(existingNoteList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteListExists(noteList.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(noteList);
        }

        // GET: NoteLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);

            var noteList = await _context.NoteLists
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (noteList == null)
                return NotFound();

            return View(noteList);
        }

        // POST: NoteLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);

            var noteList = await _context.NoteLists
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (noteList != null)
            {
                _context.NoteLists.Remove(noteList);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NoteListExists(int id)
        {
            return _context.NoteLists.Any(e => e.Id == id);
        }
    }
}