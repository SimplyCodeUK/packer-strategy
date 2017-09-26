// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItTest.DTO
{
    using System.IO;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using PackIt.DTO;
    using PackIt.DTO.DtoPack;
    using PackIt.Pack;

    /// <summary>   (Unit Test Method) Convert a Pack to it's DTO. </summary>
    [TestFixture]
    public class TestPackMapper
    {
        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertPack()
        {
            string text = File.ReadAllText("DTO/TestData/pack.json");
            var pack = JsonConvert.DeserializeObject<Pack>(text);
            var dto = PackMapper.Convert(pack);

            Assert.AreEqual(dto.PackId, pack.PackId);

            Assert.AreEqual(dto.Costings.Count, pack.Costings.Count);
            foreach (DtoCosting costing in dto.Costings)
            {
                Assert.AreEqual(costing.PackId, pack.PackId);
            }

            int minLevel = -1;
            Assert.AreEqual(dto.Stages.Count, pack.Stages.Count);
            int stageIndex = 0;
            foreach (DtoStage stage in dto.Stages)
            {
                Assert.AreEqual(stage.PackId, pack.PackId);
                Assert.Greater((int)stage.StageLevel, minLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(stage.Limits.Count, pack.Stages[stageIndex].Limits.Count);
                int limitIndex = 0;
                foreach (DtoLimit limit in stage.Limits)
                {
                    Assert.AreEqual(limit.PackId, pack.PackId);
                    Assert.AreEqual(limit.StageLevel, stage.StageLevel);
                    Assert.AreEqual(limit.LimitIndex, limitIndex);

                    ++limitIndex;
                }

                Assert.AreEqual(stage.Results.Count, pack.Stages[stageIndex].Results.Count);
                int resultIndex = 0;
                foreach (DtoResult result in stage.Results)
                {
                    Assert.AreEqual(result.PackId, pack.PackId);
                    Assert.AreEqual(result.StageLevel, stage.StageLevel);
                    Assert.AreEqual(result.ResultIndex, resultIndex);

                    Assert.AreEqual(result.Layers.Count, pack.Stages[stageIndex].Results[resultIndex].Layers.Count);
                    int layerIndex = 0;
                    foreach (DtoLayer layer in result.Layers)
                    {
                        Assert.AreEqual(layer.PackId, pack.PackId);
                        Assert.AreEqual(layer.StageLevel, stage.StageLevel);
                        Assert.AreEqual(layer.ResultIndex, result.ResultIndex);
                        Assert.AreEqual(layer.LayerIndex, layerIndex);

                        Assert.AreEqual(layer.Collations.Count, pack.Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count);
                        int collationIndex = 0;
                        foreach (DtoCollation collation in layer.Collations)
                        {
                            Assert.AreEqual(collation.PackId, pack.PackId);
                            Assert.AreEqual(collation.StageLevel, stage.StageLevel);
                            Assert.AreEqual(collation.ResultIndex, result.ResultIndex);
                            Assert.AreEqual(collation.LayerIndex, layer.LayerIndex);
                            Assert.AreEqual(collation.CollationIndex, collationIndex);

                            ++collationIndex;
                        }

                        ++layerIndex;
                    }

                    Assert.AreEqual(result.Materials.Count, pack.Stages[stageIndex].Results[resultIndex].Materials.Count);
                    int materialIndex = 0;
                    foreach (DtoMaterial material in result.Materials)
                    {
                        Assert.AreEqual(material.PackId, pack.PackId);
                        Assert.AreEqual(material.StageLevel, stage.StageLevel);
                        Assert.AreEqual(material.ResultIndex, result.ResultIndex);
                        Assert.AreEqual(material.MaterialIndex, materialIndex);

                        Assert.AreEqual(material.DatabaseMaterials.Count, pack.Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count);
                        int databaseIndex = 0;
                        foreach (DtoDatabaseMaterial databaseMaterial in material.DatabaseMaterials)
                        {
                            Assert.AreEqual(databaseMaterial.PackId, pack.PackId);
                            Assert.AreEqual(databaseMaterial.StageLevel, stage.StageLevel);
                            Assert.AreEqual(databaseMaterial.ResultIndex, result.ResultIndex);
                            Assert.AreEqual(databaseMaterial.MaterialIndex, material.MaterialIndex);
                            Assert.AreEqual(databaseMaterial.DatabaseMaterialIndex, databaseIndex);

                            ++databaseIndex;
                        }

                        ++materialIndex;
                    }

                    Assert.AreEqual(result.Sections.Count, pack.Stages[stageIndex].Results[resultIndex].Sections.Count);
                    int sectionIndex = 0;
                    foreach (DtoSection section in result.Sections)
                    {
                        Assert.AreEqual(section.PackId, pack.PackId);
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
    }
}
