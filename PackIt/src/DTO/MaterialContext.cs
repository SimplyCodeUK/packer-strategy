// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using PackIt.Material;

    /// <summary> A material context. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.PackItContext{TData, TDtoData, TMapper}"/>
    /// <remarks>
    /// Initialises a new instance of the <see cref="MaterialContext" /> class.
    /// </remarks>
    ///
    /// <param name="options"> Options for controlling the operation. </param>
    public class MaterialContext(DbContextOptions<MaterialContext> options) : PackItContext<Material, DtoMaterial.DtoMaterial, MaterialMapper>(options)
    {
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
            base.OnModelCreating(modelBuilder);

            ConfigureDtoMaterial(modelBuilder);
            Configure<DtoMaterial.DtoCosting>(modelBuilder, "DtoCosting", k => new { k.MaterialId, k.Quantity });
            ConfigureDtoLayer(modelBuilder);
            Configure<DtoMaterial.DtoSection>(modelBuilder, "DtoSection", k => new { k.MaterialId, k.SectionIndex });
            Configure<DtoMaterial.DtoCollation>(modelBuilder, "DtoCollation", k => new { k.MaterialId, k.LayerIndex, k.CollationIndex });
            ConfigureDtoPalletDeck(modelBuilder);
            Configure<DtoMaterial.DtoPlank>(modelBuilder, "DtoPlank", k => new { k.MaterialId, k.PalletDeckIndex, k.PlankIndex });
        }

        /// <summary>Construct default query.</summary>
        ///
        /// <returns> Query for list of materials. </returns>
        protected override IQueryable<DtoMaterial.DtoMaterial> ConstructQuery()
        {
            var query = this.Resources
                .Include(m => m.Costings)
                .Include(m => m.Layers)
                .Include(m => m.Layers).ThenInclude(l => l.Collations)
                .Include(m => m.PalletDecks)
                .Include(m => m.PalletDecks).ThenInclude(p => p.Planks)
                .Include(m => m.Sections);

            return query;
        }

        /// <summary>Construct a find task.</summary>
        ///
        /// <param name="key"> The key to search for. </param>
        ///
        /// <returns> The find task. </returns>
        protected override System.Threading.Tasks.Task<DtoMaterial.DtoMaterial> ConstructFindTask(string key)
        {
            return this.ConstructQuery().SingleAsync(p => p.MaterialId == key);
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoMaterial(ModelBuilder modelBuilder)
        {
            var builder = Configure<DtoMaterial.DtoMaterial>(modelBuilder, "DtoMaterial", k => new { k.MaterialId });
            builder
                .HasMany(m => m.Costings)
                .WithOne()
                .HasForeignKey(c => new { c.MaterialId });
            builder
                .HasMany(m => m.Layers)
                .WithOne()
                .HasForeignKey(l => new { l.MaterialId });
            builder
                .HasMany(m => m.PalletDecks)
                .WithOne()
                .HasForeignKey(p => new { p.MaterialId });
            builder
                .HasMany(m => m.Sections)
                .WithOne()
                .HasForeignKey(s => new { s.MaterialId });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoLayer(ModelBuilder modelBuilder)
        {
            Configure<DtoMaterial.DtoLayer>(modelBuilder, "DtoLayer", k => new { k.MaterialId, k.LayerIndex })
                .HasMany(m => m.Collations)
                .WithOne()
                .HasForeignKey(c => new { c.MaterialId, c.LayerIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoPalletDeck(ModelBuilder modelBuilder)
        {
            Configure<DtoMaterial.DtoPalletDeck>(modelBuilder, "DtoPalletDeck", k => new { k.MaterialId, k.PalletDeckIndex })
                .HasMany(p => p.Planks)
                .WithOne()
                .HasForeignKey(p => new { p.MaterialId, p.PalletDeckIndex });
        }
    }
}
