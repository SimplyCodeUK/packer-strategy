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
            Assert.That(this.mapper, Is.Not.Null);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertPackToDto()
        {
            var text = File.ReadAllText("DTO/TestData/pack.json");
            var pack = JsonSerializer.Deserialize<Pack>(text);
            var dto = this.mapper.ConvertToDto(pack);

            Assert.That(dto.PackId, Is.EqualTo(pack.PackId));

            Assert.That(dto.Costings.Count, Is.EqualTo(pack.Costings.Count));
            foreach (var costing in dto.Costings)
            {
                Assert.That(costing.PackId, Is.EqualTo(pack.PackId));
            }

            int minLevel = -1;
            Assert.That(dto.Stages.Count, Is.EqualTo(pack.Stages.Count));
            int stageIndex = 0;
            foreach (var stage in dto.Stages)
            {
                Assert.That(stage.PackId, Is.EqualTo(pack.PackId));
                Assert.That(minLevel, Is.LessThanOrEqualTo((int)stage.StageLevel));
                minLevel = (int)stage.StageLevel;

                Assert.That(stage.Limits.Count, Is.EqualTo(pack.Stages[stageIndex].Limits.Count));
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.That(limit.PackId, Is.EqualTo(pack.PackId));
                    Assert.That(limit.StageLevel, Is.EqualTo(stage.StageLevel));
                    Assert.That(limit.LimitIndex, Is.EqualTo(limitIndex));
                    ++limitIndex;
                }

                Assert.That(stage.Results.Count, Is.EqualTo(pack.Stages[stageIndex].Results.Count));
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.That(result.PackId, Is.EqualTo(pack.PackId));
                    Assert.That(result.StageLevel, Is.EqualTo(stage.StageLevel));
                    Assert.That(result.ResultIndex, Is.EqualTo(resultIndex));

                    Assert.That(result.Layers.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Layers.Count));
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.That(layer.PackId, Is.EqualTo(pack.PackId));
                        Assert.That(layer.StageLevel, Is.EqualTo(stage.StageLevel));
                        Assert.That(layer.ResultIndex, Is.EqualTo(result.ResultIndex));
                        Assert.That(layer.LayerIndex, Is.EqualTo(layerIndex));

                        Assert.That(layer.Collations.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count));
                        int collationIndex = 0;
                        foreach (var collation in layer.Collations)
                        {
                            Assert.That(collation.PackId, Is.EqualTo(pack.PackId));
                            Assert.That(collation.StageLevel, Is.EqualTo(stage.StageLevel));
                            Assert.That(collation.ResultIndex, Is.EqualTo(result.ResultIndex));
                            Assert.That(collation.LayerIndex, Is.EqualTo(layer.LayerIndex));
                            Assert.That(collation.CollationIndex, Is.EqualTo(collationIndex));
                            ++collationIndex;
                        }
                        ++layerIndex;
                    }

                    Assert.That(result.Materials.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Materials.Count));
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.That(material.PackId, Is.EqualTo(pack.PackId));
                        Assert.That(material.StageLevel, Is.EqualTo(stage.StageLevel));
                        Assert.That(material.ResultIndex, Is.EqualTo(result.ResultIndex));
                        Assert.That(material.MaterialIndex, Is.EqualTo(materialIndex));

                        Assert.That(material.DatabaseMaterials.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count));
                        int databaseIndex = 0;
                        foreach (var databaseMaterial in material.DatabaseMaterials)
                        {
                            Assert.That(databaseMaterial.PackId, Is.EqualTo(pack.PackId));
                            Assert.That(databaseMaterial.StageLevel, Is.EqualTo(stage.StageLevel));
                            Assert.That(databaseMaterial.ResultIndex, Is.EqualTo(result.ResultIndex));
                            Assert.That(databaseMaterial.MaterialIndex, Is.EqualTo(material.MaterialIndex));
                            Assert.That(databaseMaterial.DatabaseMaterialIndex, Is.EqualTo(databaseIndex));
                            ++databaseIndex;
                        }
                        ++materialIndex;
                    }

                    Assert.That(result.Sections.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Sections.Count));
                    int sectionIndex = 0;
                    foreach (var section in result.Sections)
                    {
                        Assert.That(section.PackId, Is.EqualTo(pack.PackId));
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
        public void ConvertDtoToPack()
        {
            var text = File.ReadAllText("DTO/TestData/pack.json");
            var pack = JsonSerializer.Deserialize<Pack>(text);
            var dto = this.mapper.ConvertToDto(pack);
            var data = this.mapper.ConvertToData(dto);

            Assert.That(data.PackId, Is.EqualTo(pack.PackId));

            Assert.That(data.Costings.Count, Is.EqualTo(pack.Costings.Count));

            int minLevel = -1;
            Assert.That(data.Stages.Count, Is.EqualTo(pack.Stages.Count));
            int stageIndex = 0;
            foreach (var stage in data.Stages)
            {
                Assert.That(minLevel, Is.LessThanOrEqualTo((int)stage.StageLevel));
                minLevel = (int)stage.StageLevel;

                Assert.That(stage.Limits.Count, Is.EqualTo(pack.Stages[stageIndex].Limits.Count));

                Assert.That(stage.Results.Count, Is.EqualTo(pack.Stages[stageIndex].Results.Count));
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.That(result.Layers.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Layers.Count));
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.That(layer.Collations.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count));
                        ++layerIndex;
                    }

                    Assert.That(result.Materials.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Materials.Count));
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.That(material.DatabaseMaterials.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count));
                        ++materialIndex;
                    }

                    Assert.That(result.Sections.Count, Is.EqualTo(pack.Stages[stageIndex].Results[resultIndex].Sections.Count));
                    ++resultIndex;
                }
                ++stageIndex;
            }
        }
    }
}
