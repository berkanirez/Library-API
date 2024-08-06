using System;
using LibraryAPI2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace LibraryAPI2.Data
{
	public class ApplicationContext : IdentityDbContext<ApplicationUser>
	{
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Language>? Languages { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<SubCategory>? SubCategories { get; set; }
        public DbSet<Publisher>? Publishers { get; set; }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<AuthorBook>? AuthorBook { get; set; }
        public DbSet<Member>? Members { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<BookLanguage>? BookLanguage { get; set; }
        public DbSet<BookSubCategory>? BookSubCategory { get; set; }
        public DbSet<BookCopy>? BookCopy { get; set; }
        public DbSet<Borrow>? Borrow { get; set; }
        public DbSet<Department>? Department { get; set; }
        public DbSet<Voting>? Votes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AuthorBook>().HasKey(a => new { a.AuthorsId, a.BooksId });
            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorsId);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BooksId);

            //.....
            modelBuilder.Entity<BookLanguage>().HasKey(a => new { a.BooksId , a.LanguagesCode});
            modelBuilder.Entity<BookLanguage>()
                .HasOne(ab => ab.Language)
                .WithMany(a => a.BookLanguages)
                .HasForeignKey(ab => ab.LanguagesCode);

            modelBuilder.Entity<BookLanguage>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.BookLanguages)
                .HasForeignKey(ab => ab.BooksId);

            //.....
            modelBuilder.Entity<BookSubCategory>().HasKey(a => new { a.BooksId, a.SubCategoriesId });
            modelBuilder.Entity<BookSubCategory>()
                .HasOne(ab => ab.SubCategory)
                .WithMany(a => a.BookSubCategories)
                .HasForeignKey(ab => ab.SubCategoriesId);

            modelBuilder.Entity<BookSubCategory>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.BookSubCategories)
                .HasForeignKey(ab => ab.BooksId);
            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Employee)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

        }




        




        
    }
}

