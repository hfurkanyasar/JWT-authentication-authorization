using APIServiceWithMVC.myAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServiceWithMVC.myAPI.DAL.Context
{
    public class hfyContext:DbContext
    {
        public hfyContext(DbContextOptions<hfyContext>context):base(context)
        {

        }
        public DbSet<Products> Products { get; set; }
    }
}
