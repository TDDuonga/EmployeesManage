using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesManage.Data;
using EmployeesManage.Models;

namespace EmployeesManage.Controllers
{
    public class EmployesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employes
        public async Task<IActionResult> Index()
        {
            return View(await _context.employes.ToListAsync());
        }

        // GET: Employes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employes = await _context.employes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employes == null)
            {
                return NotFound();
            }

            return View(employes);
        }

        // GET: Employes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employes employes)
        {
            employes.CreateById = "Macor code";
            employes.CreatedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(employes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employes);
        }

        // GET: Employes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employes = await _context.employes.FindAsync(id);
            if (employes == null)
            {
                return NotFound();
            }
            return View(employes);
        }

        // POST: Employes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpNo,FirstName,MiddleName,LastName,Email,PhoneNumber,Address,DateOfBirth,Country,Department,Designation")] Employes employes)
        {
            if (id != employes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployesExists(employes.Id))
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
            return View(employes);
        }

        // GET: Employes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employes = await _context.employes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employes == null)
            {
                return NotFound();
            }

            return View(employes);
        }

        // POST: Employes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employes = await _context.employes.FindAsync(id);
            if (employes != null)
            {
                _context.employes.Remove(employes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployesExists(int id)
        {
            return _context.employes.Any(e => e.Id == id);
        }
    }
}
