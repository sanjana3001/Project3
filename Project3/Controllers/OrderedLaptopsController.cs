using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Controllers
{
    public class OrderedLaptopsController : Controller
    {
        private readonly ProjectContext _context;

        public OrderedLaptopsController(ProjectContext context)
        {
            _context = context;
        }
       
        // GET: OrderedLaptops
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.TblOrderedLaptop.Include(t => t.Customer).Include(t => t.L).Include(t => t.Seller);
            return View(await projectContext.ToListAsync());
        }

        // GET: OrderedLaptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var order = _context.TblOrderedLaptop.Single(x => x.Oid == id);
            ViewBag.laptop = _context.AvailableLaptop.Single(x => x.Lid == order.Lid);
            ViewBag.seller = _context.TblSeller.Single(x => x.Sid == order.SellerId);
            ViewBag.customer = _context.TblCustomer.Single(x => x.Cid == order.CustomerId);
            if (id == null)
            {
                return NotFound();
            }

            var tblOrderedLaptop = await _context.TblOrderedLaptop
                .Include(t => t.Customer)
                .Include(t => t.L)
                .Include(t => t.Seller)
                .FirstOrDefaultAsync(m => m.Oid == id);
            if (tblOrderedLaptop == null)
            {
                return NotFound();
            }

            return View(tblOrderedLaptop);
        }

        // GET: OrderedLaptops/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.TblCustomer, "Cid", "Cid");
            ViewData["Lid"] = new SelectList(_context.AvailableLaptop, "Lid", "Lid");
            ViewData["SellerId"] = new SelectList(_context.TblSeller, "Sid", "Sid");
            return View();
        }

        // POST: OrderedLaptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Oid,Lid,SellerId,CustomerId")] TblOrderedLaptop tblOrderedLaptop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblOrderedLaptop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.TblCustomer, "Cid", "Cid", tblOrderedLaptop.CustomerId);
            ViewData["Lid"] = new SelectList(_context.AvailableLaptop, "Lid", "Lid", tblOrderedLaptop.Lid);
            ViewData["SellerId"] = new SelectList(_context.TblSeller, "Sid", "Sid", tblOrderedLaptop.SellerId);
            return View(tblOrderedLaptop);
        }

        public IActionResult CreateFromCustomer()
        {
            int Cid= (int)HttpContext.Session.GetInt32("Cid");
            Console.WriteLine(Cid);
            int Lid= (int)HttpContext.Session.GetInt32("Lid");
            int SellerId= (int)HttpContext.Session.GetInt32("SellerId");
            int Oid= (int)HttpContext.Session.GetInt32("Oid");
            ViewData["CustomerId"] = Cid;
            ViewData["Lid"] = Lid;
            ViewData["SellerId"] = SellerId;
            ViewData["Oid"] = Oid;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromCustomer([Bind("Oid,Lid,SellerId,CustomerId")] TblOrderedLaptop tblOrderedLaptop)
        {

            if (ModelState.IsValid)
            {
                _context.Add(tblOrderedLaptop);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new
                {
                    Controller = "Customers",
                    action = "Dashboard"

                });
            }
            ViewData["CustomerId"] = new SelectList(_context.TblCustomer, "Cid", "Cid", tblOrderedLaptop.CustomerId);
            ViewData["Lid"] = new SelectList(_context.AvailableLaptop, "Lid", "Lid", tblOrderedLaptop.Lid);
            ViewData["SellerId"] = new SelectList(_context.TblSeller, "Sid", "Sid", tblOrderedLaptop.SellerId);
            return RedirectToRoute(new
           {
               Controller = "Customers",
               action = "Dashboard"

               });
           
        }

        // GET: OrderedLaptops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrderedLaptop = await _context.TblOrderedLaptop.FindAsync(id);
            if (tblOrderedLaptop == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.TblCustomer, "Cid", "Cid", tblOrderedLaptop.CustomerId);
            ViewData["Lid"] = new SelectList(_context.AvailableLaptop, "Lid", "Lid", tblOrderedLaptop.Lid);
            ViewData["SellerId"] = new SelectList(_context.TblSeller, "Sid", "Sid", tblOrderedLaptop.SellerId);
            return View(tblOrderedLaptop);
        }

        // POST: OrderedLaptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Oid,Lid,SellerId,CustomerId")] TblOrderedLaptop tblOrderedLaptop)
        {
            if (id != tblOrderedLaptop.Oid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblOrderedLaptop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOrderedLaptopExists(tblOrderedLaptop.Oid))
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
            ViewData["CustomerId"] = new SelectList(_context.TblCustomer, "Cid", "Cid", tblOrderedLaptop.CustomerId);
            ViewData["Lid"] = new SelectList(_context.AvailableLaptop, "Lid", "Lid", tblOrderedLaptop.Lid);
            ViewData["SellerId"] = new SelectList(_context.TblSeller, "Sid", "Sid", tblOrderedLaptop.SellerId);
            return View(tblOrderedLaptop);
        }

        // GET: OrderedLaptops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrderedLaptop = await _context.TblOrderedLaptop
                .Include(t => t.Customer)
                .Include(t => t.L)
                .Include(t => t.Seller)
                .FirstOrDefaultAsync(m => m.Oid == id);
            if (tblOrderedLaptop == null)
            {
                return NotFound();
            }

            return View(tblOrderedLaptop);
        }

        // POST: OrderedLaptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblOrderedLaptop = await _context.TblOrderedLaptop.FindAsync(id);
            _context.TblOrderedLaptop.Remove(tblOrderedLaptop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOrderedLaptopExists(int id)
        {
            return _context.TblOrderedLaptop.Any(e => e.Oid == id);
        }
    }
}
