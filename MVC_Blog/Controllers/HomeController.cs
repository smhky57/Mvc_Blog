using MVC_Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext Context = new BlogContext();
        // GET: Home
        public ActionResult Index()
        {
            var bloglar = Context.Bloglar
                .Select(i => new BlogModel()
                {
                    Id=i.Id,
                    BlogAdi = i.BlogAdi.Length>100? i.BlogAdi.Substring(0,100)+"...": i.BlogAdi,
                    BlogKonu=i.BlogKonu,
                    Resim=i.Resim,
                    EklenmeTarihi=i.EklenmeTarihi,
                    Onay=i.Onay,
                })
                .Where(i => i.Onay == true);


            return View(bloglar.ToList());
        }
    }
}