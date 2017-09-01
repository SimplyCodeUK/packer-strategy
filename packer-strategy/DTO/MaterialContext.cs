// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using PackIt.Helpers.Enums;
    using PackIt.Models.Material;

    /// <summary>   A material context. </summary>
    public class MaterialContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialContext" /> class.
        /// </summary>
        ///
        /// <param name="options">  Options for controlling the operation. </param>
        public MaterialContext(DbContextOptions<MaterialContext> options)
            : base(options)
        {
        }

        /// <summary>   Gets or sets the materials. </summary>
        ///
        /// <value> The materials. </value>
        private DbSet<DtoMaterial.DtoMaterial> Materials { get; set; }

        /// <summary>   Gets the materials. </summary>
        ///
        /// <returns>   The materials. </returns>
        public List<Material> GetMaterials()
        {
            List<Material> ret = new List<Material>();

            foreach (DtoMaterial.DtoMaterial material in this.Materials)
            {
                ret.Add(MaterialMapper.Convert(material));
            }

            return ret;
        }

        /// <summary>   Adds a material. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void AddMaterial(Material item)
        {
            DtoMaterial.DtoMaterial material = MaterialMapper.Convert(item);
            this.Materials.Add(material);
        }

        /// <summary>   Searches for the first material. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   The found material. </returns>
        public Material FindMaterial(MaterialType type, string key)
        {
            DtoMaterial.DtoMaterial material = this.Materials.Find(type, key);
            Material ret = material == null ? null : MaterialMapper.Convert(material);

            return ret;
        }

        /// <summary>   Removes the material. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="key">  The key. </param>
        public void RemoveMaterial(MaterialType type, string key)
        {
            DtoMaterial.DtoMaterial entity = this.Materials.Find(type, key);
            this.Materials.Remove(entity);
        }

        /// <summary>   Updates the material described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void UpdateMaterial(Material item)
        {
            DtoMaterial.DtoMaterial entity = this.Materials.Find(item.IdType, item.Id);
            DtoMaterial.DtoMaterial material = MaterialMapper.Convert(item);
            this.Materials.Remove(entity);
            this.SaveChanges();
            this.Materials.Add(material);
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention
        /// from the entity types exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" />
        /// properties on your derived context. The resulting model may be cached and re-used for
        /// subsequent instances of your derived context.
        /// </summary>
        ///
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        ///
        /// <param name="modelBuilder">
        /// The builder being used to construct the model for this context.
        /// Databases (and other extensions) typically define extension
        /// methods on this object that allow you to configure aspects of the
        /// model that are specific to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtoMaterial.DtoMaterial>()
                .HasKey(c => new { c.IdType, c.Id });
            modelBuilder.Entity<DtoMaterial.DtoCosting>()
                .HasKey(c => new { c.MaterialType, c.MaterialId, c.Quantity });
            modelBuilder.Entity<DtoMaterial.DtoLayer>()
                .HasKey(c => new { c.MaterialType, c.MaterialId, c.Index });
            modelBuilder.Entity<DtoMaterial.DtoCollation>()
                .HasKey(c => new { c.MaterialType, c.MaterialId, c.LayerIndex, c.Index });
        }
    }
}
