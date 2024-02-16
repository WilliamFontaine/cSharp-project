using Microsoft.EntityFrameworkCore;
using Shared.ApiModels;

namespace cSharp_project
{
    public class ProgramContext(DbContextOptions<ProgramContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleModel>()
                .HasKey(vm => vm.Id);

            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<Maintenance>()
                .HasKey(m => m.Id);
        }
    }
}
