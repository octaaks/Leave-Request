using Leave_Request.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ManagerFill> ManagerFills { get; set; }
        public DbSet<Religion> Religions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Employee --< LeaveRequest
            modelBuilder.Entity<Employee>()
                .HasMany(lr => lr.LeaveRequests)
                .WithOne(em => em.Employee);

            //Religion --< Employee
            modelBuilder.Entity<Religion>()
                .HasMany(em => em.Employees)
                .WithOne(rel => rel.Religion);

            //Job --< Employee
            modelBuilder.Entity<Job>()
                .HasMany(em => em.Employees)
                .WithOne(j => j.Job);

            //Status --< LeaveRequest
            modelBuilder.Entity<Status>()
                .HasMany(lr => lr.LeaveRequests)
                .WithOne(st => st.Status);

            //LeaveType --< LeaveRequest
            modelBuilder.Entity<LeaveType>()
                .HasMany(lr => lr.LeaveRequests)
                .WithOne(lt => lt.LeaveType);

            //LeaveRequest --< ManagerFill
            modelBuilder.Entity<LeaveRequest>()
                .HasMany(mf => mf.ManagerFills)
                .WithOne(lr => lr.LeaveRequest);

            //Employee --- Acount
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(em => em.Employee)
                .HasForeignKey<Account>(a => a.Id);

            //Account >---< Role
            modelBuilder.Entity<AccountRole>()
               .HasKey(ar => new { ar.AccountId, ar.RoleId });
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(a => a.AccountRoles)
                .HasForeignKey(ar => ar.AccountId);
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Role)
                .WithMany(r => r.AccountRoles)
                .HasForeignKey(ar => ar.RoleId);
        } 
    }
}