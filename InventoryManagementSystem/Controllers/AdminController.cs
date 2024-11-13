using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        db_testEntities db = new db_testEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(tbl_admin avm)
        {
            tbl_admin ad = db.tbl_admin.Where(x => x.ad_username == avm.ad_username && x.ad_password == avm.ad_password).SingleOrDefault();
            
            if (ad!=null)
            {
               Session["ad_id"] = ad.ad_id.ToString();
                //return RedirectToAction("Create");
                return RedirectToAction("CreateProduct", "Product");
            }
            else
            {
                ViewBag.error = "Invalid Username or password";
            }
            return View();
        }

        public ActionResult Create()
        {
            if(Session["ad_id"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }
    }
}