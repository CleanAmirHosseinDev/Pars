using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Domain.Entities.BasicInfo;
using ParsKyanCrm.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Contexts
{
    public interface IDataBaseContext
    {
        DbSet<City> City { get; set; }

        DbSet<State> States { get; set; }

        DbSet<Users> Users { get; set; }

        DbSet<UserRoles> UserRoles { get; set; }

        DbSet<Roles> Roles { get; set; }

        DbSet<Customers> Customers { get; set; }

        DbSet<SystemSeting> SystemSetings { get; set; }

        DbSet<RequestForReating> RequestForReating { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }

    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }

        public virtual DbSet<City> City { get; set; }

        public virtual DbSet<State> States { get; set; }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<UserRoles> UserRoles { get; set; }

        public virtual DbSet<Roles> Roles { get; set; }

        public virtual DbSet<SystemSeting> SystemSetings { get; set; }

        public virtual DbSet<RequestForReating> RequestForReating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customers>(entity =>
            {

                entity.HasOne(d => d.HowGetKnowCompany)
                    .WithMany(p => p.CustomerHowGetKnowCompanies)
                    .HasForeignKey(d => d.HowGetKnowCompanyID);

                entity.HasOne(d => d.KindOfCompany)
                    .WithMany(p => p.CustomerKindOfCompanies)
                    .HasForeignKey(d => d.KindOfCompanyID);

                entity.HasOne(d => d.TypeServiceRequested)
                    .WithMany(p => p.CustomerTypeServiceRequesteds)
                    .HasForeignKey(d => d.TypeServiceRequestedID);
            });

        }

    }
}
