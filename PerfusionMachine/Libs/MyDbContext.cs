﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PerfusionMachine.Models;

namespace PerfusionMachine.Libs
{
    public class MyDbContext : DbContext
    {
        public DbSet<WashFlow> WashFlows { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=wash.db");
        }
      
    }
}
