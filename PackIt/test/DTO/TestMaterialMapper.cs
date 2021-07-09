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
            Assert.AreEqual(dto.Layers.Count, material.Layers.Count);

            int palletDeckIndex = 0;
            foreach (var palletDeck in dto.PalletDecks)
            {
                Assert.AreEqual(palletDeck.MaterialId, material.MaterialId);
                Assert.AreEqual(palletDeck.PalletDeckIndex, palletDeckIndex);

                Assert.AreEqual(palletDeck.Planks.Count, material.PalletDecks[palletDeckIndex].Planks.Count);
                int plankIndex = 0;
                foreach (var plank in palletDeck.Planks)
                {
                    Assert.AreEqual(plank.MaterialId, material.MaterialId);
                    Assert.AreEqual(plank.PalletDeckIndex, palletDeck.PalletDeckIndex);
                    Assert.AreEqual(plank.PlankIndex, plankIndex);
                    ++plankIndex;
                }
                ++palletDeckIndex;
            }

            int sectionIndex = 0;
            foreach (var section in dto.Sections)
            {
                Assert.AreEqual(section.MaterialId, material.MaterialId);
                Assert.AreEqual(section.SectionIndex, sectionIndex);
                ++sectionIndex;
            }
        }

        private void DoToDataTest(string file)
        {
            var text = File.ReadAllText(file);
            var material = JsonSerializer.Deserialize<Material>(text);
            var dto = this.mapper.ConvertToDto(material);
            var data = this.mapper.ConvertToData(dto);

            Assert.AreEqual(data.MaterialId, material.MaterialId);

            Assert.AreEqual(data.Costings.Count, material.Costings.Count);

            Assert.AreEqual(data.Layers.Count, material.Layers.Count);
            int layerIndex = 0;
            foreach (var layer in data.Layers)
            {
                Assert.AreEqual(layer.Collations.Count, material.Layers[layerIndex].Collations.Count);
                ++layerIndex;
            }
            Assert.AreEqual(data.Layers.Count, material.Layers.Count);

            int palletDeckIndex = 0;
            foreach (var palletDeck in data.PalletDecks)
            {
                Assert.AreEqual(palletDeck.Planks.Count, material.PalletDecks[palletDeckIndex].Planks.Count);
                ++palletDeckIndex;
            }
        }
    }
}
