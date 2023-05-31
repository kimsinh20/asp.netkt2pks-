using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kt2_lan2.Models;
using Database = kt2_lan2.Models.Database;

namespace kt2_lan2.Controllers
{
    public class ProductsController : Controller
    {
        private Database db = new Database();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        } 
        [Route("shop/danhmuc/{Categoryid?}")]
        public ActionResult ProductByCt(int Categoryid)
        {
            return View(db.Products.Where(t=>t.Categoryid == Categoryid).ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pid,Categoryid,ProdName,MetaTitle,Description,ImagePath,Price,File")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                      product.ImagePath = "flower (10).jpg";
                    var f = Request.Files["ImagePath"];
                    if (f != null && f.ContentLength > 0)
                    {
                        String fileName = System.IO.Path.GetFileName(f.FileName);
                        String uploadPath = Server.MapPath("~/Content/Images/" + fileName);
                        f.SaveAs(uploadPath);
                        product.ImagePath = fileName;
                    }
                    db.Products.Add(product);
                    db.SaveChanges();
         
                }
                 return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                ViewBag.error = ex.Message;
                return View(product);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pid,Categoryid,ProdName,MetaTitle,Description,ImagePath,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
