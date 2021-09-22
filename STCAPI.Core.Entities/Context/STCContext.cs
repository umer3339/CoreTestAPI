using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STCAPI.Core.Entities.AdminPortal;
using STCAPI.Core.Entities.Reconcilation;
using STCAPI.Core.Entities.STCVAT;
using STCAPI.Core.Entities.UserManagement;

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
        public virtual DbSet<STCPostValidation> STCPostValidations { get; set; }
        public virtual DbSet<StageModel> StageModels { get; set; }
        public virtual DbSet<MainLevel> MainLevels { get; set; }
        public virtual DbSet<StreamModel> StreamModels { get; set; }
        public virtual DbSet<RecincilationSummary> RecincilationSummaries { get; set; }
        public virtual DbSet<PortalAccess> PortalAccesses { get; set; }
    }
}
