using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controler;

[Route("api/[controller]/[action]")]
[ApiController]
public class UIController : ControllerBase
{
    private readonly DemoProjectContext _db;

    public UIController(DemoProjectContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductSubcategories(int categoryId)
    {
        var subcategories = await _db.SubCategories.Where(s => s.Isdeleted == false && s.ProductCategoryId == categoryId).ToListAsync();

        return new JsonResult(subcategories);
    }
}
