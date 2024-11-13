using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class PurchaseController : Controller
    {
        db_testEntities db = new db_testEntities();
        // GET: Purchase
        public ActionResult Index()
        { 
            return View();
        }

        [HttpGet]
        public ActionResult DisplayPurchase()
        {

            List<Purchase> list = db.Purchases.OrderByDescending(x => x.id).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult PurchaseProduct()
        {
            if (Session["u_id"] == null)
            {
                return RedirectToAction("SignUp", "User");
            }
            else
            {
                List<string> list = db.Products.Select(x => x.Product_name).ToList();
                ViewBag.Product_Name = new SelectList(list);
                return View();
            }
           
            // here we have used string in list bcz we will be binding list data with the model in view to show it on up in drop down
        }

        [HttpPost]
        public ActionResult PurchaseProduct(Purchase pur)
        {
            Purchase purchase = new Purchase();
            purchase.Purchase_prod = pur.Purchase_prod;
            purchase.Purchase_qnty = pur.Purchase_qnty;
            purchase.Purchase_date = pur.Purchase_date;
            purchase.Pur_fk_user = Convert.ToInt32(Session["u_id"].ToString());

            db.Purchases.Add(purchase);
            db.SaveChanges();
            return RedirectToAction("DisplayPurchase");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            return View(p);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            return View(p);
        }

        [HttpPost]
        public ActionResult Delete(int id, Purchase per)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            db.Purchases.Remove(p);
            db.SaveChanges();

            return RedirectToAction("DisplayPurchase");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.Product_Name = new SelectList(list);
            return View(p);

        }

        [HttpPost]
        public ActionResult Edit(int id, Purchase pur)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            p.Purchase_date = pur.Purchase_date;
            p.Purchase_prod = pur.Purchase_prod;
            p.Purchase_qnty = pur.Purchase_qnty;
            p.Pur_fk_user = Convert.ToInt32(Session["u_id"].ToString());

            db.SaveChanges();
            return RedirectToAction("DisplayPurchase");

        }
    }
}