// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DTO
{
    using System.IO;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using PackIt.DTO;
    using PackIt.Drawing;

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
            var drawing = JsonConvert.DeserializeObject<Drawing>(text);
            var dto = this.mapper.ConvertToDto(drawing);

            Assert.AreEqual(dto.DrawingId, drawing.DrawingId);

            Assert.AreEqual(dto.Packs[0].Costings.Count, drawing.Packs[0].Costings.Count);
            foreach (var costing in dto.Packs[0].Costings)
            {
                Assert.AreEqual(costing.PackId, drawing.DrawingId);
            }

            int minLevel = -1;
            Assert.AreEqual(dto.Packs[0].Stages.Count, drawing.Packs[0].Stages.Count);
            int stageIndex = 0;
            foreach (var stage in dto.Packs[0].Stages)
            {
                Assert.AreEqual(stage.PackId, drawing.DrawingId);
                Assert.Greater((int)stage.StageLevel, minLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(stage.Limits.Count, drawing.Packs[0].Stages[stageIndex].Limits.Count);
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.AreEqual(limit.PackId, drawing.DrawingId);
                    Assert.AreEqual(limit.StageLevel, stage.StageLevel);
                    Assert.AreEqual(limit.LimitIndex, limitIndex);
                    ++limitIndex;
                }

                Assert.AreEqual(stage.Results.Count, drawing.Packs[0].Stages[stageIndex].Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.AreEqual(result.PackId, drawing.DrawingId);
                    Assert.AreEqual(result.StageLevel, stage.StageLevel);
                    Assert.AreEqual(result.ResultIndex, resultIndex);

                    Assert.AreEqual(result.Layers.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.AreEqual(layer.PackId, drawing.DrawingId);
                        Assert.AreEqual(layer.StageLevel, stage.StageLevel);
                        Assert.AreEqual(layer.ResultIndex, result.ResultIndex);
                        Assert.AreEqual(layer.LayerIndex, layerIndex);

                        Assert.AreEqual(layer.Collations.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count);
                        int collationIndex = 0;
                        foreach (var collation in layer.Collations)
                        {
                            Assert.AreEqual(collation.PackId, drawing.DrawingId);
                            Assert.AreEqual(collation.StageLevel, stage.StageLevel);
                            Assert.AreEqual(collation.ResultIndex, result.ResultIndex);
                            Assert.AreEqual(collation.LayerIndex, layer.LayerIndex);
                            Assert.AreEqual(collation.CollationIndex, collationIndex);
                            ++collationIndex;
                        }
                        ++layerIndex;
                    }

                    Assert.AreEqual(result.Materials.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.AreEqual(material.PackId, drawing.DrawingId);
                        Assert.AreEqual(material.StageLevel, stage.StageLevel);
                        Assert.AreEqual(material.ResultIndex, result.ResultIndex);
                        Assert.AreEqual(material.MaterialIndex, materialIndex);

                        Assert.AreEqual(material.DatabaseMaterials.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count);
                        int databaseIndex = 0;
                        foreach (var databaseMaterial in material.DatabaseMaterials)
                        {
                            Assert.AreEqual(databaseMaterial.PackId, drawing.DrawingId);
                            Assert.AreEqual(databaseMaterial.StageLevel, stage.StageLevel);
                            Assert.AreEqual(databaseMaterial.ResultIndex, result.ResultIndex);
                            Assert.AreEqual(databaseMaterial.MaterialIndex, material.MaterialIndex);
                            Assert.AreEqual(databaseMaterial.DatabaseMaterialIndex, databaseIndex);
                            ++databaseIndex;
                        }
                        ++materialIndex;
                    }

                    Assert.AreEqual(result.Sections.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Sections.Count);
                    int sectionIndex = 0;
                    foreach (var section in result.Sections)
                    {
                        Assert.AreEqual(section.PackId, drawing.DrawingId);
                        Assert.AreEqual(section.StageLevel, stage.StageLevel);
                        Assert.AreEqual(section.ResultIndex, result.ResultIndex);
                        Assert.AreEqual(section.SectionIndex, sectionIndex);
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
            var drawing = JsonConvert.DeserializeObject<Drawing>(text);
            var dto = this.mapper.ConvertToDto(drawing);
            var data = this.mapper.ConvertToData(dto);

            Assert.AreEqual(data.DrawingId, drawing.DrawingId);

            Assert.AreEqual(data.Packs[0].Costings.Count, drawing.Packs[0].Costings.Count);

            int minLevel = -1;
            Assert.AreEqual(data.Packs[0].Stages.Count, drawing.Packs[0].Stages.Count);
            int stageIndex = 0;
            foreach (var stage in data.Packs[0].Stages)
            {
                Assert.Greater((int)stage.StageLevel, minLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(stage.Limits.Count, drawing.Packs[0].Stages[stageIndex].Limits.Count);

                Assert.AreEqual(stage.Results.Count, drawing.Packs[0].Stages[stageIndex].Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.AreEqual(result.Layers.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.AreEqual(layer.Collations.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count);
                        ++layerIndex;
                    }

                    Assert.AreEqual(result.Materials.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.AreEqual(material.DatabaseMaterials.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count);
                        ++materialIndex;
                    }

                    Assert.AreEqual(result.Sections.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Sections.Count);
                    ++resultIndex;
                }
                ++stageIndex;
            }
        }
    }
}
