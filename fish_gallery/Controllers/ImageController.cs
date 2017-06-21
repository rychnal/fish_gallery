using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using NHibernate;
using NHibernate.Linq;
using fish_gallery.Models;
using NHibernate.Criterion;

namespace fish_gallery.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index(int id_fish)
        {
            IList<Image> fishes;
            Session["id_fish"] = id_fish;
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {

                fishes = session.Query<Image>().Where(x => x.FishId == id_fish).ToList(); //  Querying to get all the books
            }

            return View(fishes);
        }

        // GET: Image/Details/5
        public ActionResult Details(int id)
        {
            Image image;
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                image = session.QueryOver<Image>()
                    .Where(x => x.Id == id).JoinQueryOver<Users>(i => i.ImageUser).SingleOrDefault();

            }
            string imagePath = "/Images/" + image.ImageUser.Name + "/" + image.Name + image.Extension;
            image.imagePath = imagePath;
            
            return View(image);
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Image/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Image/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Image/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Image/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Image/Delete/5
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

        public ActionResult FileUpload(HttpPostedFileBase file, Image image)
        {

            if (file != null)
            {
                using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
                {
                    string ImageName = Path.GetFileName(image.Name);
                    string extension = Path.GetExtension(file.FileName);
                    string physicalPath = Server.MapPath("~/images/" + Session["username"] + "/" + ImageName + extension);
                    if(!System.IO.File.Exists(Server.MapPath("~/images/" + Session["username"]))){
                        Directory.CreateDirectory(Server.MapPath("~/images/" + Session["username"]));
                    }
                    file.SaveAs(physicalPath);
                    Users u = session.QueryOver<Users>()
                    .Where(x => x.Id == int.Parse(Session["user_id"].ToString())).SingleOrDefault();
                    image.FishId = int.Parse(Session["id_fish"].ToString());
                    image.ImageUser = u;//int.Parse(Session["user_id"].ToString());
                    image.Name = ImageName;
                    image.Extension = extension;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                       // IQuery sqlQry = session.CreateSQLQuery("SET IDENTITY_INSERT image ON");
                      //  object ret = sqlQry.UniqueResult();
                        session.Save(image);
                        transaction.Commit();
                    }
                    return RedirectToAction("Index", "Image", new { id_fish = int.Parse(Session["id_fish"].ToString()) });
                   
                }
         
                
               

                // save image in folder
                

                //save new record in database
               

            }
            //Display records
            return RedirectToAction("../home/Display/");
        }

       
    }
}
