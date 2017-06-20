using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using fish_gallery.Models;

namespace fish_gallery.Controllers
{
    public class FishController : Controller
    {
        // GET: Fish
        public ActionResult Index(int id_gallery)
        {
            IList<Fish> fishes;
            Session["id_gallery"] = id_gallery;
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {

                fishes = session.Query<Fish>().Where(x => x.GalleryId == id_gallery).ToList(); //  Querying to get all the books
            }

            return View(fishes);

        }

        // GET: Fish/Details/5
        public ActionResult Details(int id)
        {
            return RedirectToAction("Index", "Image", new { id_fish = id });
        }

        // GET: Fish/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fish/Create
        [HttpPost]
        public ActionResult Create(Fish fish)
        {
            try
            {
                // TODO: Add insert logic here
                using (ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        fish.GalleryId = int.Parse(Session["id_gallery"].ToString());
                        
                        session.Save(fish);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index", "Fish", new { id_gallery = Session["id_gallery"] });
            }
            catch
            {
                return View();
            }
        }

        // GET: Fish/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Fish/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                
                    return RedirectToAction("Index", new { id_fish = Session["id_fish"]});
            }
            catch
            {
                return View();
            }
        }

        // GET: Fish/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Fish/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
