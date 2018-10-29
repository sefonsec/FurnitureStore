using System.Linq;
using System.Web.Mvc;
using FurnitureStore.Filters;
using FurnitureStore.Models;
using WebMatrix.WebData;

namespace FurnitureStore.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {        
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
