// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using PackIt.Pack;

    /// <summary>   A pack repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.IPackRepository"/>
    public class PackRepository : IPackRepository
    {
        /// <summary>   The context. </summary>
        private readonly PackContext context;

        /// <summary>
        /// Initialises a new instance of the <see cref="PackRepository" /> class.
        /// </summary>
        ///
        /// <param name="context">  The context. </param>
        public PackRepository(PackContext context)
        {
            this.context = context;
        }

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackRepository.GetAll()"/>
        public IEnumerable<Pack> GetAll()
        {
            return this.context.GetPacks();
        }

        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackRepository.Add(Pack)"/>
        public void Add(Pack item)
        {
            this.context.AddPack(item);
            this.context.SaveChanges();
        }

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   A Pack. </returns>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackRepository.Find(string)"/>
        public Pack Find(string key)
        {
            return this.context.FindPack(key);
        }

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackRepository.Remove(string)"/>
        public void Remove(string key)
        {
            this.context.RemovePack(key);
            this.context.SaveChanges();
        }

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackRepository.Update(Pack)"/>
        public void Update(Pack item)
        {
            this.context.UpdatePack(item);
            this.context.SaveChanges();
        }
    }
}
