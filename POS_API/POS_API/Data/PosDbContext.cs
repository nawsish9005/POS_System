using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace POS_API.Data
{
    public class PosDbContext: DbContext
    {
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options) { }

        //public DbSet<Doctor> Doctors { get; set; }
        //public DbSet<Patient> Patients { get; set; }
        //public DbSet<Appointment> Appointments { get; set; }
        //public DbSet<MedicalRecord> MedicalRecords { get; set; }
        //public DbSet<Prescription> Prescriptions { get; set; }
        //public DbSet<MedicineInfo> MedicineInfos { get; set; }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
    }
}
