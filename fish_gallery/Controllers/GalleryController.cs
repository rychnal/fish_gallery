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
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";
            IList<Gallery> galleries;

            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {

                galleries = session.Query<Gallery>().ToList(); //  Querying to get all the books
            }

            return View(galleries);
        }

        public ActionResult Details(int id)
        {
            return RedirectToAction("Index","Fish", new { id_gallery = id });

        }
    }
}