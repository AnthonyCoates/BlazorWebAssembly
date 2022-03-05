using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TestBarberPWA.Server.Models;

namespace TestBarberPWA.Server
{
    public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=BarberDB;Trusted_Connection=true");

            return new AppDBContext(optionsBuilder.Options);
        }
    }
}
