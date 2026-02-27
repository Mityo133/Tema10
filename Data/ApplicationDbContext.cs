using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Themes.Models;

namespace Themes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Themes.Models.User> Users { get; set; }
            public DbSet<Themes.Models.NoteList> NoteLists { get; set; }
            public DbSet<Themes.Models.Note> Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
             .HasMany(u => u.NotesListss)
             .WithOne(nl => nl.User)
             .HasForeignKey(nl => nl.UserId)
             .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<NoteList>()
                .HasMany(nl => nl.Notes)
                .WithOne(n => n.NoteList)
                .HasForeignKey(n => n.NoteListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
