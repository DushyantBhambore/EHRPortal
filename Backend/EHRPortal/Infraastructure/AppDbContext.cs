using App.Core.Interface;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infraastructure
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Otp> Otp { get; set; }

        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserType> UserType { get; set; }
        public DbSet<Specialisation> Specialisation { get; set; }

        public IDbConnection GetConnection()
        {
            return this.Database.GetDbConnection();
        }
    }
}
