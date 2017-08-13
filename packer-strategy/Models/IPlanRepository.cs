//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models
{
    using System.Collections.Generic;

    /// <summary>   Interface for plan repository. </summary>
    public interface IPlanRepository
    {
        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item. </param>
        void Add(Plan.Plan item);

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        IEnumerable<Plan.Plan> GetAll();

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        ///
        /// <returns>   A Plan.Plan. </returns>
        Plan.Plan Find(string key);

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        void Remove(string key);

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item. </param>
        void Update(Plan.Plan item);
    }
}
