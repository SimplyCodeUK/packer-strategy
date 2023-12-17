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
            Assert.That(this.mapper, Is.Not.Null);
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

            Assert.That(dto.MaterialId, Is.EqualTo(material.MaterialId));

            Assert.That(dto.Costings.Count, Is.EqualTo(material.Costings.Count));
            foreach (var costing in dto.Costings)
            {
                Assert.That(costing.MaterialId, Is.EqualTo(material.MaterialId));
            }

            Assert.That(dto.Layers.Count, Is.EqualTo(material.Layers.Count));
            int layerIndex = 0;
            foreach (var layer in dto.Layers)
            {
                Assert.That(layer.MaterialId, Is.EqualTo(material.MaterialId));
                Assert.That(layer.LayerIndex, Is.EqualTo(layerIndex));

                Assert.That(layer.Collations.Count, Is.EqualTo(material.Layers[layerIndex].Collations.Count));
                int collationIndex = 0;
                foreach (var collation in layer.Collations)
                {
                    Assert.That(collation.MaterialId, Is.EqualTo(material.MaterialId));
                    Assert.That(collation.LayerIndex, Is.EqualTo(layer.LayerIndex));
                    Assert.That(collation.CollationIndex, Is.EqualTo(collationIndex));
                    ++collationIndex;
                }
                ++layerIndex;
            }
            Assert.That(dto.Layers.Count, Is.EqualTo(material.Layers.Count));

            int palletDeckIndex = 0;
            foreach (var palletDeck in dto.PalletDecks)
            {
                Assert.That(palletDeck.MaterialId, Is.EqualTo(material.MaterialId));
                Assert.That(palletDeck.PalletDeckIndex, Is.EqualTo(palletDeckIndex));

                Assert.That(palletDeck.Planks.Count, Is.EqualTo(material.PalletDecks[palletDeckIndex].Planks.Count));
                int plankIndex = 0;
                foreach (var plank in palletDeck.Planks)
                {
                    Assert.That(plank.MaterialId, Is.EqualTo(material.MaterialId));
                    Assert.That(plank.PalletDeckIndex, Is.EqualTo(palletDeck.PalletDeckIndex));
                    Assert.That(plank.PlankIndex, Is.EqualTo(plankIndex));
                    ++plankIndex;
                }
                ++palletDeckIndex;
            }

            int sectionIndex = 0;
            foreach (var section in dto.Sections)
            {
                Assert.That(section.MaterialId, Is.EqualTo(material.MaterialId));
                Assert.That(section.SectionIndex, Is.EqualTo(sectionIndex));
                ++sectionIndex;
            }
        }

        private void DoToDataTest(string file)
        {
            var text = File.ReadAllText(file);
            var material = JsonSerializer.Deserialize<Material>(text);
            var dto = this.mapper.ConvertToDto(material);
            var data = this.mapper.ConvertToData(dto);

            Assert.That(data.MaterialId, Is.EqualTo(material.MaterialId));

            Assert.That(data.Costings.Count, Is.EqualTo(material.Costings.Count));

            Assert.That(data.Layers.Count, Is.EqualTo(material.Layers.Count));
            int layerIndex = 0;
            foreach (var layer in data.Layers)
            {
                Assert.That(layer.Collations.Count, Is.EqualTo(material.Layers[layerIndex].Collations.Count));
                ++layerIndex;
            }
            Assert.That(data.Layers.Count, Is.EqualTo(material.Layers.Count));

            int palletDeckIndex = 0;
            foreach (var palletDeck in data.PalletDecks)
            {
                Assert.That(palletDeck.Planks.Count, Is.EqualTo(material.PalletDecks[palletDeckIndex].Planks.Count));
                ++palletDeckIndex;
            }
        }
    }
}
