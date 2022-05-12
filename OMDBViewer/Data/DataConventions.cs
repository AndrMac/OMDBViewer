// <copyright file="DataConventions.cs" company="Andrejs Macko">
// Copyright (c) Andrejs Macko. All rights reserved. Unauthorized copying of this file, via any medium is strictly prohibited. Proprietary and confidential.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using System.Reflection;

namespace OMDBViewer.Data
{
    public static class DataConventions
    {
        public static ModelBuilder AddConventions(this ModelBuilder builder)
        {
            builder.AddConvention(x => x.ClrType == typeof(decimal), b => b.HasColumnType("decimal(18,2)"));

            // Review the following conventions to match the business needs
            builder.AddConvention(
                x => x.Name.ToLower() == "title" && x.ClrType == typeof(string),
                b => b.HasMaxLength(50).IsUnicode(false));

            builder.AddConvention(
                x => x.Name.ToLower() == "Comment" && x.ClrType == typeof(string),
                b => b.HasMaxLength(50).IsUnicode(false));

            builder.AddConvention(
                x => x.Name.ToLower() == "name" && x.ClrType == typeof(string),
                b => b.HasMaxLength(150).IsUnicode(true));

            builder.AddConvention(
                x => x.Name.ToLower() == "releasedate" && x.ClrType == typeof(string),
                b => b.HasMaxLength(150).IsUnicode(true));

            builder.AddConvention(
                x => x.ClrType == typeof(string) && x.Name.ToLower() == "email",
                p => p.ConventionMaxLength(100).ConventionIsUnicode(false));

            builder.AddConvention(
              x => x.ClrType == typeof(DateTime),
              b => b.HasColumnType("datetime2"));

            builder.AddConvention(
            x => x.ClrType == typeof(DateTime?),
            b => b.HasColumnType("datetime2"));

            builder.AddConvention(
                x => x.ClrType == typeof(decimal),
                p => p.ConventionColumnType("decimal(18,2)"));

            builder.AddConvention(
                x => x.ClrType == typeof(decimal?),
                p => p.ConventionColumnType("decimal(18,2)"));

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                        .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType
                        && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                        .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            return builder;
        }

        public static PropertyBuilder<string> Name(this PropertyBuilder<string> builder)
        {
            builder.HasMaxLength(150).IsUnicode(true);

            return builder;
        }
    }
}
