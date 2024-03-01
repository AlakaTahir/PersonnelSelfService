using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Personnel.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonnelSelfService.Migrations
{
    public class PersonnelDatabaseContext : IdentityDbContext<AppIdentityUser>
    {
        public PersonnelDatabaseContext(DbContextOptions<PersonnelDatabaseContext> options) : base(options)
        {
        }

        public DbSet<EmployeeInfo> EmployeeInfos { get; set; }
        public DbSet<LoanInfo> LoanInfos { get; set; }
        public DbSet<LeaveInfo> LeaveInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeInfo>().ToTable("EmployeeInfos");
            modelBuilder.Entity<LoanInfo>().ToTable("LoanInfos");
            modelBuilder.Entity<LeaveInfo>().ToTable("LeaveInfos");

            base.OnModelCreating(modelBuilder);

        }
    }

}

