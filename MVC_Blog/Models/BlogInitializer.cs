using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Blog.Models
{

    public class BlogInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            List<Kategori> kategoriler = new List<Kategori>()
            {
                new Kategori(){KategoriAd="C#"},
                new Kategori(){KategoriAd="C++"},
                 new Kategori(){KategoriAd="Vs"},
                new Kategori(){KategoriAd="php"},
                                new Kategori(){KategoriAd="html"},


            };
            foreach (var item in kategoriler)
            {
                context.Kategoriler.Add(item);
            }
            context.SaveChanges();


            List<Blog> bloglar = new List<Blog>()
            {
                new Blog(){BlogAdi="yeni baslik",BlogIcerik="blabla"},
                new Blog(){BlogAdi="yeni baslik",BlogIcerik="blabla"},
                new Blog(){BlogAdi="yeni baslik",BlogIcerik="blabla"},
                new Blog(){BlogAdi="yeni baslik",BlogIcerik="blabla"},
                new Blog(){BlogAdi="yeni baslik",BlogIcerik="blabla"},
            };
            foreach (var iem in bloglar)
            {
                context.Bloglar.Add(iem);
            }

            base.Seed(context);

        }
    }
}
