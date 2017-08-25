//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Package;

    /// <summary>   A package repository. </summary>
    ///
    /// <seealso cref="T:packer_strategy.DTO.IPackageRepository"/>
    public class PackageRepository : IPackageRepository
    {
        /// <summary>   The context. </summary>
        private readonly PackageContext _context;

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="context">  The context. </param>
        public PackageRepository(PackageContext context)
        {
            _context = context;
        }

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPackageRepository.GetAll()"/>
        public IEnumerable<Package> GetAll()
        {
            return _context.GetPackages();
        }

        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPackageRepository.Add(Package)"/>
        public void Add(Package item)
        {
            _context.AddPackage(item);
            _context.SaveChanges();
        }

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   A Package. </returns>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPackageRepository.Find(string)"/>
        public Package Find(string key)
        {
            return _context.FindPackage(key);
        }

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPackageRepository.Remove(string)"/>
        public void Remove(string key)
        {
            _context.RemovePackage(key);
            _context.SaveChanges();
        }

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPackageRepository.Update(Package)"/>
        public void Update(Package item)
        {
            _context.UpdatePackage(item);
            _context.SaveChanges();
        }
    }
}
