using Microsoft.EntityFrameworkCore;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB;database=BarberDB;Trusted_Connection=true");
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ServiceSold> ServicesSold { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed People table
            modelBuilder.Entity<Person>().HasData(new Person
            {
                PersonID = 1,
                Forename = "Aaron",
                Surname = "Aaronson",
                DateOfBirth = new DateTime(1991, 01, 01),
                Gender = Gender.Male,
                PhoneNo = "01234567890",
                Email = "a.aaronson@aaronmail.com",
                IsEmployee = false
            });

            modelBuilder.Entity<Person>().HasData(new Person
            {
                PersonID = 2,
                Forename = "Betty",
                Surname = "Bettyson",
                DateOfBirth = new DateTime(1992, 02, 02),
                Gender = Gender.Female,
                PhoneNo = "09876543210",
                Email = "b.bettyson@bettymail.com",
                IsEmployee = true
            });

            // Seed Addresses table
            modelBuilder.Entity<Address>().HasData(new Address
            {
                AddressID = 1,
                PersonID = 1,
                LineOne = "1 A Street",
                LineTwo = "Somewhere",
                Town = "Testville",
                PostCode = "AA11 1AA"
            });

            modelBuilder.Entity<Address>().HasData(new Address
            {
                AddressID = 2,
                PersonID = 2,
                LineOne = "2 B Street",
                LineTwo = "Somewhere",
                Town = "Testville",
                PostCode = "BB22 2BB"
            });

            // Seed Services table
            modelBuilder.Entity<Service>().HasData(new Service
            {
                ServiceID = 1,
                Name = "Hair Cut",
                Description = "A standard, dry hair cut.",
                Price = 14.99f
            });

            modelBuilder.Entity<Service>().HasData(new Service
            {
                ServiceID = 2,
                Name = "Shave",
                Description = "A standard, wet shave.",
                Price = 5.99f
            });

            // Seed Appointments table
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 1,
                EmployeeID = 2,
                CustomerID = 1,
                DateTime = new DateTime(2022, 01, 01, 15, 30, 00),
                Notes = "Someone having a full shave and hair cut."
            });

            // Seed ServicesSold table
            modelBuilder.Entity<ServiceSold>().HasKey(ss => new { ss.AppointmentID, ss.ServiceID }); // Create compound key

            modelBuilder.Entity<ServiceSold>().HasData(new ServiceSold
            {
                AppointmentID = 1,
                ServiceID = 1,
                Quantity = 1,
                SubTotal = 14.99f
            });

            modelBuilder.Entity<ServiceSold>().HasData(new ServiceSold
            {
                AppointmentID = 1,
                ServiceID = 2,
                Quantity = 1,
                SubTotal = 5.99f
            });
        }
    }
}
