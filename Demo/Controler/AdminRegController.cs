using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace Demo.Controler
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminRegController : ControllerBase
    {
        private readonly DemoProjectContext _db;
        public AdminRegController(DemoProjectContext db)
        {
            _db = db;
        }
        //-------------------------state dropdown with respect to country---------------------------
        public async Task<IActionResult> GetStateDropdown(int countryID)
        {
            var state = await _db.States.Where(s => s.IsDeleted == false && s.CountryId == countryID).Select(s => new

            {
                Id = s.Id,
                Fullname = s.FullName
            }).ToListAsync();

            return new JsonResult(state);
        }

        //-------------------------city dropdown with respect to state ---------------------------

        public async Task<IActionResult> GetCityDropdown(int StateID)
        {
            var city = await _db.Cities.Where(c => c.IsDeleted == false && c.StateId == StateID).Select(c => new
            {
                id = c.Id,
                fullname = c.FullName
            }).ToListAsync();
            return new JsonResult(city);
        }

    }
}
