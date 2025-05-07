// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.DTO
{
    using System.Collections.Generic;

    /// <summary> A data repository. </summary>
    ///
    /// <typeparam name="TData"> The type of the data. </typeparam>
    /// <typeparam name="TDtoData"> The type of the data transfer object. </typeparam>
    /// <typeparam name="TMapper"> Data to/from DTO mapper. </typeparam>
    public class Repository<TData, TDtoData, TMapper>
        where TData : class
        where TDtoData : class
        where TMapper : IPackItMapper<TData, TDtoData>, new()
    {
        /// <summary> The context. </summary>
        private readonly PackItContext<TData, TDtoData, TMapper> context;

        /// <summary>
        /// Initialises a new instance of the <see cref="Repository{TData, TDtoData, TMapper}" /> class.
        /// </summary>
        ///
        /// <param name="context"> The context. </param>
        public Repository(PackItContext<TData, TDtoData, TMapper> context)
        {
            this.context = context;
        }

        /// <summary> Gets all items in this collection. </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        public IList<TData> GetAll()
        {
            return this.context.GetAll();
        }

        /// <summary> Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        public void Add(TData item)
        {
            this.context.Add(item);
            this.context.SaveChanges();
        }

        /// <summary> Searches for the first match for the given string. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> A Material. </returns>
        public TData Find(string key)
        {
            return this.context.Find(key);
        }

        /// <summary> Removes the given key. </summary>
        ///
        /// <param name="key"> The key. </param>
        public void Remove(string key)
        {
            this.context.Remove(key);
            this.context.SaveChanges();
        }

        /// <summary> Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        public void Update(TData item)
        {
            this.context.Update(item);
            this.context.SaveChanges();
        }
    }
}
