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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";
            IList<Users> users;

            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
             
                users = session.Query<Users>().ToList(); //  Querying to get all the books
            }

            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Users user)
        {
            
                using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(user);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            
        }


    }
}