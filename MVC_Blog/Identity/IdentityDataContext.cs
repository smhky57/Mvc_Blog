using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Blog.Identity;


namespace MVC_Blog.Identity
{
    public class IdentityDataContext: IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext():base("dbcontext")
        {

        }

    }
}