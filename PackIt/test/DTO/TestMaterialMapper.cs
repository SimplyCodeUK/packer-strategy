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
    using PackIt.Material;

    /// <summary> (Unit Test Method) Convert a Material to it's DTO. </summary>
    [TestFixture]
    public class TestMaterialMapper
    {
        /// <summary> The mapper under test. </summary>
        private MaterialMapper mapper;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.mapper = new();
            Assert.IsNotNull(this.mapper);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertMaterialBottleToDto()
        {
            this.DoToDtoTest("DTO/TestData/material_bottle.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertDtoToMaterialBottle()
        {
            this.DoToDataTest("DTO/TestData/material_bottle.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertMaterialCrateToDto()
        {
            this.DoToDtoTest("DTO/TestData/material_crate.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertDtoToMaterialCrate()
        {
            this.DoToDataTest("DTO/TestData/material_crate.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertMaterialPalletToDto()
        {
            this.DoToDtoTest("DTO/TestData/material_pallet.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void ConvertDtoToMaterialPallet()
        {
            this.DoToDataTest("DTO/TestData/material_pallet.json");
        }

        private void DoToDtoTest(string file)
        {
            var text = File.ReadAllText(file);
            var material = JsonSerializer.Deserialize<Material>(text);
            var dto = this.mapper.ConvertToDto(material);

            Assert.AreEqual(material.MaterialId, dto.MaterialId);

            Assert.AreEqual(material.Costings.Count, dto.Costings.Count);
            foreach (var costing in dto.Costings)
            {
                Assert.AreEqual(material.MaterialId, costing.MaterialId);
            }

            Assert.AreEqual(material.Layers.Count, dto.Layers.Count);
            int layerIndex = 0;
            foreach (var layer in dto.Layers)
            {
                Assert.AreEqual(material.MaterialId, layer.MaterialId);
                Assert.AreEqual(layerIndex, layer.LayerIndex);

                Assert.AreEqual(material.Layers[layerIndex].Collations.Count, layer.Collations.Count);
                int collationIndex = 0;
                foreach (var collation in layer.Collations)
                {
                    Assert.AreEqual(material.MaterialId, collation.MaterialId);
                    Assert.AreEqual(layer.LayerIndex, collation.LayerIndex);
                    Assert.AreEqual(collationIndex, collation.CollationIndex);
                    ++collationIndex;
                }
                ++layerIndex;
            }
            Assert.AreEqual(material.Layers.Count, dto.Layers.Count);

            int palletDeckIndex = 0;
            foreach (var palletDeck in dto.PalletDecks)
            {
                Assert.AreEqual(material.MaterialId, palletDeck.MaterialId);
                Assert.AreEqual(palletDeckIndex, palletDeck.PalletDeckIndex);

                Assert.AreEqual(material.PalletDecks[palletDeckIndex].Planks.Count, palletDeck.Planks.Count);
                int plankIndex = 0;
                foreach (var plank in palletDeck.Planks)
                {
                    Assert.AreEqual(material.MaterialId, plank.MaterialId);
                    Assert.AreEqual(palletDeck.PalletDeckIndex, plank.PalletDeckIndex);
                    Assert.AreEqual(plankIndex, plank.PlankIndex);
                    ++plankIndex;
                }
                ++palletDeckIndex;
            }

            int sectionIndex = 0;
            foreach (var section in dto.Sections)
            {
                Assert.AreEqual(material.MaterialId, section.MaterialId);
                Assert.AreEqual(sectionIndex, section.SectionIndex);
                ++sectionIndex;
            }
        }

        private void DoToDataTest(string file)
        {
            var text = File.ReadAllText(file);
            var material = JsonSerializer.Deserialize<Material>(text);
            var dto = this.mapper.ConvertToDto(material);
            var data = this.mapper.ConvertToData(dto);

            Assert.AreEqual(material.MaterialId, data.MaterialId);

            Assert.AreEqual(material.Costings.Count, data.Costings.Count);

            Assert.AreEqual(material.Layers.Count, data.Layers.Count);
            int layerIndex = 0;
            foreach (var layer in data.Layers)
            {
                Assert.AreEqual(material.Layers[layerIndex].Collations.Count, layer.Collations.Count);
                ++layerIndex;
            }
            Assert.AreEqual(material.Layers.Count, data.Layers.Count);

            int palletDeckIndex = 0;
            foreach (var palletDeck in data.PalletDecks)
            {
                Assert.AreEqual(material.PalletDecks[palletDeckIndex].Planks.Count, palletDeck.Planks.Count);
                ++palletDeckIndex;
            }
        }
    }
}
