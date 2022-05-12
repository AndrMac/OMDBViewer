// <copyright file="AppDataContext.Routines.cs" company="Andrejs Macko">
// Copyright (c) Andrejs Macko. All rights reserved. Unauthorized copying of this file, via any medium is strictly prohibited. Proprietary and confidential.
// </copyright>

using Microsoft.EntityFrameworkCore;
using OMDBViewer.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OMDBViewer.Data
{
    public partial class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options)
          : base(options)
        {
        }

        public static string Schema => "dbo";

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var e in this.ChangeTracker.Entries())
            {
                if (!(e.Entity is EntityBase))
                {
                    continue;
                }

                var entity = e.Entity as EntityBase;

                switch (e.State)
                {
                    case EntityState.Added:
                        entity.Created = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.Updated = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.AddConventions();
            modelBuilder.AddAssemblyConfiguration(this.GetType().Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
