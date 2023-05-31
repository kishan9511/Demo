using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controler
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DemoProjectContext _db;
        public CityController(DemoProjectContext db) 
        
        {
        
            _db= db; 
        }

        public async Task<IActionResult> GetstateDropdown(int countryid)
        {
            var st = await _db.States.Where(s => s.IsDeleted == false && s.CountryId == countryid).Select(s => new
            {
                ID = s.Id,
                fullname = s.FullName

            }).ToListAsync();

            return new JsonResult(st);
        }

    }
}
