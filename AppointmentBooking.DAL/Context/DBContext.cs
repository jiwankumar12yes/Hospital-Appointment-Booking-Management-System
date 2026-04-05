using AppointmentBooking.DAL.Entity;
using AppointmentBooking.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.DAL.Context
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(AppConfig.ConnectionString);
            optionsBuilder.UseSqlServer("Data Source=dell,51434;Initial Catalog=Jiwan;Persist Security Info=True;User ID=Jiwan;Password=Jiwan;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=\"SQL Server Management Studio\";Command Timeout=0");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasIndex(x => x.Email)
                .IsUnique();

                entity.Property(x => x.Password).IsRequired();

                entity.HasData(
                    new User { Id = 1, Email = "jiwan@gmail.com", Password = "Jiwan@123", IsAdmin = false },
                    new User { Id = 2, Email = "shyam@gmail.com", Password = "Shyam@123", IsAdmin = true },
                    new User { Id = 3, Email = "mohan@gmail.com", Password = "Mohan@123", IsAdmin = false },
                    new User { Id = 4, Email = "Raju@gmail.com", Password = "Raju@123", IsAdmin = true },
                    new User { Id = 5, Email = "raja@gmail.com", Password = "Raja@123", IsAdmin = false },
                    new User { Id = 6, Email = "charli@gmail.com", Password = "Charli@123", IsAdmin = true },
                    new User { Id = 7, Email = "ranga@gmail.com", Password = "Ranga@123", IsAdmin = false },
                    new User { Id = 8, Email = "fortis@gmail.com", Password = "Fortis@123", IsAdmin = true },
                    new User { Id = 9, Email = "chabra@gmail.com", Password = "Chabra@123", IsAdmin = true }
                    );
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(e => e.User)
                .WithOne(u => u.Patient)
                .HasForeignKey<Patient>(e => e.UserId);

                entity.HasData(new Patient { Id = 1, Name = "Mohan", UserId = 3, Age = 20, PhoneNumber = "99999999999" });
                entity.HasData(new Patient { Id = 2, Name = "Jiwan", UserId = 1, Age = 22, PhoneNumber = "99999999999" });
                entity.HasData(new Patient { Id = 3, Name = "raja", UserId = 5, Age = 28, PhoneNumber = "99999999999" });
                entity.HasData(new Patient { Id = 4, Name = "ranga", UserId = 7, Age = 20, PhoneNumber = "99999999999" });

            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(x => x.Id);

                //one to one 
                entity.HasOne(e => e.User)
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(e => e.UserId);

                entity.HasData(
                    new Doctor { Id = 1, Name = "Shyam", UserId = 2, Specialization = Shared.Specialization.General, ConsultationFee = 500.0m, AvailableDays = new List<Shared.WeekDays> { Shared.WeekDays.Monday, Shared.WeekDays.Wednesday } },
                    new Doctor { Id = 2, Name = "Raju", UserId = 4, Specialization = Shared.Specialization.Dermatology, ConsultationFee = 1500.0m, AvailableDays = new List<Shared.WeekDays> { Shared.WeekDays.Tuesday, Shared.WeekDays.Friday } },
                    new Doctor { Id = 3, Name = "charli", UserId = 6, Specialization = Shared.Specialization.Neurology, ConsultationFee = 500.0m, AvailableDays = new List<Shared.WeekDays> { Shared.WeekDays.Monday, Shared.WeekDays.Wednesday } },
                    new Doctor { Id = 4, Name = "Fortis", UserId = 8, Specialization = Shared.Specialization.Orthopedics, ConsultationFee = 300.0m, AvailableDays = new List<Shared.WeekDays> { Shared.WeekDays.Monday, Shared.WeekDays.Friday } },
                    new Doctor { Id = 5, Name = "Chabra", UserId = 9, Specialization = Shared.Specialization.Dermatology, ConsultationFee = 800.0m, AvailableDays = new List<Shared.WeekDays> { Shared.WeekDays.Monday, Shared.WeekDays.Saturday } }
                    );
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(x => x.Id);

                //one to many
                entity.HasOne(e => e.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
                //.IsRequired();

                //one to many
                entity.HasOne(e => e.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(e => e.DoctorId)
               .OnDelete(DeleteBehavior.NoAction);
                //.IsRequired();

                entity.Property(e => e.Status).HasDefaultValue(Status.Scheduled);
            });
        }
    }
}
