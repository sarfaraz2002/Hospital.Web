using Hospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class ApplicationDbContext:IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<HospitalInfo> HospitalInfos { get; set; }
        public DbSet<Room> Rooms  { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<PrescribedMedicine> PrescribedMedicines{ get; set; }
        public DbSet<Contact> Contacts { get; set; }
 
    }
}
