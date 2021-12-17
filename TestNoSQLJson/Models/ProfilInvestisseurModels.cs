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
    public class ProfilsInvestisseurContext : DbContext
    {
        public ProfilsInvestisseurContext(DbContextOptions<ProfilsInvestisseurContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfilsInvestisseur>()
                .Property(b => b._Content).HasColumnName("Content");

            modelBuilder.Entity<Labels>()
                .HasKey(l => new { l.FieldName, l.Version });

            //modelBuilder.Entity<Blog>()
            //    .Property(b => b._Owner).HasColumnName("Owner");
        }

        public DbSet<ProfilsInvestisseur> ProfilsInvestisseur { get; set; }
        public DbSet<Labels> Labels { get; set; }
    }

    public class ProfilsInvestisseur
    {
        public int ProfilsInvestisseurId { get; set; }
        internal string _Content { get; set; }

        [NotMapped]
        public ProfilLine[] Content
        {
            get { return _Content == null ? null : JsonConvert.DeserializeObject<ProfilLine[]>(_Content); }
            set { _Content = JsonConvert.SerializeObject(value); }
        }
    }

    public class ProfilLine
    {
        //public int FieldName { get; set; }
        //public decimal Version { get; set; }
        public string LabelText { get; set; }
        public object Value { get; set; }

        public Type Type { get; set; }
    }

    public class Labels
    {
        public string FieldName { get; set; }
        public decimal Version { get; set; }

        [Required]
        public string Text { get; set; }
    }
}


