using AspNetFrameworkAuthorisation.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Yarp.ReverseProxy.Health;
using Microsoft.AspNetCore.Authorization;

namespace AspNetFrameworkAuthorisation.Controllers {
    public class HomeController : Controller {
        private AppDbContext _dbContext = new AppDbContext();

        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Users() {
            var user = _dbContext.Users.FirstOrDefault();

            if (user != null) {
                Debug.WriteLine(user.Id);
            }

            return View();
        }

        public ActionResult Claims() {
            ViewBag.ClaimsIdentity = HttpContext.User.Identity;

            return View();
        }

        [Authorize(Policy = "Admin")]
        public ActionResult Admin() {
            return View();
        }
    }
}