using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContactsDemo.Services.Model.Contacts;
using Microsoft.EntityFrameworkCore;

namespace ContactsDemo.Services
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactNumber> ContactNumbers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("User ID =postgres;Password=12345;Server=localhost;Port=5432;Database=contactDb;Integrated Security=true;Pooling=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasIndex(p => new { p.Firstname, p.Lastname, p.Address })
                .IsUnique();

            modelBuilder.Entity<Contact>().HasQueryFilter(x => x.Status != ContactsDemo.Services.Model.Status.Deleted);
            modelBuilder.Entity<ContactNumber>().HasQueryFilter(x => x.Status != ContactsDemo.Services.Model.Status.Deleted);
        }

        public override int SaveChanges()
        {
            UpdateDates();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateDates();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateDates()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["DateCreated"] = DateTime.Now;
                        entry.CurrentValues["DateModified"] = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues["DateModified"] = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DateModified"] = DateTime.Now;
                        break;
                }
            }
        }
    }
}
