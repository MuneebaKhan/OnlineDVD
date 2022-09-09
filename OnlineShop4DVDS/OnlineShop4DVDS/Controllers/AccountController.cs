using OnlineShop4DVDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop4DVDS.Controllers
{
    public class AccountController : Controller
    {
        OnlineDVDEntities4 db = new OnlineDVDEntities4();

        // GET: Account
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(tbl_User user)
        {
             user.Role = "V";
            db.tbl_User.Add(user);

       
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_User user)
        {


            var query = db.tbl_User.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (query != null)
            {
                if (query.Role == "V")
                {
                    FormsAuthentication.SetAuthCookie(query.UserName, false);
                    Session["Uid"] = query.Id;
                    Session["User"] = query.UserName;
                    Session["Email"] = query.Email;
                    return RedirectToAction("Index", "Home");
                }
                else if (query.Role == "A")
                {
                    FormsAuthentication.SetAuthCookie(query.UserName, false);
                    Session["Uid"] = query.Id;
                    Session["User"] = query.UserName;
                    return RedirectToAction("Index", "Admin");
                }


            }
            else
            {
                TempData["msg"] = "user name and password is in correct";

            }

            return View();
            //var count = db.tbl_User.Where(x => x.Name == user.Name && x.Password == user.Password).Count();

            //if (count != 0)
            //{
            //    FormsAuthentication.SetAuthCookie(user.Name, false);
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    TempData["msg"] = "user name and password is in correct";
            //    return View();
            //}
        }
            public ActionResult Logout()
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }

        }
    }