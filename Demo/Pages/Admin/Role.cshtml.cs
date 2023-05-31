using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class RoleModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public RoleModel(DemoProjectContext db)
        {
            _db = db;
        }

        public IEnumerable<Role> datalist { get; set; }

        [BindProperty]
        public Role roleModel { get; set; }

        public IActionResult OnGet(int? id)
        {
            datalist = _db.Roles.Where(r => r.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var ro = _db.Roles.Where(r => r.RoleId == id && r.Isdeleted == false).FirstOrDefault();

                if (ro == null)
                {
                    return NotFound();
                }
                roleModel = ro;
                return Page();
            }
            roleModel = new Role();
            return Page();
        }

        public IActionResult OnPost()

        {
            if (roleModel.RoleId > 0)
            {
                var ro = _db.Roles.AsNoTracking().Where(r => r.RoleId == roleModel.RoleId && r.Isdeleted == false).FirstOrDefault();
                if (ro == null)
                {
                    return NotFound();
                }
                roleModel.Isdeleted = ro.Isdeleted;

                _db.Roles.Update(roleModel);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }
            else
            {
                try
                {
                    roleModel.Isdeleted = false;
                    _db.Roles.Add(roleModel);
                    _db.SaveChanges();
                    TempData["success"] = "data saved";
                }
                catch (Exception ex) { }
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var ro = _db.Roles.Where(e => e.RoleId == id && e.Isdeleted == false).FirstOrDefault();
            if (ro == null)
            {
                return NotFound();
            }
            ro.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();
        }
    }
}





