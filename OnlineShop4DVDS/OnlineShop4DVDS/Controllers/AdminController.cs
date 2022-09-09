using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OnlineShop4DVDS.Models;

namespace OnlineShop4DVDS.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        OnlineDVDEntities4 db = new OnlineDVDEntities4();
        // GET: Admin

      
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MainCat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MainCat(tbl_Category mainCat)
        {
            db.tbl_Category.Add(mainCat);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            List<tbl_Category> categorylist = db.tbl_Category.ToList();
            ViewBag.CatList = new SelectList(categorylist, "CatId", "CatName");
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(tbl_SubCategory category)
        {
            List<tbl_Category> categorylist = db.tbl_Category.ToList();
            ViewBag.CatList = new SelectList(categorylist, "CatId", "CatName");
            db.tbl_SubCategory.Add(category);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult ListCategory()
        {
            var listcat = db.tbl_Category.ToList();
            return View(listcat);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            List<tbl_SubCategory> categorylist = db.tbl_SubCategory.ToList();
            ViewBag.CatList = new SelectList(categorylist, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(tbl_Product Prod, HttpPostedFileBase uploadImg)
        {
            List<tbl_SubCategory> categorylist = db.tbl_SubCategory.ToList();
            ViewBag.CatList = new SelectList(categorylist, "Id", "Name");

            var filename = Path.GetFileName(uploadImg.FileName);
            uploadImg.SaveAs(Server.MapPath("~/images/" + filename));
            Prod.ProductImage = filename;
            db.tbl_Product.Add(Prod);
            db.SaveChanges();
            ModelState.Clear();

            return View();
        }

        public ActionResult ListProduct()
        {
            var listProd = db.tbl_Product.ToList();
            return View(listProd);
        }

        [HttpGet]
        public ActionResult CategoryEdit(int id)
        {
            var maincat = db.tbl_Category.Find(id);
            return View(maincat);
        }

        [HttpPost]
        public ActionResult CategoryEdit(tbl_Category categoryMainn)
        {
            
            db.Entry(categoryMainn).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListCategory");

        }


        public ActionResult MainCatDelete(int id)
        {
            var del = db.tbl_Category.Find(id);
            db.tbl_Category.Remove(del);
            int v = db.SaveChanges();

            return RedirectToAction("ListCategory");
        }

        //public ActionResult OrderDetails()
        //{
        //    var orderList = db.tbl_Order.ToList();
        //    return View(orderList);
        //}


        public ActionResult SubListCategory()
        {
            var listcat = db.tbl_SubCategory.ToList();
            return View(listcat);
        }

        [HttpGet]
        public ActionResult SubCatEdit(int id)
        {

            var maincat = db.tbl_SubCategory.Find(id);

            List<tbl_Category> categorylist = db.tbl_Category.ToList();
            ViewBag.CatList = new SelectList(categorylist, "CatId", "CatName");

            return View(maincat);
        }

        [HttpPost]
        public ActionResult SubCatEdit(tbl_SubCategory subCat)
        {

            db.Entry(subCat).State = System.Data.Entity.EntityState.Modified;

            List<tbl_Category> categorylist = db.tbl_Category.ToList();
            ViewBag.CatList = new SelectList(categorylist, "CatId", "CatName");

            db.SaveChanges();

            return RedirectToAction("SubListCategory");
        }


        public ActionResult SubCatDelete(int id)
        {
            var del = db.tbl_SubCategory.Find(id);
            db.tbl_SubCategory.Remove(del);
            db.SaveChanges();

            return RedirectToAction("SubListCategory");
        }

        [HttpGet]
        public ActionResult ProdEdit(int id)
        {

            var maincat = db.tbl_Product.Find(id);

            List<tbl_SubCategory> categorylist = db.tbl_SubCategory.ToList();
            ViewBag.CatList = new SelectList(categorylist, "Id", "Name");

            return View(maincat);

        }

        [HttpPost]

        public ActionResult ProdEdit(tbl_Product Editprod)
        {

            db.Entry(Editprod).State = System.Data.Entity.EntityState.Modified;

            List<tbl_SubCategory> categorylist = db.tbl_SubCategory.ToList();
            ViewBag.CatList = new SelectList(categorylist, "Id", "Name");


            db.SaveChanges();

            return RedirectToAction("ListProduct");

        }

        public ActionResult ProdDelete(int id)
        {
            var del = db.tbl_Product.Find(id);
            db.tbl_Product.Remove(del);
            db.SaveChanges();

            return RedirectToAction("ListProduct");
        }



    }
}