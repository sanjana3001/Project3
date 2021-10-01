using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Controllers
{
    public class AvailableLaptopsController : Controller
    {
        private readonly ProjectContext _context;

        public AvailableLaptopsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: AvailableLaptops
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.AvailableLaptop.Include(a => a.Seller);
            return View(await projectContext.ToListAsync());
        }

        // GET: AvailableLaptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableLaptop = await _context.AvailableLaptop
                .Include(a => a.Seller)
                .FirstOrDefaultAsync(m => m.Lid == id);
            if (availableLaptop == null)
            {
                return NotFound();
            }

            return View(availableLaptop);
        }

        // GET: AvailableLaptops/Create
        public IActionResult Create()
        {
            ViewData["SellerId"] = new SelectList(_context.TblSeller, "Sid", "Sid");
            return View();
        }

        // POST: AvailableLaptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Lid,SellerId,Lname,Lmodel,Lprice")] AvailableLaptop availableLaptop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availableLaptop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SellerId"] = new SelectList(_context.TblSeller, "Sid", "Sid", availableLaptop.SellerId);
            return View(availableLaptop);
        }

        // GET: AvailableLaptops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableLaptop = await _context.AvailableLaptop.FindAsync(id);
            if (availableLaptop == null)
            {
                return NotFound();
            }
            ViewData["SellerId"] = new SelectList(_context.TblSeller, "Sid", "Sid", availableLaptop.SellerId);
            return View(availableLaptop);
        }

        // POST: AvailableLaptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Lid,SellerId,Lname,Lmodel,Lprice")] AvailableLaptop availableLaptop)
        {
            if (id != availableLaptop.Lid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availableLaptop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailableLaptopExists(availableLaptop.Lid))
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
            ViewData["SellerId"] = new SelectList(_context.TblSeller, "Sid", "Sid", availableLaptop.SellerId);
            return View(availableLaptop);
        }

        // GET: AvailableLaptops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableLaptop = await _context.AvailableLaptop
                .Include(a => a.Seller)
                .FirstOrDefaultAsync(m => m.Lid == id);
            if (availableLaptop == null)
            {
                return NotFound();
            }

            return View(availableLaptop);
        }

        // POST: AvailableLaptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availableLaptop = await _context.AvailableLaptop.FindAsync(id);
            _context.AvailableLaptop.Remove(availableLaptop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailableLaptopExists(int id)
        {
            return _context.AvailableLaptop.Any(e => e.Lid == id);
        }
    }
}
