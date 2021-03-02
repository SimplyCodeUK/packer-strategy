// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary> A context. </summary>
    public abstract class PackItContext<TData, TDtoData> : DbContext
                                                           where TDtoData : class
    {
        /// <summary> Create a DbContext. </summary>
        ///
        /// <param name="options"> The options for this context. </param>
        public PackItContext([NotNullAttribute] DbContextOptions options)
            : base(options)
        {
            Resources = this.Set<TDtoData>();
        }

        /// <summary> Gets the resources. </summary>
        ///
        /// <value> The resources. </value>
        public DbSet<TDtoData> Resources { get; private set; }

        /// <summary> Gets all data. </summary>
        ///
        /// <returns> The data. </returns>
        public abstract IList<TData> GetAll();

        /// <summary> Add a data item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public abstract void Add(TData item);

        /// <summary> Searches for the first data item. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> The found material. </returns>
        public abstract TData Find(string key);

        /// <summary> Removes a data item. </summary>
        ///
        /// <param name="key"> The key. </param>
        public void Remove(string key)
        {
            var entity = this.Resources.Find(key);
            this.Resources.Remove(entity);
        }

        /// <summary> Updates a data item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public abstract void Update(TData item);

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="table">The table name.</param>
        /// <param name="keyExpression">The table key expression.</param>
        ///
        /// <returns> A builder. </returns>
        protected static Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TDto> Configure<TDto>(ModelBuilder modelBuilder, string table, [NotNullAttribute] Expression<Func<TDto, object>> keyExpression) where TDto : class
        {
            var builder = modelBuilder.Entity<TDto>();
            builder.ToTable(table);
            builder.HasKey(keyExpression);
            return builder;
        }
    }
}
