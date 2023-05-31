using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages
{
    public class aboutFruitkhaModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public readonly IHostEnvironment _hostEnvironment;
        public aboutFruitkhaModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }

        public IEnumerable<AboutBanner> bannerlist { get; set; }
        public IEnumerable<AboutService> Servicelist { get; set; }

        public IEnumerable<OurTeam> teanlist { get; set; }


        [BindProperty]
        public AboutBanner bannerobj { get; set; }

        [BindProperty]
        public OurTeam teamobj { get; set; }



        [BindProperty]
        public About aboutobj { get; set; }

        public void OnGet()
        {
            aboutobj = _db.Abouts.FirstOrDefault(x => x.Isdeleted == false);

            bannerobj = _db.AboutBanners.FirstOrDefault(x => x.Isdeleted == false);

            teamobj = _db.OurTeams.FirstOrDefault(t => t.Isdeleted == false);





            teanlist = _db.OurTeams.Where(t => t.Isdeleted == false).ToList();

            Servicelist = _db.AboutServices.Where(a => a.Isdeleted == false).ToList();

            bannerlist = _db.AboutBanners.Where(b => b.Isdeleted == false).ToList();

        }
    }
}






