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
        public DbSet<DtoMaterial.DtoMaterial> Materials { get; set; }

        /// <summary>   Gets the materials. </summary>
        ///
        /// <returns>   The materials. </returns>
        public List<Material> GetMaterials()
        {
            List<Material> ret = new List<Material>();

            foreach (DtoMaterial.DtoMaterial item in this.Materials)
            {
                ret.Add(MaterialMapper.Convert(item));
            }

            return ret;
        }

        /// <summary>   Adds a material. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void AddMaterial(Material item)
        {
            DtoMaterial.DtoMaterial dto = MaterialMapper.Convert(item);
            this.Materials.Add(dto);
        }

        /// <summary>   Searches for the first material. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   The found material. </returns>
        public Material FindMaterial(string key)
        {
            DtoMaterial.DtoMaterial dto = this.Materials.Find(key);
            Material ret = dto == null ? null : MaterialMapper.Convert(dto);

            return ret;
        }

        /// <summary>   Removes the material. </summary>
        ///
        /// <param name="key">  The key. </param>
        public void RemoveMaterial(string key)
        {
            DtoMaterial.DtoMaterial entity = this.Materials.Find(key);
            this.Materials.Remove(entity);
        }

        /// <summary>   Updates the material described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void UpdateMaterial(Material item)
        {
            DtoMaterial.DtoMaterial entity = this.Materials.Find(item.Id);
            DtoMaterial.DtoMaterial dto = MaterialMapper.Convert(item);
            this.Materials.Remove(entity);
            this.SaveChanges();
            this.Materials.Add(dto);
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
                .HasKey(c => new { c.Id });
            modelBuilder.Entity<DtoMaterial.DtoCosting>()
                .HasKey(c => new { c.MaterialId, c.Quantity });
            modelBuilder.Entity<DtoMaterial.DtoLayer>()
                .HasKey(c => new { c.MaterialId, c.Index });
            modelBuilder.Entity<DtoMaterial.DtoCollation>()
                .HasKey(c => new { c.MaterialId, c.LayerIndex, c.Index });
        }
    }
}
