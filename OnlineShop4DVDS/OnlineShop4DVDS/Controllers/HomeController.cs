using OnlineShop4DVDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop4DVDS.Controllers
{
    public class HomeController : Controller
    {
        OnlineDVDEntities4 db = new OnlineDVDEntities4();
        List<Cart> li = new List<Cart>();

        // GET: Home
        public ActionResult Index()
        {
            var prodlist = db.tbl_Product.Include("tbl_SubCategory").ToList();
            return View(prodlist);
        }

        [HttpGet]
        public ActionResult AddtoCart(int id)
        {
            var query = db.tbl_Product.Where(x => x.ProductId == id).SingleOrDefault();

            return View(query);
        }

        [HttpPost]
        public ActionResult AddtoCart(int id, int qty)
        {
            tbl_Product p = db.tbl_Product.Where(x => x.ProductId == id).SingleOrDefault();
            Cart c = new Cart();
            c.prodid = id; //4
            c.proname = p.ProductName; //Bag
            c.Image = p.ProductImage;//img6
            c.price = Convert.ToInt32(p.ProductPrice); //1000
            c.qty = Convert.ToInt32(qty);//2
            c.bill = c.qty * c.price; //2 * 1000 =2000

            if (Session["Cart"] == null)
            {
                li.Add(c);
                Session["Cart"] = li;
            }
            else
            {

                List<Cart> li2 = Session["Cart"] as List<Cart>;
                int flag = 0;

                foreach (var item in li2)
                {
                    if (item.prodid == c.prodid)
                    {
                        item.bill += c.bill;
                        item.qty += c.qty;
                        flag = 1;

                    }

                }

                if (flag == 0)
                {
                    li2.Add(c);
                }
                Session["Cart"] = li2;
            }

            if (Session["Cart"] != null)
            {
                int x = 0;
                List<Cart> li2 = Session["Cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                Session["total"] = x;
                Session["item_count"] = li2.Count();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult remove(int id)
        {
            if (Session["Cart"] == null)
            {
                Session.Remove("total");
                Session.Remove("Cart");
            }
            else
            {
                List<Cart> li2 = Session["Cart"] as List<Cart>;
                Cart c = li2.Where(x => x.prodid == id).SingleOrDefault();
                li2.Remove(c);
                int s = 0;
                foreach (var item in li2)
                {
                    s += item.bill;
                }
                Session["total"] = s;
                Session["item_count"] = li2.Count();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CheckoutProceed()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Registration", "Account");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult CheckoutProceed(string contact, string address, string Pay)
        {
            ViewBag.Contact = contact;
            ViewBag.Address = address;
            ViewBag.Pay = Pay;


            if (ModelState.IsValid)
            {
                List<Cart> li2 = Session["Cart"] as List<Cart>;
                tbl_Invoice inv = new tbl_Invoice();
                inv.UserId = Convert.ToInt32(Session["Uid"].ToString());
                inv.InvoiceDate = System.DateTime.Now;
                inv.Bill = (int)Session["total"];
                inv.Payment = ViewBag.Pay;
                db.tbl_Invoice.Add(inv);
                db.SaveChanges();

                foreach (var item in li2)
                {
                    tbl_OrderDetail order = new tbl_OrderDetail();
                    order.ProductId = item.prodid;
                    //order.tbl_Invoice.InvoiceDate = System.DateTime.Now;
                    order.InvoiceId = inv.Invoiceid;
                    order.UserId = Convert.ToInt32(Session["Uid"].ToString());

                    order.Qty = item.qty;
                    order.ProductName = item.proname;
                    order.Unit = item.price;
                    order.Total = item.bill;
                    order.PayMethod = ViewBag.Pay;
                    order.Contact = ViewBag.Contact;
                    order.Address = ViewBag.Address;
                    order.OrderDate = DateTime.Now;

                    db.tbl_OrderDetail.Add(order);
                    db.SaveChanges();
                }
                Session.Remove("total");
                Session.Remove("Cart");
                Session["msg"] = "Order Book Successfully";

                return RedirectToAction("Index");

            }

            return RedirectToAction("CheckoutProceed");
        }

        public ActionResult Orders(int id)
        {
            var orderList = db.tbl_Invoice.Where(x => x.UserId == id).ToList();

            return View(orderList);
        }

        public ActionResult About()
        {
            return View("About");
        }

        public ActionResult Contact()
        {
            return View("Contact");
        }

        [HttpGet]
        public ActionResult CencelOrder(int id)
        {
            var del = db.tbl_OrderDetail.Where(x => x.InvoiceId == id).ToList();
            foreach (var item in del)
            {
                db.tbl_OrderDetail.Remove(item);
                db.SaveChanges();
            }

            var invdel = db.tbl_Invoice.Find(id);
            db.tbl_Invoice.Remove(invdel);
            db.SaveChanges();



            return RedirectToAction("Orders",new {id = Session["Uid"] });
        }
    }
}