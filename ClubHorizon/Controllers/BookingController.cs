using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClubHorizon.Data.DBModel;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace ClubHorizon.Controllers
{
    public class BookingController : Controller
    {
        private ghorizonEntities db = new ghorizonEntities();
        private ApplicationUserManager _userManager;
        private static decimal Amount=250;
        public BookingController()
        {

        }
        public ActionResult Index()
        {
            return View(db.TimeSlotMasters.ToList());
        }
        public  async Task<ActionResult> checkout()
        {
            ViewBag.FirstName = "";
            ViewBag.LastName = "";
            ViewBag.EmailId = "";
            if (Session["BookId"] != null)
             
            {
                if (!string.IsNullOrEmpty(User.Identity.Name))
                    {
                    var user = await UserManager.FindByNameAsync(User.Identity.Name);
                    BookingInfo bi = db.BookingInfoes.Where(i => i.Userid == user.Id).FirstOrDefault();
                    if (bi != null)
                    {
                        ViewBag.FirstName = bi.FirstName;
                        ViewBag.LastName = bi.LastName;
                        ViewBag.EmailId = bi.EmailId;
                    }
                }
                else
                {
                    return RedirectToAction("Login","Account");
                }
                var data = EncryptDecryptStringHelper.DecryptString(Session["BookId"].ToString());
                List<demoevent> Decript = (List<demoevent>)JsonConvert.DeserializeObject<List<demoevent>>(data);
                return View(Decript);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<ActionResult> checkout(BookingInfo Bi)
        {
            if (Session["BookId"] != null)
            {
                var data = EncryptDecryptStringHelper.DecryptString(Session["BookId"].ToString());
                List<demoevent> Decript = (List<demoevent>)JsonConvert.DeserializeObject<List<demoevent>>(data);
                var user = await UserManager.FindByNameAsync(User.Identity.Name);

                BookingInfo bi = new BookingInfo();
                bi.FirstName = Bi.FirstName;
                bi.LastName = Bi.LastName;
                bi.EmailId = Bi.EmailId;
                bi.Phone = Bi.Phone;
                bi.NetAmount = Decript.Count() * 250;
                bi.DiscountAmount = Decript.Count() > 1 ? 100 : 0;
                bi.TotalAmount = bi.NetAmount - bi.DiscountAmount;
                bi.CreatedOn = DateTime.Now;
                bi.CreatedBy = User.Identity.Name;
                bi.Userid = user.Id;
                bi.TotalSlot = Decript.Count();
                bi.TransectionStatus = "Pending";
                bi.IsActive = false;
                db.BookingInfoes.Add(bi);
               await db.SaveChangesAsync();
                List<TimeSlotMaster> TSM = db.TimeSlotMasters.ToList();
               
                foreach (var item in Decript)
                {
                    db.BookingDtlInfoes.Add(new BookingDtlInfo
                    {
                        OrderId = bi.OrderId,
                        Date =Convert.ToDateTime( item.date),
                        TimeSlotsId = TSM.Where(i => i.TimeSlot == item.name).Select(d => d.TimeSlotId).FirstOrDefault(),
                        TimeSlots=item.name,
                        Amount= Amount,
                        CreatedOn=DateTime.Now,
                        CreatedBy = User.Identity.Name


                }) ;
                    
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Success","booking",new { txnId="5632569",Orderid= bi.OrderId });
            }
            else
            {
                return RedirectToAction("Index");
            }
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
        public async Task<ActionResult> Success(string txnId,int Orderid)
        {
            List<BookingDtlInfo> bdi = db.BookingDtlInfoes.Where(i => i.OrderId == Orderid).ToList();

            List<demoevent> Decript = new List<demoevent>();
          
            BookingInfo bi=  db.BookingInfoes.Where(i => i.OrderId == Orderid).FirstOrDefault();
            bi.TransectionNumber = txnId;
            bi.TransectionStatus = "Success";
            bi.IsActive = true;
            db.Entry(bi).State = EntityState.Modified;
            db.SaveChanges();

            Session.Remove("BookId");
            return View(bdi);
        }
        public JsonResult setTimeSlot(demoevent de)
        {
           
           

            List<demoevent> Decript = new List<demoevent>(); 
            if (Session["BookId"] != null && !string.IsNullOrEmpty(Session["BookId"].ToString()))
            {
                var data = EncryptDecryptStringHelper.DecryptString(Session["BookId"].ToString());
                Decript = (List<demoevent>)JsonConvert.DeserializeObject<List<demoevent>>(data);
             
            }
            if (Decript.Any(i => i.id == de.id))
            {
               int index= Decript.FindIndex(i=>i.id == de.id);
                Decript.RemoveAt(index);
            }
            else
            {
                Decript.Add(de);
            }
            Session.Add("BookId", EncryptDecryptStringHelper.EncryptString(JsonConvert.SerializeObject(Decript)));
            return new JsonResult {Data="true", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetTimeSlot(int id)
        {
            List<TimeSlotMaster> tsl = db.TimeSlotMasters.ToList();
            List<demoevent> del = new List<demoevent>();

            var Mondaylist = tsl.Where(i => i.Days.Contains("Monday")).ToList();
            var Saturdaylist = tsl.Where(i => i.Days.Contains("Saturday"));
            var Fridaylist = tsl.Where(i => i.Days.Contains("Friday"));
            var Thursdaylist = tsl.Where(i => i.Days.Contains("Thursday"));
            var Wednesdaylist = tsl.Where(i => i.Days.Contains("Wednesday"));
            var Tuesdaylist = tsl.Where(i => i.Days.Contains("Tuesday"));
            var Sundaylist = tsl.Where(i => i.Days.Contains("Sunday"));
            var count = 1;
            DateTime dtFrom = DateTime.Now.Date;
            List<BookingDtlInfo> BDI=  db.BookingDtlInfoes.Where(i => i.Date >= dtFrom).ToList();
            for (int i = 0; i < 31; i++)
            {
                var date = DateTime.Now.AddDays(i);
                if (date.DayOfWeek == DayOfWeek.Monday)
                        {
                    foreach (var item in Mondaylist)
                    {if (!BDI.Any(u => u.Date == Convert.ToDateTime(date) && u.TimeSlots == item.TimeSlot))
{
                            demoevent de = new demoevent();
                            de.id = count;
                            de.name = item.TimeSlot;
                            de.date = Convert.ToDateTime(date).ToString("MMMM/dd/yyyy");
                            de.type = "Golf Course";
                            del.Add(de);
                            count++;
                        }
                    }
                }
                else if (date.DayOfWeek == DayOfWeek.Tuesday)
                {
                    foreach (var item in Tuesdaylist)
                    {
                       if (!BDI.Any(u => Convert.ToDateTime(u.Date).ToString("MMMM/dd/yyyy") == Convert.ToDateTime(date).ToString("MMMM/dd/yyyy") && u.TimeSlots == item.TimeSlot))
{
                            demoevent de = new demoevent();
                            de.id = count;
                            de.name = item.TimeSlot;
                            de.date = Convert.ToDateTime(date).ToString("MMMM/dd/yyyy");
                            de.type = "Golf Course";
                            del.Add(de);
                            count++;
                        }
                    }
                }
               else if (date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    foreach (var item in Wednesdaylist)
                    {
                        if (!BDI.Any(u => Convert.ToDateTime(u.Date).ToString("MMMM/dd/yyyy") == Convert.ToDateTime(date).ToString("MMMM/dd/yyyy") && u.TimeSlots == item.TimeSlot))
                        {

                            demoevent de = new demoevent();
                            de.id = count;
                            de.name = item.TimeSlot;
                            de.date = Convert.ToDateTime(date).ToString("MMMM/dd/yyyy");
                            de.type = "Golf Course";
                            del.Add(de);
                            count++;
                        }
                    }
                }
              else  if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    foreach (var item in Thursdaylist)
                    {
                       if (!BDI.Any(u => Convert.ToDateTime(u.Date).ToString("MMMM/dd/yyyy") == Convert.ToDateTime(date).ToString("MMMM/dd/yyyy") && u.TimeSlots == item.TimeSlot))
                        {
                            demoevent de = new demoevent();
                            de.id = count;
                            de.name = item.TimeSlot;
                            de.date = Convert.ToDateTime(date).ToString("MMMM/dd/yyyy");
                            de.type = "Golf Course";
                            del.Add(de);
                            count++;
                        }
                    }
                }
                else if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    foreach (var item in Fridaylist)
                    {
                       if (!BDI.Any(u => Convert.ToDateTime(u.Date).ToString("MMMM/dd/yyyy") == Convert.ToDateTime(date).ToString("MMMM/dd/yyyy") && u.TimeSlots == item.TimeSlot))
                        {

                            demoevent de = new demoevent();
                            de.id = count;
                            de.name = item.TimeSlot;
                            de.date = Convert.ToDateTime(date).ToString("MMMM/dd/yyyy");
                            de.type = "Golf Course";
                            del.Add(de);
                            count++;
                        }
                    }
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    foreach (var item in Saturdaylist)
                    {
                       if (!BDI.Any(u => Convert.ToDateTime(u.Date).ToString("MMMM/dd/yyyy") == Convert.ToDateTime(date).ToString("MMMM/dd/yyyy") && u.TimeSlots == item.TimeSlot))
                        {

                            demoevent de = new demoevent();
                            de.id = count;
                            de.name = item.TimeSlot;
                            de.date = Convert.ToDateTime(date).ToString("MMMM/dd/yyyy");
                            de.type = "Golf Course";
                            del.Add(de);
                            count++;
                        }
                    }
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    foreach (var item in Sundaylist)
                    {
                       if (!BDI.Any(u => Convert.ToDateTime(u.Date).ToString("MMMM/dd/yyyy") == Convert.ToDateTime(date).ToString("MMMM/dd/yyyy") && u.TimeSlots == item.TimeSlot))
                        {
                            demoevent de = new demoevent();
                            de.id = count;
                            de.name = item.TimeSlot;
                            de.date = Convert.ToDateTime(date).ToString("MMMM/dd/yyyy");
                            de.type = "Golf Course";
                            del.Add(de);
                            count++;
                        }
                    }
                }
            }

          

            return new JsonResult { Data = del, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        // GET: Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlotMaster timeSlotMaster = db.TimeSlotMasters.Find(id);
            if (timeSlotMaster == null)
            {
                return HttpNotFound();
            }
            return View(timeSlotMaster);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeSlotId,LocId,TimeSlot,Days,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsActive")] TimeSlotMaster timeSlotMaster)
        {
            if (ModelState.IsValid)
            {
                db.TimeSlotMasters.Add(timeSlotMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeSlotMaster);
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlotMaster timeSlotMaster = db.TimeSlotMasters.Find(id);
            if (timeSlotMaster == null)
            {
                return HttpNotFound();
            }
            return View(timeSlotMaster);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeSlotId,LocId,TimeSlot,Days,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsActive")] TimeSlotMaster timeSlotMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeSlotMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeSlotMaster);
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<demoevent> Decript = new List<demoevent>();
            if (Session["BookId"] != null && !string.IsNullOrEmpty(Session["BookId"].ToString()))
            {
                var data = EncryptDecryptStringHelper.DecryptString(Session["BookId"].ToString());
                Decript = (List<demoevent>)JsonConvert.DeserializeObject<List<demoevent>>(data);

            }
            if (Decript.Any(i => i.id == id))
            {
                int index = Decript.FindIndex(i => i.id == id);
                Decript.RemoveAt(index);
            }
            if (Decript.Count() > 0)
            {
                Session.Add("BookId", EncryptDecryptStringHelper.EncryptString(JsonConvert.SerializeObject(Decript)));
            }
            else
            {
                Session.Remove("BookId");


            }
                return RedirectToAction(nameof(checkout));
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSlotMaster timeSlotMaster = db.TimeSlotMasters.Find(id);
            db.TimeSlotMasters.Remove(timeSlotMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
