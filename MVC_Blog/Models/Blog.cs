using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Blog.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string BlogAdi { get; set; }
        public string BlogKonu { get; set; }
        public string BlogIcerik { get; set; }
        public string Resim { get; set; }
        public bool Onay { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public int KategoriId { get; set; }

        public Kategori Kategori { get; set; }





    }
}