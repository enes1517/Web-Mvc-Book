using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using web.Models;

namespace web.Repositories
{
    public class RepositoryContext:DbContext
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions options):base(options) { }

        public DbSet<Users> User { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define one-to-many relationship between Product and Comments
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ProductId);
        }

    }
}
