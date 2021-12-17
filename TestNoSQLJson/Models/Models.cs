using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestNoSQLJson.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(b => b._Tags).HasColumnName("Tags");

            modelBuilder.Entity<Blog>()
                .Property(b => b._Owner).HasColumnName("Owner");
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }

        [Url]
        public string Url { get; set; }

        public List<Post> Posts { get; set; }

        internal string _Tags { get; set; }

        [NotMapped]
        public string[] Tags
        {
            get { return _Tags == null ? null : JsonConvert.DeserializeObject<string[]>(_Tags); }
            set { _Tags = JsonConvert.SerializeObject(value); }
        }

        internal string _Owner { get; set; }

        [NotMapped]
        public Person Owner
        {
            get { return _Owner == null ? null : JsonConvert.DeserializeObject<Person>(_Owner); }
            set 
            {
                value.Version = Constants.CURRENT_VERSION;
                _Owner = JsonConvert.SerializeObject(value); 
            }
        }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }

    public class Person
    {

        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string CivicAddress { get; set; }
        public string Gender { get; set; }
        public decimal Version { get; set; }
    }
}