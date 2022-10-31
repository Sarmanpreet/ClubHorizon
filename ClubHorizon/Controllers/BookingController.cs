using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClubHorizon.Data.DBModel;
using Newtonsoft.Json;

namespace ClubHorizon.Controllers
{
    public class BookingController : Controller
    {
        private ghorizonEntities db = new ghorizonEntities();

        // GET: Booking

        public ActionResult Index()
        {
            return View(db.TimeSlotMasters.ToList());
        }

        public JsonResult setTimeSlot(demoevent de)
        {
            
            Session.Add("BookId", EncryptDecryptStringHelper.EncryptString(JsonConvert.SerializeObject(de)));
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
            for (int i = 0; i < 31; i++)
            {
                var date = DateTime.Now.AddDays(i);
                if (date.DayOfWeek == DayOfWeek.Monday)
                        {
                    foreach (var item in Mondaylist)
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
                else if (date.DayOfWeek == DayOfWeek.Tuesday)
                {
                    foreach (var item in Tuesdaylist)
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
               else if (date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    foreach (var item in Wednesdaylist)
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
              else  if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    foreach (var item in Thursdaylist)
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
                else if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    foreach (var item in Fridaylist)
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
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    foreach (var item in Saturdaylist)
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
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    foreach (var item in Sundaylist)
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
            TimeSlotMaster timeSlotMaster = db.TimeSlotMasters.Find(id);
            if (timeSlotMaster == null)
            {
                return HttpNotFound();
            }
            return View(timeSlotMaster);
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
