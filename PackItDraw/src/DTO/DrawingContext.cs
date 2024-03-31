// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using PackIt.Drawing;

    /// <summary> A drawing context. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.PackItContext{TData, TDtoData, TMapper}"/>
    /// <remarks>
    /// Initialises a new instance of the <see cref="DrawingContext" /> class.
    /// </remarks>
    ///
    /// <param name="options"> Options for controlling the operation. </param>
    public class DrawingContext(DbContextOptions<DrawingContext> options) : PackItContext<Drawing, DtoDrawing.DtoDrawing, DrawingMapper>(options)
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

            ConfigureDtoDrawing(modelBuilder);
            ConfigureDtoPack(modelBuilder);
            Configure<DtoPack.DtoCosting>(modelBuilder, "DtoCosting", k => new { k.PackId, k.RequiredQuantity });
            ConfigureDtoStage(modelBuilder);
            Configure<DtoPack.DtoLimit>(modelBuilder, "DtoLimit", k => new { k.PackId, k.StageLevel, k.LimitIndex });
            ConfigureDtoResult(modelBuilder);
            ConfigureDtoLayer(modelBuilder);
            Configure<DtoPack.DtoCollation>(modelBuilder, "DtoCollation", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.LayerIndex, k.CollationIndex });
            ConfigureDtoMaterial(modelBuilder);
            Configure<DtoPack.DtoDatabaseMaterial>(modelBuilder, "DtoDatabaseMaterial", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.MaterialIndex, k.DatabaseMaterialIndex });
            Configure<DtoPack.DtoSection>(modelBuilder, "DtoSection", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.SectionIndex });
            Configure<DtoDrawing.DtoShape3D>(modelBuilder, "DtoShape", k => new { k.DrawingId, k.ShapeIndex });
        }

        /// <summary>Construct default query.</summary>
        ///
        /// <returns> Query for list of packs. </returns>
        protected override IQueryable<DtoDrawing.DtoDrawing> ConstructQuery()
        {
            var query = this.Resources
                .Include(p => p.Packs)
                .Include(p => p.Shapes);

            return query;
        }

        /// <summary>Construct a find task.</summary>
        ///
        /// <param name="key"> The key to search for. </param>
        ///
        /// <returns> The find task. </returns>
        protected override System.Threading.Tasks.Task<DtoDrawing.DtoDrawing> ConstructFindTask(string key)
        {
            return this.ConstructQuery().SingleAsync(p => p.DrawingId == key);
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoDrawing(ModelBuilder modelBuilder)
        {
            var builder = Configure<DtoDrawing.DtoDrawing>(modelBuilder, "DtoDrawing", k => k.DrawingId);
            builder
                .HasMany(d => d.Packs)
                .WithOne()
                .HasForeignKey(k => new { k.PackId });
            builder
                .HasMany(d => d.Shapes)
                .WithOne()
                .HasForeignKey(k => new { k.DrawingId });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoPack(ModelBuilder modelBuilder)
        {
            var builder = Configure<DtoPack.DtoPack>(modelBuilder, "DtoPack", k => k.PackId);
            builder
                .HasMany(p => p.Costings)
                .WithOne()
                .HasForeignKey(c => new { c.PackId });
            builder
                .HasMany(p => p.Stages)
                .WithOne()
                .HasForeignKey(s => new { s.PackId });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoStage(ModelBuilder modelBuilder)
        {
            var builder = Configure<DtoPack.DtoStage>(modelBuilder, "DtoStage", k => new { k.PackId, k.StageLevel });
            builder
                .HasMany(s => s.Limits)
                .WithOne()
                .HasForeignKey(l => new { l.PackId, l.StageLevel });
            builder
                .HasMany(s => s.Results)
                .WithOne()
                .HasForeignKey(r => new { r.PackId, r.StageLevel });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoResult(ModelBuilder modelBuilder)
        {
            var builder = Configure<DtoPack.DtoResult>(modelBuilder, "DtoResult", k => new { k.PackId, k.StageLevel, k.ResultIndex });
            builder
                .HasMany(r => r.Layers)
                .WithOne()
                .HasForeignKey(l => new { l.PackId, l.StageLevel, l.ResultIndex });
            builder
                .HasMany(r => r.Materials)
                .WithOne()
                .HasForeignKey(m => new { m.PackId, m.StageLevel, m.ResultIndex });
            builder
                .HasMany(r => r.Sections)
                .WithOne()
                .HasForeignKey(s => new { s.PackId, s.StageLevel, s.ResultIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoLayer(ModelBuilder modelBuilder)
        {
            Configure<DtoPack.DtoLayer>(modelBuilder, "DtoLayer", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.LayerIndex })
                .HasMany(l => l.Collations)
                .WithOne()
                .HasForeignKey(c => new { c.PackId, c.StageLevel, c.ResultIndex, c.LayerIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoMaterial(ModelBuilder modelBuilder)
        {
            Configure<DtoPack.DtoMaterial>(modelBuilder, "DtoMaterial", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.MaterialIndex })
                .HasMany(m => m.DatabaseMaterials)
                .WithOne()
                .HasForeignKey(d => new { d.PackId, d.StageLevel, d.ResultIndex, d.MaterialIndex });
        }
    }
}
