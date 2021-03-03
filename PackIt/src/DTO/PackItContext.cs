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
    public abstract class PackItContext<TData, TDtoData, TMapper> : DbContext
        where TDtoData : class
        where TMapper : PackItMapper<TData, TDtoData>, new()
    {
        /// <summary> Create a DbContext. </summary>
        ///
        /// <param name="options"> The options for this context. </param>
        public PackItContext([NotNullAttribute] DbContextOptions options)
            : base(options)
        {
            Resources = this.Set<TDtoData>();
            Mapper = new TMapper();
        }

        /// <summary> Gets the resources. </summary>
        ///
        /// <value> The resources. </value>
        public DbSet<TDtoData> Resources { get; private set; }

        /// <summary> Gets the resources. </summary>
        ///
        /// <value> The resources. </value>
        public PackItMapper<TData, TDtoData> Mapper { get; private set; }

        /// <summary>Construct default query.</summary>
        ///
        /// <returns> Query for list of TDtoData. </returns>
        protected abstract IQueryable<TDtoData> ConstructQuery();

        /// <summary> Gets all data. </summary>
        ///
        /// <returns> The data. </returns>
        public IList<TData> GetAll()
        {
            var ret = new List<TData>();
            var query = ConstructQuery();

            foreach (var item in query)
            {
                ret.Add(this.Mapper.ConvertToData(item));
            }

            return ret;
        }

        /// <summary> Add a data item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void Add(TData item)
        {
            var dto = this.Mapper.ConvertToDto(item);
            this.Resources.Add(dto);
        }

        /// <summary> Removes a data item. </summary>
        ///
        /// <param name="key"> The key. </param>
        public void Remove(string key)
        {
            var entity = this.Resources.Find(key);
            this.Resources.Remove(entity);
        }

        /// <summary> Searches for the first data item. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> The found material. </returns>
        public abstract TData Find(string key);

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
