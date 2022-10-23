// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DTO
{
    using System.IO;
    using NUnit.Framework;
    using PackIt.DTO;
    using PackIt.Drawing;
    using System.Text.Json;

    /// <summary> (Unit Test Method) Convert a Pack to it's DTO. </summary>
    [TestFixture]
    public class TestDrawingMapper
    {
        /// <summary> The mapper under test. </summary>
        private DrawingMapper mapper;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.mapper = new();
            Assert.IsNotNull(this.mapper);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertDrawingToDto()
        {
            var text = File.ReadAllText("DTO/TestData/drawing.json");
            var drawing = JsonSerializer.Deserialize<Drawing>(text);
            var dto = this.mapper.ConvertToDto(drawing);

            Assert.AreEqual(drawing.DrawingId, dto.DrawingId);

            Assert.AreEqual(drawing.Packs[0].Costings.Count, dto.Packs[0].Costings.Count);
            foreach (var costing in dto.Packs[0].Costings)
            {
                Assert.AreEqual(drawing.DrawingId, costing.PackId);
            }

            int minLevel = -1;
            Assert.AreEqual(drawing.Packs[0].Stages.Count, dto.Packs[0].Stages.Count);
            int stageIndex = 0;
            foreach (var stage in dto.Packs[0].Stages)
            {
                Assert.AreEqual(drawing.DrawingId, stage.PackId);
                Assert.LessOrEqual(minLevel, (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Limits.Count, stage.Limits.Count);
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.AreEqual(drawing.DrawingId, limit.PackId);
                    Assert.AreEqual(stage.StageLevel, limit.StageLevel);
                    Assert.AreEqual(limitIndex, limit.LimitIndex);
                    ++limitIndex;
                }

                Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results.Count, stage.Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.AreEqual(drawing.DrawingId, result.PackId);
                    Assert.AreEqual(stage.StageLevel, result.StageLevel);
                    Assert.AreEqual(resultIndex, result.ResultIndex);

                    Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers.Count, result.Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.AreEqual(drawing.DrawingId, layer.PackId);
                        Assert.AreEqual(stage.StageLevel, layer.StageLevel);
                        Assert.AreEqual(result.ResultIndex, layer.ResultIndex);
                        Assert.AreEqual(layerIndex, layer.LayerIndex);

                        Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count, layer.Collations.Count);
                        int collationIndex = 0;
                        foreach (var collation in layer.Collations)
                        {
                            Assert.AreEqual(drawing.DrawingId, collation.PackId);
                            Assert.AreEqual(stage.StageLevel, collation.StageLevel);
                            Assert.AreEqual(result.ResultIndex, collation.ResultIndex);
                            Assert.AreEqual(layer.LayerIndex, collation.LayerIndex);
                            Assert.AreEqual(collationIndex, collation.CollationIndex);
                            ++collationIndex;
                        }
                        ++layerIndex;
                    }

                    Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials.Count, result.Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.AreEqual(drawing.DrawingId, material.PackId);
                        Assert.AreEqual(stage.StageLevel, material.StageLevel);
                        Assert.AreEqual(result.ResultIndex, material.ResultIndex);
                        Assert.AreEqual(materialIndex, material.MaterialIndex);

                        Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count, material.DatabaseMaterials.Count);
                        int databaseIndex = 0;
                        foreach (var databaseMaterial in material.DatabaseMaterials)
                        {
                            Assert.AreEqual(drawing.DrawingId, databaseMaterial.PackId);
                            Assert.AreEqual(stage.StageLevel, databaseMaterial.StageLevel);
                            Assert.AreEqual(result.ResultIndex, databaseMaterial.ResultIndex);
                            Assert.AreEqual(material.MaterialIndex, databaseMaterial.MaterialIndex);
                            Assert.AreEqual(databaseIndex, databaseMaterial.DatabaseMaterialIndex);
                            ++databaseIndex;
                        }
                        ++materialIndex;
                    }

                    Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Sections.Count, result.Sections.Count);
                    int sectionIndex = 0;
                    foreach (var section in result.Sections)
                    {
                        Assert.AreEqual(drawing.DrawingId, section.PackId);
                        Assert.AreEqual(stage.StageLevel, section.StageLevel);
                        Assert.AreEqual(result.ResultIndex, section.ResultIndex);
                        Assert.AreEqual(sectionIndex, section.SectionIndex);
                        ++sectionIndex;
                    }
                    ++resultIndex;
                }
                ++stageIndex;
            }
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertDtoDrawing()
        {
            var text = File.ReadAllText("DTO/TestData/drawing.json");
            var drawing = JsonSerializer.Deserialize<Drawing>(text);
            var dto = this.mapper.ConvertToDto(drawing);
            var data = this.mapper.ConvertToData(dto);

            Assert.AreEqual(drawing.DrawingId, data.DrawingId);

            Assert.AreEqual(drawing.Packs[0].Costings.Count, data.Packs[0].Costings.Count);

            int minLevel = -1;
            Assert.AreEqual(drawing.Packs[0].Stages.Count, data.Packs[0].Stages.Count);
            int stageIndex = 0;
            foreach (var stage in data.Packs[0].Stages)
            {
                Assert.LessOrEqual(minLevel, (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Limits.Count, stage.Limits.Count);

                Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results.Count, stage.Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers.Count, result.Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count, layer.Collations.Count);
                        ++layerIndex;
                    }

                    Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials.Count, result.Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count, material.DatabaseMaterials.Count);
                        ++materialIndex;
                    }

                    Assert.AreEqual(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Sections.Count, result.Sections.Count);
                    ++resultIndex;
                }
                ++stageIndex;
            }
        }
    }
}
