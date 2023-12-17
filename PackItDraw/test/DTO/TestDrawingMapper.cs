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
            Assert.That(this.mapper, Is.Not.Null);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertDrawingToDto()
        {
            var text = File.ReadAllText("DTO/TestData/drawing.json");
            var drawing = JsonSerializer.Deserialize<Drawing>(text);
            var dto = this.mapper.ConvertToDto(drawing);

            Assert.That(dto.DrawingId, Is.EqualTo(drawing.DrawingId));

            Assert.That(dto.Packs[0].Costings.Count, Is.EqualTo(drawing.Packs[0].Costings.Count));
            foreach (var costing in dto.Packs[0].Costings)
            {
                Assert.That(costing.PackId, Is.EqualTo(drawing.DrawingId));
            }

            int minLevel = -1;
            Assert.That(dto.Packs[0].Stages.Count, Is.EqualTo(drawing.Packs[0].Stages.Count));
            int stageIndex = 0;
            foreach (var stage in dto.Packs[0].Stages)
            {
                Assert.That(stage.PackId, Is.EqualTo(drawing.DrawingId));
                Assert.That(minLevel, Is.LessThanOrEqualTo((int)stage.StageLevel));
                minLevel = (int)stage.StageLevel;

                Assert.That(stage.Limits.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Limits.Count));
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.That(limit.PackId, Is.EqualTo(drawing.DrawingId));
                    Assert.That(limit.StageLevel, Is.EqualTo(stage.StageLevel));
                    Assert.That(limit.LimitIndex, Is.EqualTo(limitIndex));
                    ++limitIndex;
                }

                Assert.That(stage.Results.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results.Count));
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.That(result.PackId, Is.EqualTo(drawing.DrawingId));
                    Assert.That(result.StageLevel, Is.EqualTo(stage.StageLevel));
                    Assert.That(result.ResultIndex, Is.EqualTo(resultIndex));

                    Assert.That(result.Layers.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers.Count));
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.That(layer.PackId, Is.EqualTo(drawing.DrawingId));
                        Assert.That(layer.StageLevel, Is.EqualTo(stage.StageLevel));
                        Assert.That(layer.ResultIndex, Is.EqualTo(result.ResultIndex));
                        Assert.That(layer.LayerIndex, Is.EqualTo(layerIndex));

                        Assert.That(layer.Collations.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count));
                        int collationIndex = 0;
                        foreach (var collation in layer.Collations)
                        {
                            Assert.That(collation.PackId, Is.EqualTo(drawing.DrawingId));
                            Assert.That(collation.StageLevel, Is.EqualTo(stage.StageLevel));
                            Assert.That(collation.ResultIndex, Is.EqualTo(result.ResultIndex));
                            Assert.That(collation.LayerIndex, Is.EqualTo(layer.LayerIndex));
                            Assert.That(collation.CollationIndex, Is.EqualTo(collationIndex));
                            ++collationIndex;
                        }
                        ++layerIndex;
                    }

                    Assert.That(result.Materials.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials.Count));
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.That(material.PackId, Is.EqualTo(drawing.DrawingId));
                        Assert.That(material.StageLevel, Is.EqualTo(stage.StageLevel));
                        Assert.That(material.ResultIndex, Is.EqualTo(result.ResultIndex));
                        Assert.That(material.MaterialIndex, Is.EqualTo(materialIndex));

                        Assert.That(material.DatabaseMaterials.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count));
                        int databaseIndex = 0;
                        foreach (var databaseMaterial in material.DatabaseMaterials)
                        {
                            Assert.That(databaseMaterial.PackId, Is.EqualTo(drawing.DrawingId));
                            Assert.That(databaseMaterial.StageLevel, Is.EqualTo(stage.StageLevel));
                            Assert.That(databaseMaterial.ResultIndex, Is.EqualTo(result.ResultIndex));
                            Assert.That(databaseMaterial.MaterialIndex, Is.EqualTo(material.MaterialIndex));
                            Assert.That(databaseMaterial.DatabaseMaterialIndex, Is.EqualTo(databaseIndex));
                            ++databaseIndex;
                        }
                        ++materialIndex;
                    }

                    Assert.That(result.Sections.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Sections.Count));
                    int sectionIndex = 0;
                    foreach (var section in result.Sections)
                    {
                        Assert.That(section.PackId, Is.EqualTo(drawing.DrawingId));
                        Assert.That(section.StageLevel, Is.EqualTo(stage.StageLevel));
                        Assert.That(section.ResultIndex, Is.EqualTo(result.ResultIndex));
                        Assert.That(section.SectionIndex, Is.EqualTo(sectionIndex));
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

            Assert.That(data.DrawingId, Is.EqualTo(drawing.DrawingId));

            Assert.That(data.Packs[0].Costings.Count, Is.EqualTo(drawing.Packs[0].Costings.Count));

            int minLevel = -1;
            Assert.That(data.Packs[0].Stages.Count, Is.EqualTo(drawing.Packs[0].Stages.Count));
            int stageIndex = 0;
            foreach (var stage in data.Packs[0].Stages)
            {
                Assert.That(minLevel, Is.LessThanOrEqualTo((int)stage.StageLevel));
                minLevel = (int)stage.StageLevel;

                Assert.That(stage.Limits.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Limits.Count));

                Assert.That(stage.Results.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results.Count));
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.That(result.Layers.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers.Count));
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.That(layer.Collations.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count));
                        ++layerIndex;
                    }

                    Assert.That(result.Materials.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials.Count));
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.That(material.DatabaseMaterials.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count));
                        ++materialIndex;
                    }

                    Assert.That(result.Sections.Count, Is.EqualTo(drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Sections.Count));
                    ++resultIndex;
                }
                ++stageIndex;
            }
        }
    }
}
