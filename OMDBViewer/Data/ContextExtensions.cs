// <copyright file="ContextExtensions.cs" company="Andrejs Macko">
// Copyright (c) Andrejs Macko. All rights reserved. Unauthorized copying of this file, via any medium is strictly prohibited. Proprietary and confidential.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMDBViewer.Entities;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OMDBViewer.Data
{
    public static class ContextExtensions
    {
        public static void AddConvention(
                          this ModelBuilder builder,
                          Func<IMutableProperty, bool> predicate,
                          Action<PropertyBuilder> action)
        {
            var props = builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                .Where(predicate).Select(p => builder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name));

            foreach (var pb in props)
            {
                action(pb);
            }
        }

        public static PropertyBuilder ConventionMaxLength(this PropertyBuilder builder, int maxlength)
        {
            if (!builder.Metadata.GetMaxLength().HasValue)
            {
                builder.HasMaxLength(maxlength);
            }

            return builder;
        }

        public static PropertyBuilder ConventionColumnType(this PropertyBuilder builder, string columnType)
        {
            if (builder.Metadata.GetColumnType() == null)
            {
                builder.HasColumnType(columnType);
            }

            return builder;
        }

        public static PropertyBuilder ConventionIsUnicode(this PropertyBuilder builder, bool isUnicode = true)
        {
            if (!builder.Metadata.IsUnicode().HasValue)
            {
                builder.IsUnicode(isUnicode);
            }

            return builder;
        }

        public static async Task<TDest> Get<TDest>(this IQueryable<TDest> source)
        {
            var result = await source.FirstOrDefaultAsync();
            return result;
        }

        public static async Task<TDest> ById<TDest>(this IQueryable<TDest> source, long id)
            where TDest : IEntity
        {
            var result = await source.Where(x => x.Id == id).Get();
            return result;
        }

        public static void AddAssemblyConfiguration(this ModelBuilder builder, Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType
                && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
