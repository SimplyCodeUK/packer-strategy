//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO
{
    using System.Collections.Generic;
    using System.Linq;
    using Helpers.Enums;
    using Models.Material;

    /// <summary>   A material repository. </summary>
    ///
    /// <seealso cref="T:packer_strategy.DTO.IMaterialRepository"/>
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
        ///
        /// <seealso cref="M:packer_strategy.DTO.IMaterialRepository.GetAll(MaterialType)"/>
        public IEnumerable<Material> GetAll(MaterialType type)
        {
            return _context.GetMaterials();
        }

        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IMaterialRepository.Add(Material)"/>
        public void Add(Material item)
        {
            _context.AddMaterial(item);
            _context.SaveChanges();
        }

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   A Material. </returns>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IMaterialRepository.Find(MaterialType,string)"/>
        public Material Find(MaterialType type, string key)
        {
            return _context.FindMaterial(type, key);
        }

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IMaterialRepository.Remove(MaterialType,string)"/>
        public void Remove(MaterialType type, string key)
        {
            _context.RemoveMaterial(type, key);
            _context.SaveChanges();
        }

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IMaterialRepository.Update(Material)"/>
        public void Update(Material item)
        {
            _context.UpdateMaterial(item);
            _context.SaveChanges();
        }
    }
}
