using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNoSQLJson.Models
{
    public class ProfilInvestisseurContext : DbContext
    {
        public ProfilInvestisseurContext(DbContextOptions<ProfilInvestisseurContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfilInvestisseur>()
                .Property(b => b._Content).HasColumnName("Content");

            modelBuilder.Entity<ProfilInvestisseur>()
                .HasKey(p => new { p.ProfilInvestisseurId });

            modelBuilder.Entity<Label>()
                .HasKey(l => new { l.FieldName, l.Version });

            modelBuilder.Entity<Subscriber>()
                .HasKey(b => b.SubscriberId);

            modelBuilder.Entity<ProfilInvestisseur>()
                .HasOne(p => p.Subscriber);
        }

        public DbSet<ProfilInvestisseur> ProfilInvestisseur { get; set; }
        public DbSet<Subscriber> Subscriber { get; set; }
        public DbSet<Label> Labels { get; set; }
    }

    public class ProfilInvestisseur
    {
        public int ProfilInvestisseurId { get; set; }
        public Subscriber Subscriber { get; set; }
        internal string _Content { get; set; }

        [NotMapped]
        public ProfilLine[] Content
        {
            get { return _Content == null ? null : JsonConvert.DeserializeObject<ProfilLine[]>(_Content); }
            set { _Content = JsonConvert.SerializeObject(value); }
        }
    }

    public class Subscriber
    {
        public int SubscriberId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

        public class ProfilLine
    {
        //public int FieldName { get; set; }
        //public decimal Version { get; set; }
        public string LabelText { get; set; }
        public object Value { get; set; }

        public Type Type { get; set; }
    }

    public class Label
    {
        public string FieldName { get; set; }
        public decimal Version { get; set; }

        [Required]
        public string Text { get; set; }
    }
}


