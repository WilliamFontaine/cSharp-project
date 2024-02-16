using Microsoft.EntityFrameworkCore;
using Shared.ApiModels;

namespace cSharp_project
{
    public class ProgramContext(DbContextOptions<ProgramContext> options) : DbContext(options)
    {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Vehicle>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Maintenance>()
                .HasKey(x => x.Id);

        }
    }
}
