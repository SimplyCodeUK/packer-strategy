// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;

    /// <summary> A context. </summary>
    public interface IContext<TData>
    {
        /// <summary> Gets all data. </summary>
        ///
        /// <returns> The data. </returns>
        public IList<TData> GetAll();

        /// <summary> Add a data item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void Add(TData item);

        /// <summary> Searches for the first data item. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> The found material. </returns>
        public TData Find(string key);

        /// <summary> Removes a data item. </summary>
        ///
        /// <param name="key"> The key. </param>
        public void Remove(string key);

        /// <summary> Updates a data item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void Update(TData item);

        /// <summary> Save data changes </summary>
        public int SaveChanges();
    }
}
