using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSDP.DATA.EF;
using FSDP.UI.MVC.Utilities;
using Microsoft.AspNet.Identity;

namespace FSDP.UI.MVC.Controllers
{
    public class UserDetailsController : Controller
    {
        private FSDPEntities db = new FSDPEntities();

        //#region Ajax Delete
        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult AjaxDelete(int id)
        //{
        //    //get the publisher from the database
        //    UserDetail user = db.UserDetails.Find(id);

        //    //remove the publisher from EF
        //    db.UserDetails.Remove(user);

        //    //save the changes to the database
        //    db.SaveChanges();

        //    //create a message to send to the user as a JSON result
        //    var message = $"Deleted the following user from the database: {user.FirstName} {user.LastName}";

        //    //return the jsonResult
        //    return Json(new
        //    {
        //        id = id,
        //        message = message

        //    });
        //}
        //#endregion

        [Authorize(Roles = "Admin")]
        // GET: UserDetails
        public ActionResult Index()
        {
            return View(db.UserDetails.ToList());
        }

        // GET: UserDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // GET: UserDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,Image,Instrument1,Instrument2,RelatedSkills,ResumeFilename")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                db.UserDetails.Add(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDetail);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,Image,Instrument1,Instrument2,RelatedSkills,ResumeFilename")] UserDetail userDetail, HttpPostedFileBase userImage, HttpPostedFileBase resume)
        {
            if (ModelState.IsValid)
            {
                if (userImage != null)
                {
                    string imgName = userImage.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png", ".jfif" };
                    if (goodExts.Contains(ext.ToLower()) && (userImage.ContentLength <= 4194304))
                    {
                        imgName = Guid.NewGuid() + ext.ToLower();
                        string savePath = Server.MapPath("~/Content/img/UserImage/");
                        Image convertedImage = Image.FromStream(userImage.InputStream);
                        int maxImageSize = 2000;
                        int maxThumbSize = 150;
                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        if (userDetail.Image != null && userDetail.Image != "NoImage.png")
                        {
                            string path = Server.MapPath("~/Content/img/UserImage/");
                            ImageService.Delete(path, userDetail.Image);
                        }

                        userDetail.Image = imgName;
                    }
                }

                if (resume != null)
                {
                    string resumeName = resume.FileName;
                    string ext = resumeName.Substring(resumeName.LastIndexOf('.'));
                    string[] goodExts = { ".docx", ".doc", ".pdf" };
                    if (goodExts.Contains(ext.ToLower()) && (resume.ContentLength <= 4194304))
                    {
                        resumeName = Guid.NewGuid() + ext.ToLower();
                        resume.SaveAs(Server.MapPath("~/Content/resumes/" + resumeName));

                        if (userDetail.ResumeFilename != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Content/resumes/" + userDetail.ResumeFilename));
                        }

                        userDetail.ResumeFilename = resumeName;
                    }
                }
                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetail);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetail);
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
