using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class ParsKyanCrmDBContext : DbContext
    {
        public ParsKyanCrmDBContext()
        {
        }

        public ParsKyanCrmDBContext(DbContextOptions<ParsKyanCrmDBContext> options)
            : base(options)
        {
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string cs =  "data source=77.238.123.197;initial catalog=ParsKyanCrmDB;user id=vam30;password=10155;MultipleActiveResultSets=True";
               // string cs = "data source=172.16.19.9;initial catalog=ParsKyanCrmDB;user id=pars;password=pars@10155;MultipleActiveResultSets=True";
                optionsBuilder.UseSqlServer(cs);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
