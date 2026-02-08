using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarMan.Data;

namespace GarMan.Controllers;

public class RecyclingCentersController : Controller
{
    private readonly ApplicationDbContext _context;

    public RecyclingCentersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: RecyclingCenters
    public async Task<IActionResult> Index()
    {
        var centers = await _context.RecyclingCenters
            .Where(c => c.IsActive)
            .OrderBy(c => c.CenterName)
            .ToListAsync();
        return View(centers);
    }

    // GET: RecyclingCenters/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recyclingCenter = await _context.RecyclingCenters
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recyclingCenter == null)
        {
            return NotFound();
        }

        return View(recyclingCenter);
    }
}
