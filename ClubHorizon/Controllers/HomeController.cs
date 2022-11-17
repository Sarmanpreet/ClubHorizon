using ClubHorizon.Data.DBModel;
using ClubHorizon.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClubHorizon.Controllers
{
    public class HomeController : Controller
    {
        private ghorizonEntities db = new ghorizonEntities();
        private ApplicationUserManager _userManager;
        public ActionResult Index()
        {
            return View();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public async Task<ActionResult> MyOrders()
        {
            MyProfile mp = new MyProfile();
            mp.user = await UserManager.FindByNameAsync(User.Identity.Name);
            mp.BookingInfo = db.BookingInfoes.Where(i => i.Userid == mp.user.Id).ToList();
            mp.BookingDtlInfo = (from bdi in db.BookingDtlInfoes
                                 join bi in db.BookingInfoes on bdi.OrderId equals bi.OrderId
                                 where bi.Userid == mp.user.Id
                                 select bdi).ToList();

            return View(mp);
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
    }
}