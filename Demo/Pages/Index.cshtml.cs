using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.Pages
{
    public class IndexModel : PageModel
    {


        public readonly DemoProjectContext _db;

        public IndexModel(DemoProjectContext db)
        {
            _db = db;
        }


        public IEnumerable<IndexHome> indexList { get; set; }

        public IEnumerable<Product> Productlist { get; set; }

        public IEnumerable<Deal> dealList { get; set; }

        public IEnumerable<OurTeam> teanlist { get; set; }

        public IEnumerable<GeneralNews> generalNewsDatalist { get; set; }






        [BindProperty]
        public GeneralNews generalnewsobj { get; set; }


        [BindProperty]
        public ProductTitle titleobj { get; set; }

        [BindProperty]
        public IndexHome indexobj { get; set; }


        [BindProperty]
        public Product productobj { get; set; }


        [BindProperty]
        public Deal dealobj { get; set; }


        [BindProperty]
        public AboutBanner bannerobj { get; set; }


        [BindProperty]
        public News newsobj { get; set; }



        [BindProperty]
        public Frui fruiobj { get; set; }

        public void OnGet()
        {
            indexList = _db.IndexHomes.Where(i => i.Isdeleted == false).ToList();

            Productlist = _db.Products.Where(i => i.Isdeleted == false).ToList();

            dealList = _db.Deals.Where(i => i.Isdeleted == false).ToList();

            teanlist = _db.OurTeams.Where(t => t.Isdeleted == false).ToList();

            generalNewsDatalist = _db.GeneralNews.Where(t => t.Isdeleted == false).ToList();



            indexobj = _db.IndexHomes.FirstOrDefault(i => i.Isdeleted == false);

            titleobj = _db.ProductTitles.FirstOrDefault(p => p.Isdeleted == false);

            productobj = _db.Products.FirstOrDefault(p => p.Isdeleted == false);

            dealobj = _db.Deals.FirstOrDefault(d => d.Isdeleted == false);

            bannerobj = _db.AboutBanners.FirstOrDefault(x => x.Isdeleted == false);

            newsobj = _db.News.FirstOrDefault(x => x.Isdeleted == false);

            generalnewsobj = _db.GeneralNews.FirstOrDefault(x => x.Isdeleted == false);


            fruiobj = _db.Fruis.FirstOrDefault(x => x.Isdeleted == false);

        }
    }
}