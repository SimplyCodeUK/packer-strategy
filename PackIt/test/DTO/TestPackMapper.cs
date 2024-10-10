// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DTO
{
    using System.IO;
    using System.Text.Json;
    using Xunit;
    using PackIt.DTO;
    using PackIt.Pack;

    /// <summary> (Unit Test Method) Convert a Pack to it's DTO. </summary>
    public class TestPackMapper
    {
        /// <summary> The mapper under test. </summary>
        private readonly PackMapper mapper;

        /// <summary> Setup for all unit tests here. </summary>
        public TestPackMapper()
        {
            this.mapper = new();
            Assert.NotNull(this.mapper);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void ConvertPackToDto()
        {
            var text = File.ReadAllText("DTO/TestData/pack.json");
            var pack = JsonSerializer.Deserialize<Pack>(text);
            var dto = this.mapper.ConvertToDto(pack);

            Assert.Equal(dto.PackId, pack.PackId);

            Assert.Equal(dto.Costings.Count, pack.Costings.Count);
            foreach (var costing in dto.Costings)
            {
                Assert.Equal(costing.PackId, pack.PackId);
            }

            int minLevel = -1;
            Assert.Equal(dto.Stages.Count, pack.Stages.Count);
            int stageIndex = 0;
            foreach (var stage in dto.Stages)
            {
                Assert.Equal(stage.PackId, pack.PackId);
                Assert.True(minLevel <= (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.Equal(stage.Limits.Count, pack.Stages[stageIndex].Limits.Count);
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.Equal(limit.PackId, pack.PackId);
                    Assert.Equal(limit.StageLevel, stage.StageLevel);
                    Assert.Equal(limit.LimitIndex, limitIndex);
                    ++limitIndex;
                }

                Assert.Equal(stage.Results.Count, pack.Stages[stageIndex].Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.Equal(result.PackId, pack.PackId);
                    Assert.Equal(result.StageLevel, stage.StageLevel);
                    Assert.Equal(result.ResultIndex, resultIndex);

                    Assert.Equal(result.Layers.Count, pack.Stages[stageIndex].Results[resultIndex].Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.Equal(layer.PackId, pack.PackId);
                        Assert.Equal(layer.StageLevel, stage.StageLevel);
                        Assert.Equal(layer.ResultIndex, result.ResultIndex);
                        Assert.Equal(layer.LayerIndex, layerIndex);

                        Assert.Equal(layer.Collations.Count, pack.Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count);
                        int collationIndex = 0;
                        foreach (var collation in layer.Collations)
                        {
                            Assert.Equal(collation.PackId, pack.PackId);
                            Assert.Equal(collation.StageLevel, stage.StageLevel);
                            Assert.Equal(collation.ResultIndex, result.ResultIndex);
                            Assert.Equal(collation.LayerIndex, layer.LayerIndex);
                            Assert.Equal(collation.CollationIndex, collationIndex);
                            ++collationIndex;
                        }
                        ++layerIndex;
                    }

                    Assert.Equal(result.Materials.Count, pack.Stages[stageIndex].Results[resultIndex].Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.Equal(material.PackId, pack.PackId);
                        Assert.Equal(material.StageLevel, stage.StageLevel);
                        Assert.Equal(material.ResultIndex, result.ResultIndex);
                        Assert.Equal(material.MaterialIndex, materialIndex);

                        Assert.Equal(material.DatabaseMaterials.Count, pack.Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count);
                        int databaseIndex = 0;
                        foreach (var databaseMaterial in material.DatabaseMaterials)
                        {
                            Assert.Equal(databaseMaterial.PackId, pack.PackId);
                            Assert.Equal(databaseMaterial.StageLevel, stage.StageLevel);
                            Assert.Equal(databaseMaterial.ResultIndex, result.ResultIndex);
                            Assert.Equal(databaseMaterial.MaterialIndex, material.MaterialIndex);
                            Assert.Equal(databaseMaterial.DatabaseMaterialIndex, databaseIndex);
                            ++databaseIndex;
                        }
                        ++materialIndex;
                    }

                    Assert.Equal(result.Sections.Count, pack.Stages[stageIndex].Results[resultIndex].Sections.Count);
                    int sectionIndex = 0;
                    foreach (var section in result.Sections)
                    {
                        Assert.Equal(section.PackId, pack.PackId);
                        Assert.Equal(section.StageLevel, stage.StageLevel);
                        Assert.Equal(section.ResultIndex, result.ResultIndex);
                        Assert.Equal(section.SectionIndex, sectionIndex);
                        ++sectionIndex;
                    }
                    ++resultIndex;
                }
                ++stageIndex;
            }
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void ConvertDtoToPack()
        {
            var text = File.ReadAllText("DTO/TestData/pack.json");
            var pack = JsonSerializer.Deserialize<Pack>(text);
            var dto = this.mapper.ConvertToDto(pack);
            var data = this.mapper.ConvertToData(dto);

            Assert.Equal(data.PackId, pack.PackId);

            Assert.Equal(data.Costings.Count, pack.Costings.Count);

            int minLevel = -1;
            Assert.Equal(data.Stages.Count, pack.Stages.Count);
            int stageIndex = 0;
            foreach (var stage in data.Stages)
            {
                Assert.True(minLevel <= (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.Equal(stage.Limits.Count, pack.Stages[stageIndex].Limits.Count);

                Assert.Equal(stage.Results.Count, pack.Stages[stageIndex].Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.Equal(result.Layers.Count, pack.Stages[stageIndex].Results[resultIndex].Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.Equal(layer.Collations.Count, pack.Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count);
                        ++layerIndex;
                    }

                    Assert.Equal(result.Materials.Count, pack.Stages[stageIndex].Results[resultIndex].Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.Equal(material.DatabaseMaterials.Count, pack.Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count);
                        ++materialIndex;
                    }

                    Assert.Equal(result.Sections.Count, pack.Stages[stageIndex].Results[resultIndex].Sections.Count);
                    ++resultIndex;
                }
                ++stageIndex;
            }
        }
    }
}
