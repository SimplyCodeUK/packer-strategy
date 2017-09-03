﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using PackIt.Helpers.Enums;
    using PackIt.Models.Material;

    /// <summary>   A material repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.IMaterialRepository"/>
    public class MaterialRepository : IMaterialRepository
    {
        /// <summary>   The context. </summary>
        private readonly MaterialContext context;

        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialRepository" /> class.
        /// </summary>
        ///
        /// <param name="context">  The context. </param>
        public MaterialRepository(MaterialContext context)
        {
            this.context = context;
        }

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <param name="type"> The type. </param>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        ///
        /// <seealso cref="M:PackIt.DTO.IMaterialRepository.GetAll(MaterialType)"/>
        public IEnumerable<Material> GetAll(MaterialType type)
        {
            return this.context.GetMaterials();
        }

        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IMaterialRepository.Add(Material)"/>
        public void Add(Material item)
        {
            this.context.AddMaterial(item);
            this.context.SaveChanges();
        }

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   A Material. </returns>
        ///
        /// <seealso cref="M:PackIt.DTO.IMaterialRepository.Find(MaterialType,string)"/>
        public Material Find(MaterialType type, string key)
        {
            return this.context.FindMaterial(type, key);
        }

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IMaterialRepository.Remove(MaterialType,string)"/>
        public void Remove(MaterialType type, string key)
        {
            this.context.RemoveMaterial(type, key);
            this.context.SaveChanges();
        }

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item to add. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IMaterialRepository.Update(Material)"/>
        public void Update(Material item)
        {
            this.context.UpdateMaterial(item);
            this.context.SaveChanges();
        }
    }
}