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

                fishes = session.Query<Fish>().Where(x => x.FishGallery.Id == id_gallery).ToList(); //  Querying to get all the books
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
           
                // TODO: Add insert logic here
                using (ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                         Gallery gl = session.QueryOver<Gallery>()
                         .Where(g => g.Id == int.Parse(Session["id_gallery"].ToString())).SingleOrDefault();
                         fish.FishGallery = gl;
                        
                        session.Save(fish);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index", "Fish", new { id_gallery = Session["id_gallery"] });
          
        }

        // GET: Fish/Edit/5
        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var fish = session.Get<Fish>(id);
                return View(fish);
            }
       
        }

        // POST: Fish/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Fish fish)
        {
            try
            {
                using (ISession session = NHibernateSession.OpenSession())
                {
                    var fishtoUpdate = session.Get<Fish>(id);

                    fishtoUpdate.Name = fish.Name;
                    fishtoUpdate.Weight = fish.Weight;
                    fishtoUpdate.Length = fish.Length;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(fishtoUpdate);
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

        // GET: Fish/Delete/5
        public ActionResult Delete( Fish fh)
        {
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                using (ITransaction Transaction = session.BeginTransaction())
                {
                    session.Delete(fh);
                    Transaction.Commit();
                }


            }
            return RedirectToAction("Index", "Gallery", new { id_gallery = int.Parse(Session["uder_id"].ToString()) });
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
