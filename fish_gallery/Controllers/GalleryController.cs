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
        public ActionResult Index(int id_user_gallery)
        {
            ViewBag.Message = "Your application description page.";
            IList<Gallery> galleries;
            Session["user_id_gallery"] = id_user_gallery;
            
            if (Session["user_id"] == null || id_user_gallery != int.Parse(Session["user_id"].ToString()))
            {

                using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
                {

                    galleries = session.Query<Gallery>()
                        .Where(x => x.UserId == id_user_gallery)
                        .Where(x => x.PublicGallery == 1).ToList(); //  Querying to get all the books
                }
            }else
            {
                using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
                {

                    galleries = session.Query<Gallery>()
                        .Where(x => x.UserId == id_user_gallery)
                        .ToList(); //  Querying to get all the books
                }
            }

            return View(galleries);
        }

        public ActionResult Details(int id)
        {
            Gallery gl;
            using (ISession session = NHibernateSession.OpenSession())
            {
                gl =  session.QueryOver<Gallery>()
                    .Where(x => x.Id == id).SingleOrDefault();
            }
            Session["gallery_id"] = gl.Id;
            if (Session["user_id"] != null && gl.UserId.ToString() == Session["user_id"].ToString())
            {
                return RedirectToAction("Index", "Fish", new { id_gallery = id });
            }
            else
            {
                return RedirectToAction("LoginGallery", "Gallery");
            }
            

        }
        public ActionResult LoginGallery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginGallery(string password)
        {
            Gallery gl;
            using (ISession session = NHibernateSession.OpenSession())
            {
                gl = session.QueryOver<Gallery>()
                    .Where(x => x.Id == int.Parse(Session["gallery_id"].ToString())).SingleOrDefault();
            }
            if(gl.Password == password)
            {
                Session["gallery_id"] = null;
                return RedirectToAction("Index", "Fish", new { id_gallery =  gl.Id});
            }
            return View();
        }




        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Gallery gl)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    gl.UserId = int.Parse(Session["user_id"].ToString());
                   
                    session.Save(gl);

                    gl.Url = Request.Url.GetLeftPart(UriPartial.Authority) + "/Gallery/Details/" + gl.Id;
                    session.SaveOrUpdate(gl);
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index", "Gallery", new { id_user_gallery = gl.UserId });

        }

        public ActionResult Delete(Gallery gl)
        {
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                using (ITransaction Transaction = session.BeginTransaction())
                {
                    session.Delete(gl);
                    Transaction.Commit();
                }


            }
            return RedirectToAction("Index", "Gallery", new { id_user_gallery = Session["user_id"].ToString() });
        }

    }
}