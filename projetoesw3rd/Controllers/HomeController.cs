﻿using projetoesw3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace projetoesw3rd.Controllers
{
    public class HomeController : Controller
    {
        private BdESWG2Entities db = new BdESWG2Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User reg)
        {
            if(ModelState.IsValid)
            {
                db.Users.Add(reg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User reg)
        {
            if (ModelState.IsValid)
            {
                var details = (from userlist in db.Users
                               where userlist.users_number == reg.users_number && userlist.users_password == reg.users_password
                               select new
                               {
                                   userlist.users_id,
                                   userlist.users_number
                               }).ToList();
                if (details.FirstOrDefault() != null)
                {
                    Session["users_id"] = details.FirstOrDefault().users_id;
                    Session["users_number"] = details.FirstOrDefault().users_number;

                    FormsAuthentication.SetAuthCookie(Session["users_number"].ToString(), false);

                    var authTicket = new FormsAuthenticationTicket(1, Session["users_number"].ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, Session["users_id"].ToString());
                    //   string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    // var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, authTicket.ToString());
                    HttpContext.Response.Cookies.Add(authCookie);

                    return RedirectToAction("Welcome", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View(reg);
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult About()
        {
            if (Session["users_number"].ToString() == null)
            {
                ViewBag.Message = "Your application description page.";

            }
            else
            {
                ViewBag.Message = "Your application description";

            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}