//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;
using System.Linq;

namespace packer_strategy.Models
{
    /// <summary>   A material repository. </summary>
    public class MaterialRepository : IMaterialRepository
    {
        /// <summary>   The context. </summary>
        private readonly MaterialContext _context;

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="context">  The context. </param>
        public MaterialRepository(MaterialContext context)
        {
            _context = context;
        }

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <param name="type"> The type. </param>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        public IEnumerable<Material.Material> GetAll(Material.Material.Type type)
        {
            return _context.Materials.ToList();
        }

        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        public void Add(Material.Material item)
        {
            _context.Materials.Add(item);
            _context.SaveChanges();
        }

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   A Material.Material. </returns>
        public Material.Material Find(Material.Material.Type type, string key)
        {
            return _context.Materials.FirstOrDefault(t => t.IdType==type && t.Id == key);
        }

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        public void Remove(Material.Material.Type type, string key)
        {
            var entity = _context.Materials.First(t => t.IdType == type && t.Id == key);
            _context.Materials.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        public void Update(Material.Material item)
        {
            _context.Materials.Update(item);
            _context.SaveChanges();
        }
    }
}
