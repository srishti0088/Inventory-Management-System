using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        db_testEntities db = new db_testEntities();
        // GET: User
        public ActionResult Index()
        {
            List<Product> list = db.Products.OrderByDescending(x => x.id).ToList();

            return View(list);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(tbl_user uvm)
        {
            tbl_user u = new tbl_user();
            u.u_name = uvm.u_name;
            u.u_email = uvm.u_email;
            u.u_password = uvm.u_password;
            u.u_contact = uvm.u_contact;
            db.tbl_user.Add(u);
            db.SaveChanges();
            return RedirectToAction("login");
        }

        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(tbl_user avm)
        {
            tbl_user ad = db.tbl_user.Where(x => x.u_email == avm.u_email && x.u_password == avm.u_password).SingleOrDefault();

            if (ad != null)
            {
                Session["u_id"] = ad.u_id.ToString();
                //return RedirectToAction("Create");
                return RedirectToAction("PurchaseProduct", "Purchase");
            }
            else
            {
                ViewBag.error = "Invalid Username or password";
            }
            return View();
        }

        public ActionResult Signout(tbl_user avm)
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}