using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SampleMVCApp.Models;
using SampleMVCApp.ViewModels;

namespace SampleMVCApp.Controllers
{
    public class StudentsController : Controller
    {
        private SampleMVCAppDBEntities db = new SampleMVCAppDBEntities();

        // GET: Students
        public ActionResult Index()
        {
            List<StudentVm> students = new List<StudentVm>();
            foreach(var std in db.Students)
            {
                StudentVm model = new StudentVm();
                model.Crebo = std.Crebo;
                model.Id = std.StudentId;
                model.Username = std.AspNetUsers.UserName;
                model.Slb = std.Slb;
                model.Group = std.Group;
                students.Add(model);
            }
            return View(students);
        }

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentVm studentVm = db.StudentVms.Find(id);
            if (studentVm == null)
            {
                return HttpNotFound();
            }
            return View(studentVm);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Crebo,Slb,Group,Username,Password,StudentId")] StudentVm studentVm)
        {
            if (ModelState.IsValid)
            {
                if(String.IsNullOrEmpty(studentVm.Password))
                {
                    studentVm.Password = "TesT12345";
                }

                // Voeg een nieuw aspnetuser account toe
                var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = studentVm.Username };
                user.Email = studentVm.Username;
                manager.Create(user, studentVm.Password);

                // Koppel de aspnetrole Student aan het account
                manager.AddToRole(user.Id, "Student");

                // Maak een nieuw student object aan
                Students s = new Students();
                s.Group = studentVm.Group;
                s.Slb = studentVm.Slb;
                s.Crebo = studentVm.Crebo;
                s.Id = user.Id;
                s.StudentId = studentVm.StudentId;

                // Voeg het student object toe aan de database
                db.Students.Add(s);

                // Sla de wijzigingen op in de database
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentVm);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentVm studentVm = db.StudentVms.Find(id);
            if (studentVm == null)
            {
                return HttpNotFound();
            }
            return View(studentVm);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Crebo,Slb,Group,Username")] StudentVm studentVm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentVm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentVm);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentVm studentVm = db.StudentVms.Find(id);
            if (studentVm == null)
            {
                return HttpNotFound();
            }
            return View(studentVm);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            StudentVm studentVm = db.StudentVms.Find(id);
            db.StudentVms.Remove(studentVm);
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
