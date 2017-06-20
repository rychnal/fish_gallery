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

        public ActionResult Login()
        {
            return View();

        }



        [HttpPost]
        public ActionResult Login(Users user)
        {
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                //var userd = session.CreateQuery("from Users where Users.name = :name").SetParameter("name", user.Name).UniqueResult();
                //var user_data = session.CreateSQLQuery("SELECT * FROM Users WHERE Users.name = :name").SetParameter("name", user.Name).UniqueResult(); //  Querying to get all the books
                //Console.Write(user_data[0]);
                var user_data = session.QueryOver<Users>()
                    .Where(x => x.Name == user.Name).List();
                Users user_info;
                if (user_data != null)
                {
                    user_info = user_data[0];
                    if (user_info.Password == user.Password)
                    {
                        Session["username"] = user.Name;
                        Session["user_id"] = user.Id;
                        return RedirectToAction("Index","Gallery");
                    }
                }
               
                
            }
            return View();
        }

    }
}