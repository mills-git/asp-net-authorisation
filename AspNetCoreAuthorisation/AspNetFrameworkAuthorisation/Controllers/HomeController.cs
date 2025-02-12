using AspNetFrameworkAuthorisation.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Authorization.Mvc;

namespace AspNetFrameworkAuthorisation.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _dbContext = new AppDbContext(); 

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Users() {
            Debug.WriteLine(_dbContext.Users.FirstOrDefault().Id); 

            return View(); 
        }

        public ActionResult Claims() {
            ViewBag.ClaimsIdentity = Thread.CurrentPrincipal.Identity;

            return View(); 
        }

        [ClaimsAuthorize("Permissions", "Admin")]
        public ActionResult Admin() {
            return View(); 
        }
    }
}