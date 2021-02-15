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
    using PackIt.DTO.DtoMaterial;
    using PackIt.Material;

    /// <summary> (Unit Test Method) Convert a Material to it's DTO. </summary>
    [TestFixture]
    public class TestMaterialMapper
    {
        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertMaterial()
        {
            var text = File.ReadAllText("DTO/TestData/material.json");
            var material = JsonConvert.DeserializeObject<Material>(text);
            var dto = MaterialMapper.Convert(material);

            Assert.AreEqual(dto.MaterialId, material.MaterialId);

            Assert.AreEqual(dto.Costings.Count, material.Costings.Count);
            foreach (var costing in dto.Costings)
            {
                Assert.AreEqual(costing.MaterialId, material.MaterialId);
            }

            Assert.AreEqual(dto.Layers.Count, material.Layers.Count);
            int layerIndex = 0;
            foreach (var layer in dto.Layers)
            {
                Assert.AreEqual(layer.MaterialId, material.MaterialId);
                Assert.AreEqual(layer.LayerIndex, layerIndex);

                Assert.AreEqual(layer.Collations.Count, material.Layers[layerIndex].Collations.Count);
                int collationIndex = 0;
                foreach (var collation in layer.Collations)
                {
                    Assert.AreEqual(collation.MaterialId, material.MaterialId);
                    Assert.AreEqual(collation.LayerIndex, layer.LayerIndex);
                    Assert.AreEqual(collation.CollationIndex, collationIndex);

                    ++collationIndex;
                }

                ++layerIndex;
            }
        }
    }
}
