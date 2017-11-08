// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using PackIt.Material;

    /// <summary> Interface for material repository. </summary>
    public interface IMaterialRepository
    {
        /// <summary> Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        void Add(Material item);

        /// <summary> Gets all items in this collection. </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        IList<Material> GetAll();

        /// <summary> Searches for the first match for the given string. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> A Material.Material. </returns>
        Material Find(string key);

        /// <summary> Removes the given key. </summary>
        ///
        /// <param name="key"> The key. </param>
        void Remove(string key);

        /// <summary> Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        void Update(Material item);
    }
}
