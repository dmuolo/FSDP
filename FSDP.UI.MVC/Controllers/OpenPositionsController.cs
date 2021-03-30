using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSDP.DATA.EF;
using PagedList.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;
using FSDP.UI.MVC.Models;

namespace FSDP.UI.MVC.Controllers
{
    public class OpenPositionsController : Controller
    {
        private FSDPEntities db = new FSDPEntities();

        [Authorize(Roles = "Employee")]
        public ActionResult Apply(int id)
        {
            string userID = User.Identity.GetUserId();
            UserDetail user = db.UserDetails.Find(userID);
            string resume = user.ResumeFilename;
            Application app = new Application();
            app.OpenPositionId = id;
            app.UserId = userID;
            app.ApplicationDate = DateTime.Now;
            app.ApplicationStatus = 1;
            app.ResumeFilename = resume;
            db.Applications.Add(app);
            db.SaveChanges();

            return RedirectToAction("Index", "OpenPositions");
        }

        // GET: OpenPositions
        public ActionResult Index(string searchString, int page = 1, int locationid = -1)
        {
            string userID = User.Identity.GetUserId();
            int pageSize = 8;
            var openPositions = db.OpenPositions.OrderBy(o => o.Position.Title).ToList();

            //list of user's applications
            var userApps = db.Applications.Where(ua => ua.UserId == userID);

            foreach (var op in openPositions)
            {
                foreach (var ua in userApps)
                {
                    if (op.OpenPositionId == ua.OpenPositionId)
                    {
                        op.HasApplied = true;
                    }
                }
            }

            //list of user's resumes
            UserDetail user = db.UserDetails.Find(userID);
            string resume = user.ResumeFilename;
            if (resume != null)
            {
                user.HasResume = true;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                openPositions = openPositions.Where(o => o.Position.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }

            if (locationid != -1)
            {
                openPositions = openPositions.Where(o => o.LocationId == locationid).ToList();
            }

            if (User.IsInRole("Manager"))
            {
                openPositions = db.OpenPositions.Where(o => o.Location.ManagerId == userID).ToList();
                return View(openPositions.ToPagedList(page, pageSize));
            }
            else
            {

                ViewBag.SearchString = searchString;

                ViewBag.LocationID = locationid;

                return View(openPositions.ToPagedList(page, pageSize));
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: OpenPositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        [Authorize(Roles = "Manager")]
        // GET: OpenPositions/Create
        public ActionResult Create()
        {
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title");
            return View();
        }

        [Authorize(Roles = "Manager")]
        // POST: OpenPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpenPositionId,PositionId,LocationId")] OpenPosition openPosition)
        {
            string userid = User.Identity.GetUserId();
            UserDetail user = db.UserDetails.Find(userid);
            var location = db.Locations.Where(l => l.ManagerId == userid).SingleOrDefault();
            openPosition.LocationId = location.LocationId;

            if (ModelState.IsValid)
            {
                db.OpenPositions.Add(openPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            return View(openPosition);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: OpenPositions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            return View(openPosition);
        }

        [Authorize(Roles = "Admin,Manager")]
        // POST: OpenPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpenPositionId,PositionId,LocationId")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openPosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            return View(openPosition);
        }

        [Authorize(Roles = "Manager")]
        // GET: OpenPositions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        [Authorize(Roles = "Manager")]
        // POST: OpenPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpenPosition openPosition = db.OpenPositions.Find(id);
            db.OpenPositions.Remove(openPosition);
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
