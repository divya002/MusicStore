using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        MusicStoreEntities storeDB = new MusicStoreEntities();

        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(9);

            return View(albums);
        }

       private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }
        public ActionResult DailyDeal()
        {
            Album album = GetDailyDeal();
            return PartialView("_DailyDeal", album);
        }
        private Album GetDailyDeal()
        {
            var album = storeDB.Albums
                .OrderBy(a => System.Guid.NewGuid())
                .First();
            album.Price *= 0.5m;
            return album;
        }
    }
}