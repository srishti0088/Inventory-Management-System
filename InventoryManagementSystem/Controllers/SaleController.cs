using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class SaleController : Controller
    {
        db_testEntities db = new db_testEntities();
        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DisplaySale()
        {
            List<Sale> list = db.Sales.OrderByDescending(x => x.id).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult SaleProduct()
        {
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.Product_Name = new SelectList(list);
            return View();
            // here we have used string in list bcz we will be binding list data with the model in view to show it on up in drop down
        }

        [HttpPost]
        public ActionResult SaleProduct(Sale sale)
        {
            db.Sales.Add(sale);
            db.SaveChanges();
            return RedirectToAction("DisplaySale");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Sale sale = db.Sales.Where(x => x.id == id).SingleOrDefault();
            return View(sale);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Sale sale = db.Sales.Where(x => x.id == id).SingleOrDefault();
            return View(sale);
        }

        [HttpPost]
        public ActionResult Delete(int id, Sale sale)
        {
            Sale sal = db.Sales.Where(x => x.id == id).SingleOrDefault();
            db.Sales.Remove(sal);
            db.SaveChanges();

            return RedirectToAction("DisplaySale");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Sale sale = db.Sales.Where(x => x.id == id).SingleOrDefault();
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.Product_Name = new SelectList(list);
            return View(sale);
        }

        [HttpPost]
        public ActionResult Edit(int id, Sale sale)
        {
            Sale s = db.Sales.Where(x => x.id == id).SingleOrDefault();
            s.Sale_prod = sale.Sale_prod;
            s.Sale_qnty = sale.Sale_qnty;
            s.Sale_date = sale.Sale_date;
            db.SaveChanges();
            return RedirectToAction("DisplaySale");
        }

    }
}