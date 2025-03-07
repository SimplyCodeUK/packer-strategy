// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DTO
{
    using System.IO;
    using Xunit;
    using PackIt.DTO;
    using PackIt.Drawing;
    using System.Text.Json;

    /// <summary> (Unit Test Method) Convert a Pack to it's DTO. </summary>
    public class TestDrawingMapper
    {
        /// <summary> The mapper under test. </summary>
        private readonly DrawingMapper mapper;

        /// <summary> Setup for all unit tests here. </summary>
        public TestDrawingMapper()
        {
            this.mapper = new();
            Assert.NotNull(this.mapper);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void ConvertDrawingToDto()
        {
            var text = File.ReadAllText("DTO/TestData/drawing.json");
            var drawing = JsonSerializer.Deserialize<Drawing>(text);
            var dto = this.mapper.ConvertToDto(drawing);

            Assert.Equal(drawing.DrawingId, dto.DrawingId);

            Assert.Equal(drawing.Packs[0].Costings.Count, dto.Packs[0].Costings.Count);
            foreach (var costing in dto.Packs[0].Costings)
            {
                Assert.Equal(drawing.DrawingId, costing.PackId);
            }

            int minLevel = -1;
            Assert.Equal(drawing.Packs[0].Stages.Count, dto.Packs[0].Stages.Count);
            int stageIndex = 0;
            foreach (var stage in dto.Packs[0].Stages)
            {
                Assert.Equal(drawing.DrawingId, stage.PackId);
                Assert.True(minLevel <= ((int)stage.StageLevel));
                minLevel = (int)stage.StageLevel;

                Assert.Equal(drawing.Packs[0].Stages[stageIndex].Limits.Count, stage.Limits.Count);
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.Equal(drawing.DrawingId, limit.PackId);
                    Assert.Equal(stage.StageLevel, limit.StageLevel);
                    Assert.Equal(limitIndex, limit.LimitIndex);
                    ++limitIndex;
                }

                Assert.Equal(drawing.Packs[0].Stages[stageIndex].Results.Count, stage.Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.Equal(drawing.DrawingId, result.PackId);
                    Assert.Equal(stage.StageLevel, result.StageLevel);
                    Assert.Equal(resultIndex, result.ResultIndex);

                    Assert.Equal(result.Layers.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.Equal(drawing.DrawingId, layer.PackId);
                        Assert.Equal(stage.StageLevel, layer.StageLevel);
                        Assert.Equal(result.ResultIndex, layer.ResultIndex);
                        Assert.Equal(layerIndex, layer.LayerIndex);

                        Assert.Equal(layer.Collations.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count);
                        int collationIndex = 0;
                        foreach (var collation in layer.Collations)
                        {
                            Assert.Equal(collation.PackId, drawing.DrawingId);
                            Assert.Equal(collation.StageLevel, stage.StageLevel);
                            Assert.Equal(collation.ResultIndex, result.ResultIndex);
                            Assert.Equal(collation.LayerIndex, layer.LayerIndex);
                            Assert.Equal(collation.CollationIndex, collationIndex);
                            ++collationIndex;
                        }
                        ++layerIndex;
                    }

                    Assert.Equal(result.Materials.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.Equal(material.PackId, drawing.DrawingId);
                        Assert.Equal(material.StageLevel, stage.StageLevel);
                        Assert.Equal(material.ResultIndex, result.ResultIndex);
                        Assert.Equal(material.MaterialIndex, materialIndex);

                        Assert.Equal(material.DatabaseMaterials.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count);
                        int databaseIndex = 0;
                        foreach (var databaseMaterial in material.DatabaseMaterials)
                        {
                            Assert.Equal(databaseMaterial.PackId, drawing.DrawingId);
                            Assert.Equal(databaseMaterial.StageLevel, stage.StageLevel);
                            Assert.Equal(databaseMaterial.ResultIndex, result.ResultIndex);
                            Assert.Equal(databaseMaterial.MaterialIndex, material.MaterialIndex);
                            Assert.Equal(databaseMaterial.DatabaseMaterialIndex, databaseIndex);
                            ++databaseIndex;
                        }
                        ++materialIndex;
                    }

                    Assert.Equal(result.Sections.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Sections.Count);
                    int sectionIndex = 0;
                    foreach (var section in result.Sections)
                    {
                        Assert.Equal(section.PackId, drawing.DrawingId);
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
        public void ConvertDtoDrawing()
        {
            var text = File.ReadAllText("DTO/TestData/drawing.json");
            var drawing = JsonSerializer.Deserialize<Drawing>(text);
            var dto = this.mapper.ConvertToDto(drawing);
            var data = this.mapper.ConvertToData(dto);

            Assert.Equal(data.DrawingId, drawing.DrawingId);

            Assert.Equal(data.Packs[0].Costings.Count, drawing.Packs[0].Costings.Count);

            int minLevel = -1;
            Assert.Equal(data.Packs[0].Stages.Count, drawing.Packs[0].Stages.Count);
            int stageIndex = 0;
            foreach (var stage in data.Packs[0].Stages)
            {
                Assert.True(minLevel <= (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.Equal(stage.Limits.Count, drawing.Packs[0].Stages[stageIndex].Limits.Count);

                Assert.Equal(stage.Results.Count, drawing.Packs[0].Stages[stageIndex].Results.Count);
                int resultIndex = 0;
                foreach (var result in stage.Results)
                {
                    Assert.Equal(result.Layers.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers.Count);
                    int layerIndex = 0;
                    foreach (var layer in result.Layers)
                    {
                        Assert.Equal(layer.Collations.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count);
                        ++layerIndex;
                    }

                    Assert.Equal(result.Materials.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials.Count);
                    int materialIndex = 0;
                    foreach (var material in result.Materials)
                    {
                        Assert.Equal(material.DatabaseMaterials.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count);
                        ++materialIndex;
                    }

                    Assert.Equal(result.Sections.Count, drawing.Packs[0].Stages[stageIndex].Results[resultIndex].Sections.Count);
                    ++resultIndex;
                }
                ++stageIndex;
            }
        }
    }
}