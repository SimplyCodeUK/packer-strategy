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
    using PackIt.DTO.DtoPackage;
    using PackIt.Models.Package;

    /// <summary>   (Unit Test Fixture) a mapper for packs. </summary>
    [TestFixture]
    public class TestPackMapper
    {
        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertPack()
        {
            string text = File.ReadAllText(@"DTO\TestData\pack.json");
            Package pack = JsonConvert.DeserializeObject<Package>(text);

            DtoPackage dto = PackageMapper.Convert(pack);

            Assert.AreEqual(dto.Id, pack.Id);

            Assert.AreEqual(dto.Costings.Count, pack.Costings.Count);
            foreach (DtoCosting costing in dto.Costings)
            {
                Assert.AreEqual(dto.Costings[0].PackageId, pack.Id);
            }

            int minLevel = -1;
            Assert.AreEqual(dto.Stages.Count, pack.Stages.Count);
            int stageIndex = 0;
            foreach (DtoStage stage in dto.Stages)
            {
                Assert.AreEqual(stage.PackageId, pack.Id);
                Assert.Greater((int)stage.Level, minLevel);
                minLevel = (int)stage.Level;

                Assert.AreEqual(stage.Limits.Count, pack.Stages[stageIndex].Limits.Count);
                int limitIndex = 0;
                foreach (DtoLimit limit in stage.Limits)
                {
                    Assert.AreEqual(limit.PackageId, pack.Id);
                    Assert.AreEqual(limit.StageLevel, stage.Level);
                    Assert.AreEqual(limit.Index, limitIndex);

                    ++limitIndex;
                }

                Assert.AreEqual(stage.Results.Count, pack.Stages[stageIndex].Results.Count);
                int resultIndex = 0;
                foreach (DtoResult result in stage.Results)
                {
                    Assert.AreEqual(result.PackageId, pack.Id);
                    Assert.AreEqual(result.StageLevel, stage.Level);
                    Assert.AreEqual(result.Index, resultIndex);

                    Assert.AreEqual(result.Layers.Count, pack.Stages[stageIndex].Results[resultIndex].Layers.Count);
                    int layerIndex = 0;
                    foreach (DtoLayer layer in result.Layers)
                    {
                        Assert.AreEqual(layer.PackageId, pack.Id);
                        Assert.AreEqual(layer.StageLevel, stage.Level);
                        Assert.AreEqual(layer.ResultIndex, result.Index);
                        Assert.AreEqual(layer.Index, layerIndex);

                        Assert.AreEqual(layer.Collations.Count, pack.Stages[stageIndex].Results[resultIndex].Layers[layerIndex].Collations.Count);
                        int collationIndex = 0;
                        foreach (DtoCollation collation in layer.Collations)
                        {
                            Assert.AreEqual(collation.PackageId, pack.Id);
                            Assert.AreEqual(collation.StageLevel, stage.Level);
                            Assert.AreEqual(collation.ResultIndex, result.Index);
                            Assert.AreEqual(collation.LayerIndex, layer.Index);
                            Assert.AreEqual(collation.Index, collationIndex);

                            ++collationIndex;
                        }

                        ++layerIndex;
                    }

                    Assert.AreEqual(result.Materials.Count, pack.Stages[stageIndex].Results[resultIndex].Materials.Count);
                    int materialIndex = 0;
                    foreach (DtoMaterial material in result.Materials)
                    {
                        Assert.AreEqual(material.PackageId, pack.Id);
                        Assert.AreEqual(material.StageLevel, stage.Level);
                        Assert.AreEqual(material.ResultIndex, result.Index);
                        Assert.AreEqual(material.Index, materialIndex);

                        Assert.AreEqual(material.DatabaseMaterials.Count, pack.Stages[stageIndex].Results[resultIndex].Materials[materialIndex].DatabaseMaterials.Count);
                        int databaseIndex = 0;
                        foreach (DtoDatabaseMaterial databaseMaterial in material.DatabaseMaterials)
                        {
                            Assert.AreEqual(databaseMaterial.PackageId, pack.Id);
                            Assert.AreEqual(databaseMaterial.StageLevel, stage.Level);
                            Assert.AreEqual(databaseMaterial.ResultIndex, result.Index);
                            Assert.AreEqual(databaseMaterial.MaterialIndex, material.Index);
                            Assert.AreEqual(databaseMaterial.Index, databaseIndex);

                            ++databaseIndex;
                        }

                        ++materialIndex;
                    }

                    Assert.AreEqual(result.Sections.Count, pack.Stages[stageIndex].Results[resultIndex].Sections.Count);
                    int sectionIndex = 0;
                    foreach (DtoSection section in result.Sections)
                    {
                        Assert.AreEqual(section.PackageId, pack.Id);
                        Assert.AreEqual(section.StageLevel, stage.Level);
                        Assert.AreEqual(section.ResultIndex, result.Index);
                        Assert.AreEqual(section.Index, sectionIndex);

                        ++sectionIndex;
                    }

                    ++resultIndex;
                }

                ++stageIndex;
            }
        }
    }
}
