// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using PackIt.Models.Plan;

    /// <summary>   Interface for plan repository. </summary>
    public interface IPlanRepository
    {
        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item. </param>
        void Add(Plan item);

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        IEnumerable<Plan> GetAll();

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        ///
        /// <returns>   A Plan.Plan. </returns>
        Plan Find(string key);

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        void Remove(string key);

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item. </param>
        void Update(Plan item);
    }
}
