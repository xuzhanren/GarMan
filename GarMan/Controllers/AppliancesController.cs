using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarMan.Data;
using GarMan.Models;
using System.Security.Claims;

namespace GarMan.Controllers;

[Authorize]
public class AppliancesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AppliancesController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // GET: Appliances
    public async Task<IActionResult> Index(int? page)
    {
        // Get page size from configuration
        var pageSize = _configuration.GetValue<int>("Paging:AppliancesPageSize", 10);
        var pageNumber = page ?? 1;

        var query = _context.Appliances.AsQueryable();

        // Users with "User" role can only see their own appliances
        if (User.IsInRole("User"))
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            query = query.Where(a => a.UserId == userId);
        }

        // Get total count for pagination
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        // Get paged data
        var appliances = await query
            .OrderByDescending(a => a.DateSubmitted)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Pass paging info to view
        ViewBag.CurrentPage = pageNumber;
        ViewBag.TotalPages = totalPages;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalItems = totalItems;

        return View(appliances);
    }

    // GET: Appliances/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appliance = await _context.Appliances
            .FirstOrDefaultAsync(m => m.Id == id);
        if (appliance == null)
        {
            return NotFound();
        }

        // Users with "User" role can only view their own appliances
        if (User.IsInRole("User"))
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appliance.UserId != userId)
            {
                return Forbid();
            }
        }

        return View(appliance);
    }

    // GET: Appliances/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Appliances/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ApplianceType,Brand,ModelNumber,YearOfManufacture,Condition,Description,Weight,CollectionDate,CollectionAddress,Status,RecyclingStatus,EstimatedValue,RecycledValue,CustomerName,CustomerPhone,CustomerEmail")] Appliance appliance)
    {
        if (ModelState.IsValid)
        {
            appliance.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            appliance.DateSubmitted = DateTime.Now;
            _context.Add(appliance);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Appliance collection request submitted successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(appliance);
    }

    // GET: Appliances/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appliance = await _context.Appliances.FindAsync(id);
        if (appliance == null)
        {
            return NotFound();
        }

        // Users with "User" role can only edit their own appliances
        if (User.IsInRole("User"))
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appliance.UserId != userId)
            {
                return Forbid();
            }
        }

        return View(appliance);
    }

    // POST: Appliances/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ApplianceType,Brand,ModelNumber,YearOfManufacture,Condition,Description,Weight,CollectionDate,CollectionAddress,Status,RecyclingStatus,EstimatedValue,RecycledValue,DateSubmitted,UserId,CustomerName,CustomerPhone,CustomerEmail")] Appliance appliance)
    {
        if (id != appliance.Id)
        {
            return NotFound();
        }

        // Users with "User" role can only edit their own appliances
        if (User.IsInRole("User"))
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appliance.UserId != userId)
            {
                return Forbid();
            }
        }

        if (ModelState.IsValid)
        {
            try
            {
                appliance.LastUpdated = DateTime.Now;
                _context.Update(appliance);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Appliance updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplianceExists(appliance.Id))
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
        return View(appliance);
    }

    // GET: Appliances/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appliance = await _context.Appliances
            .FirstOrDefaultAsync(m => m.Id == id);
        if (appliance == null)
        {
            return NotFound();
        }

        // Users with "User" role can only delete their own appliances
        if (User.IsInRole("User"))
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appliance.UserId != userId)
            {
                return Forbid();
            }
        }

        return View(appliance);
    }

    // POST: Appliances/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var appliance = await _context.Appliances.FindAsync(id);
        if (appliance != null)
        {
            // Users with "User" role can only delete their own appliances
            if (User.IsInRole("User"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (appliance.UserId != userId)
                {
                    return Forbid();
                }
            }

            _context.Appliances.Remove(appliance);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Appliance deleted successfully!";
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ApplianceExists(int id)
    {
        return _context.Appliances.Any(e => e.Id == id);
    }
}
