using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SCADA.RTDB.Adaptation
{
    public class DeviceMapContext : DbContext
    {
        public IDbSet<DeviceMapMode> DeviceMapModeSet { get; set; }
        public DeviceMapContext(string dbNameOrConnectingString = "VariableDB", bool isLoadData = true)
            : base(dbNameOrConnectingString)
        {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DeviceMapContext>());

            DeviceMapModeSet.Load();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceMapMode>().Ignore(p => p.TimeSpace);
            modelBuilder.Entity<DeviceMapMode>().Ignore(p => p.Value);
            modelBuilder.Entity<DeviceMapMode>().HasKey(p => p.DeviceModeId);
            base.OnModelCreating(modelBuilder);
        }

        public void Save()
        {
            base.SaveChanges();
        }
    }
}