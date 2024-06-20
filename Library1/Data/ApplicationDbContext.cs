using Library1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Reflection.Emit;

namespace Library1.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Report)
                .WithOne(r => r.Book)
                .HasForeignKey<Report>(r => r.Book_Id);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Report)
                .WithOne(r => r.Student)
                .HasForeignKey<Report>(r => r.Student_Id);

            base.OnModelCreating(modelBuilder);
        }

    }
}