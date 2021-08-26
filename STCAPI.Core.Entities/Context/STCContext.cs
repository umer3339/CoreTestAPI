using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STCAPI.Core.Entities.STCVAT;

namespace STCAPI.Core.Entities.Context
{
    public class STCContext:DbContext
    {
        private readonly string _connectionString;

        public STCContext(IConfiguration configuration) {
            _connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString,ServerVersion.AutoDetect(_connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DemoConfiguration());
            modelBuilder.ApplyConfiguration(new STCVATConfiguration());
        }
         
        public virtual DbSet<STCVATForm> STCVATForms { get; set; }
    }
}
