//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models
{
    using System.Collections.Generic;

    /// <summary>   Interface for material repository. </summary>
    public interface IMaterialRepository
    {
        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        void Add(Material.Material item);

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <param name="type"> The type. </param>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        IEnumerable<Material.Material> GetAll(Material.Material.Type type);

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   A Material.Material. </returns>
        Material.Material Find(Material.Material.Type type, string key);

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        void Remove(Material.Material.Type type, string key);

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        void Update(Material.Material item);
    }
}
