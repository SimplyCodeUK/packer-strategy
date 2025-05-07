// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    /// <summary> A context. </summary>
    ///
    /// <typeparam name="TData"> The type of the data. </typeparam>
    /// <typeparam name="TDtoData"> The type of the data transfer object. </typeparam>
    /// <typeparam name="TMapper"> Data to/from DTO mapper. </typeparam>
    public abstract class PackItContext<TData, TDtoData, TMapper> : DbContext
        where TData : class
        where TDtoData : class
        where TMapper : IPackItMapper<TData, TDtoData>, new()
    {
        /// <summary> Create a DbContext. </summary>
        ///
        /// <param name="options"> The options for this context. </param>
        protected PackItContext([NotNullAttribute] DbContextOptions options)
            : base(options)
        {
            this.Resources = this.Set<TDtoData>();
            this.Mapper = new TMapper();
        }

        /// <summary> Gets the resources. </summary>
        ///
        /// <value> The resources. </value>
        public DbSet<TDtoData> Resources { get; private set; }

        /// <summary> Gets the resources. </summary>
        ///
        /// <value> The resources. </value>
        public IPackItMapper<TData, TDtoData> Mapper { get; private set; }

        /// <summary>Construct default query.</summary>
        ///
        /// <returns> Query for list of TDtoData. </returns>
        protected abstract IQueryable<TDtoData> ConstructQuery();

        /// <summary>Construct a find task.</summary>
        ///
        /// <param name="key"> The key to search for. </param>
        ///
        /// <returns> The find task. </returns>
        protected abstract System.Threading.Tasks.Task<TDtoData> ConstructFindTask(string key);

        /// <summary> Gets all data. </summary>
        ///
        /// <returns> The data. </returns>
        public IList<TData> GetAll()
        {
            var ret = new List<TData>();
            var query = this.ConstructQuery();

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
        public TData Find(string key)
        {
            try
            {
                var query = this.ConstructFindTask(key);
                query.Wait();
                return this.Mapper.ConvertToData(query.Result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Updates a data item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void Update(TData item)
        {
            this.Remove(this.Mapper.KeyForData(item));
            this.SaveChanges();

            var dto = this.Mapper.ConvertToDto(item);
            this.Resources.Add(dto);
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="table">The table name.</param>
        /// <param name="keyExpression">The table key expression.</param>
        ///
        /// <returns> A builder. </returns>
        protected static Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TDto> Configure<TDto>(ModelBuilder modelBuilder, string table, [NotNullAttribute] Expression<Func<TDto, object>> keyExpression)
            where TDto : class
        {
            var builder = modelBuilder.Entity<TDto>();
            builder.ToTable(table);
            builder.HasKey(keyExpression);
            return builder;
        }
    }
}
