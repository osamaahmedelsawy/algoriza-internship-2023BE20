using core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.data
{
    public class VezeetaProjectDbContext:DbContext
    {
        public VezeetaProjectDbContext(DbContextOptions<VezeetaProjectDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());   
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specializations> Specializations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> FeedBacks { get; set; }
       

    }
}
