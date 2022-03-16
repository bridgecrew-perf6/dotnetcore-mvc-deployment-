using System;
using Microsoft.EntityFrameworkCore;

namespace dacon_exam.Models
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){

        }

        public DbSet<Info> infos {get; set;}
        public DbSet<Cml> Cmls {get; set;}
        public DbSet<TestPoint> TestPoints {get; set;}
        public DbSet<Thickness> Thicknesses {get; set;}

        protected override void OnModelCreating(ModelBuilder builder){

            
            base.OnModelCreating(builder);
        }


    }
}