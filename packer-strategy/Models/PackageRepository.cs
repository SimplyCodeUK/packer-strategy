//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>   A package repository. </summary>
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
        public IEnumerable<Package.Package> GetAll()
        {
            return _context.Packages.ToList();
        }

        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        public void Add(Package.Package item)
        {
            _context.Packages.Add(item);
            _context.SaveChanges();
        }

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   A Package.Package. </returns>
        public Package.Package Find(string key)
        {
            return _context.Packages.FirstOrDefault(t => t.Id == key);
        }

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="key">  The key. </param>
        public void Remove(string key)
        {
            var entity = _context.Packages.First(t => t.Id == key);
            _context.Packages.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        public void Update(Package.Package item)
        {
            _context.Packages.Update(item);
            _context.SaveChanges();
        }
    }
}
