using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Blog.Models;

namespace MVC_Blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult List(int? id, string q)
        {
            var bloglar = db.Bloglar.Where(i => i.Onay == true)
               .Select(i => new BlogModel()
               {
                   Id = i.Id,
                   BlogAdi = i.BlogAdi.Length > 100 ? i.BlogAdi.Substring(0, 100) + "..." : i.BlogAdi,
                   BlogKonu = i.BlogKonu,
                   Resim = i.Resim,
                   EklenmeTarihi = i.EklenmeTarihi,
                   Onay = i.Onay,
                   KategoriId = i.KategoriId,
                }).AsQueryable();

            if (string.IsNullOrEmpty("q") == false)
            {
                bloglar = bloglar.Where(i => i.BlogAdi.Contains(q) || i.BlogKonu.Contains(q));
            }

            if (id!=null)
            {
                bloglar = bloglar.Where(i => i.KategoriId == id);
            }


            return View(bloglar.ToList());
        
        }

        // GET: Blog
        public ActionResult Index()
        {
            var bloglar = db.Bloglar.Include(b => b.Kategori).OrderByDescending(i=>i.EklenmeTarihi);
            return View(bloglar.ToList());
        }
        [Authorize]

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        [Authorize]

        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAd");
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BlogAdi,BlogKonu,BlogIcerik,Resim,Onay,EklenmeTarihi,KategoriId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.EklenmeTarihi = DateTime.Now;
                db.Bloglar.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAd", blog.KategoriId);
            return View(blog);
        }
        [Authorize]

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAd", blog.KategoriId);
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BlogAdi,BlogKonu,BlogIcerik,Resim,Onay,KategoriId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                var entity = db.Bloglar.Find(blog.Id);
                    if(entity!=null)
                {
                    entity.BlogAdi = blog.BlogAdi;
                    entity.BlogIcerik = blog.BlogIcerik;
                    entity.BlogKonu = blog.BlogKonu;
                    entity.KategoriId = blog.KategoriId;
                    entity.Resim = blog.Resim;
                    entity.Onay = blog.Onay;
                    TempData["Blog"] = entity;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAd", blog.KategoriId);
            return View(blog);
        }
        [Authorize]

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Bloglar.Find(id);
            db.Bloglar.Remove(blog);
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
