using GymSys.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GymSys.Data;

namespace GymSys.Data
{
    public class GymSysContext : DbContext
    {
        public GymSysContext()
        {

        }


        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientCard> ClientCards { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Town> Towns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=.;Database=GymSystem;Integrated Security=true");

            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientCard>(x =>
            {
                x.HasKey(x => new { x.CardId, x.ClientId });
            }) ;

            base.OnModelCreating(modelBuilder);
        }

    }
}
