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
    public class CustomersController : Controller
    {
        private readonly ProjectContext _context;
        int Cid;
        public CustomersController(ProjectContext context)
        {
            _context = context;
        }  

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Cname,Cpass")] TblCustomer customer)
        {
            var user = _context.TblCustomer.Single(x => x.Cname == customer.Cname);
            if (user != null)
            {
                if (user.Cpass == customer.Cpass)
                {
                    ViewBag.msg = "Valid Credentials";
                    
                    HttpContext.Session.SetInt32("Cid", user.Cid);
                    return RedirectToAction("Dashboard");
                }
                else
                    ViewBag.msg = "Invalid Credentials";


            }
            else
                ViewBag.msg = "Invalid Credentials";

            return View();
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        
        public async Task<IActionResult> GetAllLaptop()
        {
            List<AvailableLaptop> laptop = _context.AvailableLaptop.ToList();

            
            return View(laptop);
        }
        
       
        public IActionResult Orders(int id)
        {
            List<TblOrderedLaptop> order = _context.TblOrderedLaptop.ToList();
            var index = order.Last();
            int Oid = index.Oid+1;
            int customerId = (int)HttpContext.Session.GetInt32("Cid");
            //ViewBag.id = Oid+1;
            //ViewBag.Cid = customerId;
            var temp = _context.AvailableLaptop.Single(x => x.Lid == id);


            HttpContext.Session.SetInt32("CustomerId", customerId);
            HttpContext.Session.SetInt32("Oid", Oid);
            HttpContext.Session.SetInt32("Lid", id);
            HttpContext.Session.SetInt32("SellerId", (int)temp.SellerId);

            return RedirectToRoute(new
            {
                controller = "OrderedLaptops",
               action = "CreateFromCustomer"
           });
        
        }

       

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomer
                .FirstOrDefaultAsync(m => m.Cid == id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return View(tblCustomer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cid,Cname,Cpass,Ccontact,Cemail")] TblCustomer tblCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCustomer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomer.FindAsync(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }
            return View(tblCustomer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cid,Cname,Cpass,Ccontact,Cemail")] TblCustomer tblCustomer)
        {
            if (id != tblCustomer.Cid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCustomerExists(tblCustomer.Cid))
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
            return View(tblCustomer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomer
                .FirstOrDefaultAsync(m => m.Cid == id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return View(tblCustomer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblCustomer = await _context.TblCustomer.FindAsync(id);
            _context.TblCustomer.Remove(tblCustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCustomerExists(int id)
        {
            return _context.TblCustomer.Any(e => e.Cid == id);
        }

        public IActionResult Logout()
        {
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index"
            });
        }
    }
}
