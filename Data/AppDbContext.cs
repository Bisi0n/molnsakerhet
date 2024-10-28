﻿using Microsoft.EntityFrameworkCore;
using molnsakerhet.Models;

namespace molnsakerhet.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
    }
}