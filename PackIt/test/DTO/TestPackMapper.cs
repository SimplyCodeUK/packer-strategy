// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DTO
{
    using System.IO;
    using System.Text.Json;
    using NUnit.Framework;
    using PackIt.DTO;
    using PackIt.Pack;

    /// <summary> (Unit Test Method) Convert a Pack to it's DTO. </summary>
    [TestFixture]
    public class TestPackMapper
    {
        /// <summary> The mapper under test. </summary>
        private PackMapper mapper;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.mapper = new();
            Assert.IsNotNull(this.mapper);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertPackToDto()
        {
            var text = File.ReadAllText("DTO/TestData/pack.json");
            var pack = JsonSerializer.Deserialize<Pack>(text);
            var dto = this.mapper.ConvertToDto(pack);

            Assert.AreEqual(pack.PackId, dto.PackId);

            Assert.AreEqual(pack.Costings.Count, dto.Costings.Count);
            foreach (var costing in dto.Costings)
            {
                Assert.AreEqual(pack.PackId, costing.PackId);
            }

            int minLevel = -1;
            Assert.AreEqual(pack.Stages.Count, dto.Stages.Count);
            int stageIndex = 0;
            foreach (var stage in dto.Stages)
            {
                Assert.AreEqual(pack.PackId, stage.PackId);
                Assert.LessOrEqual(minLevel, (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(pack.Stages[stageIndex].Limits.Count, stage.Limits.Count);
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.AreEqual(pack.PackId, limit.PackId);
                    Assert.AreEqual(stage.StageLevel, limit.StageLevel);
                    Assert.AreEqual(limitIndex, limit.LimitIndex);
                    ++limitIndex;
                }

                Assert.AreEqual(pack.Stages[stageIndex].Results.Count, stage.Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.AreEqual(pack.PackId, result.PackId);
                    Assert.AreEqual(stage.StageLevel, result.StageLevel);
                    Assert.AreEqual(resultIndex, result.ResultIndex);

                    Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Layers.Count, result.Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.AreEqual(pack.PackId, layer.PackId);
                        Assert.AreEqual(stage.StageLevel, layer.StageLevel);
                        Assert.AreEqual(result.ResultIndex, layer.ResultIndex);
                        Assert.AreEqual(layerIndex, layer.LayerIndex);

                        Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count, layer.Collations.Count);
                        int collationIndex = 0;
                        foreach (var collation in layer.Collations)
                        {
                            Assert.AreEqual(pack.PackId, collation.PackId);
                            Assert.AreEqual(stage.StageLevel, collation.StageLevel);
                            Assert.AreEqual(result.ResultIndex, collation.ResultIndex);
                            Assert.AreEqual(layer.LayerIndex, collation.LayerIndex);
                            Assert.AreEqual(collationIndex, collation.CollationIndex);
                            ++collationIndex;
                        }
                        ++layerIndex;
                    }

                    Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Materials.Count, result.Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.AreEqual(pack.PackId, material.PackId);
                        Assert.AreEqual(stage.StageLevel, material.StageLevel);
                        Assert.AreEqual(result.ResultIndex, material.ResultIndex);
                        Assert.AreEqual(materialIndex, material.MaterialIndex);

                        Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count, material.DatabaseMaterials.Count);
                        int databaseIndex = 0;
                        foreach (var databaseMaterial in material.DatabaseMaterials)
                        {
                            Assert.AreEqual(pack.PackId, databaseMaterial.PackId);
                            Assert.AreEqual(stage.StageLevel, databaseMaterial.StageLevel);
                            Assert.AreEqual(result.ResultIndex, databaseMaterial.ResultIndex);
                            Assert.AreEqual(material.MaterialIndex, databaseMaterial.MaterialIndex);
                            Assert.AreEqual(databaseIndex, databaseMaterial.DatabaseMaterialIndex);
                            ++databaseIndex;
                        }
                        ++materialIndex;
                    }

                    Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Sections.Count, result.Sections.Count);
                    int sectionIndex = 0;
                    foreach (var section in result.Sections)
                    {
                        Assert.AreEqual(pack.PackId, section.PackId);
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
        public void ConvertDtoToPack()
        {
            var text = File.ReadAllText("DTO/TestData/pack.json");
            var pack = JsonSerializer.Deserialize<Pack>(text);
            var dto = this.mapper.ConvertToDto(pack);
            var data = this.mapper.ConvertToData(dto);

            Assert.AreEqual(pack.PackId, data.PackId);

            Assert.AreEqual(pack.Costings.Count, data.Costings.Count);

            int minLevel = -1;
            Assert.AreEqual(pack.Stages.Count, data.Stages.Count);
            int stageIndex = 0;
            foreach (var stage in data.Stages)
            {
                Assert.LessOrEqual(minLevel, (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(pack.Stages[stageIndex].Limits.Count, stage.Limits.Count);

                Assert.AreEqual(pack.Stages[stageIndex].Results.Count, stage.Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Layers.Count, result.Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count, layer.Collations.Count);
                        ++layerIndex;
                    }

                    Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Materials.Count, result.Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count, material.DatabaseMaterials.Count);
                        ++materialIndex;
                    }

                    Assert.AreEqual(pack.Stages[stageIndex].Results[resultIndex].Sections.Count, result.Sections.Count);
                    ++resultIndex;
                }
                ++stageIndex;
            }
        }
    }
}
