using ClassListWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassListWPF.Persistence
{
    public class ClassListDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<Pupil> Pupils { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\mssqllocaldb; Initial Catalog = ClassListDB; Integrated Security = True");
        }
    }
}