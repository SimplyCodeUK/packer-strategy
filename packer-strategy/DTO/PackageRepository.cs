// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using PackIt.Models.Package;

    /// <summary>   A package repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.IPackageRepository"/>
    public class PackageRepository : IPackageRepository
    {
        /// <summary>   The context. </summary>
        private readonly PackageContext context;

        /// <summary>
        /// Initialises a new instance of the <see cref="PackageRepository" /> class.
        /// </summary>
        ///
        /// <param name="context">  The context. </param>
        public PackageRepository(PackageContext context)
        {
            this.context = context;
        }

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackageRepository.GetAll()"/>
        public IEnumerable<Package> GetAll()
        {
            return this.context.GetPackages();
        }

        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackageRepository.Add(Package)"/>
        public void Add(Package item)
        {
            this.context.AddPackage(item);
            this.context.SaveChanges();
        }

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   A Package. </returns>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackageRepository.Find(string)"/>
        public Package Find(string key)
        {
            return this.context.FindPackage(key);
        }

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackageRepository.Remove(string)"/>
        public void Remove(string key)
        {
            this.context.RemovePackage(key);
            this.context.SaveChanges();
        }

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IPackageRepository.Update(Package)"/>
        public void Update(Package item)
        {
            this.context.UpdatePackage(item);
            this.context.SaveChanges();
        }
    }
}
