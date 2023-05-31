using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace Demo.Pages.Admin
{
    public class NewsModel : PageModel
    {
        public readonly DemoProjectContext _db;

        public NewsModel(DemoProjectContext db)
        {
            _db = db;

        }

        public IEnumerable<News> newsDatalist { get; set; }



        [BindProperty]
        public News news { get; set; }



        public IActionResult OnGet(int? id)
        {
            newsDatalist = _db.News.Where(p => p.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var newss = _db.News.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
                if (newss == null)
                {
                    return Page();
                }
                news = newss;
                return Page();
            }
            news = new News();
            return Page();

        }



        public IActionResult OnPost()

        {
            if (news.Id > 0)
            {
                var newss = _db.News.AsNoTracking().Where(p => p.Id == news.Id && p.Isdeleted == false).FirstOrDefault();
                if (newss == null)
                {
                    return Page();

                }




                news.Isdeleted = newss.Isdeleted;

                _db.News.Update(news);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }

            else


            {

                news.Isdeleted = false;

                _db.News.Add(news);
                _db.SaveChanges();

                TempData["success"] = "data saved";
            }
            return RedirectToPage();

        }



        public IActionResult OnPostDelete(int? id)

        {
            var newss = _db.News.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
            if (newss == null)
            {
                return Page();
            }

            newss.Isdeleted = true;
            _db.SaveChanges();
            TempData["success"] = "data deleted";

            return RedirectToPage();
        }



    }
}
