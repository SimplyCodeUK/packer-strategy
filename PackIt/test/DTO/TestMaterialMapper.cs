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
    using PackIt.Material;

    /// <summary> (Unit Test Method) Convert a Material to it's DTO. </summary>
    public class TestMaterialMapper
    {
        /// <summary> The mapper under test. </summary>
        private MaterialMapper mapper;

        /// <summary> Setup for all unit tests here. </summary>
        public TestMaterialMapper()
        {
            this.mapper = new();
            Assert.NotNull(this.mapper);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void ConvertMaterialBottleToDto()
        {
            this.DoToDtoTest("DTO/TestData/material_bottle.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void ConvertDtoToMaterialBottle()
        {
            this.DoToDataTest("DTO/TestData/material_bottle.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void ConvertMaterialCrateToDto()
        {
            this.DoToDtoTest("DTO/TestData/material_crate.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void ConvertDtoToMaterialCrate()
        {
            this.DoToDataTest("DTO/TestData/material_crate.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void ConvertMaterialPalletToDto()
        {
            this.DoToDtoTest("DTO/TestData/material_pallet.json");
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void ConvertDtoToMaterialPallet()
        {
            this.DoToDataTest("DTO/TestData/material_pallet.json");
        }

        private void DoToDtoTest(string file)
        {
            var text = File.ReadAllText(file);
            var material = JsonSerializer.Deserialize<Material>(text);
            var dto = this.mapper.ConvertToDto(material);

            Assert.Equal(dto.MaterialId, material.MaterialId);

            Assert.Equal(dto.Costings.Count, material.Costings.Count);
            foreach (var costing in dto.Costings)
            {
                Assert.Equal(costing.MaterialId, material.MaterialId);
            }

            Assert.Equal(dto.Layers.Count, material.Layers.Count);
            int layerIndex = 0;
            foreach (var layer in dto.Layers)
            {
                Assert.Equal(layer.MaterialId, material.MaterialId);
                Assert.Equal(layer.LayerIndex, layerIndex);

                Assert.Equal(layer.Collations.Count, material.Layers[layerIndex].Collations.Count);
                int collationIndex = 0;
                foreach (var collation in layer.Collations)
                {
                    Assert.Equal(collation.MaterialId, material.MaterialId);
                    Assert.Equal(collation.LayerIndex, layer.LayerIndex);
                    Assert.Equal(collation.CollationIndex, collationIndex);
                    ++collationIndex;
                }
                ++layerIndex;
            }
            Assert.Equal(dto.Layers.Count, material.Layers.Count);

            int palletDeckIndex = 0;
            foreach (var palletDeck in dto.PalletDecks)
            {
                Assert.Equal(palletDeck.MaterialId, material.MaterialId);
                Assert.Equal(palletDeck.PalletDeckIndex, palletDeckIndex);

                Assert.Equal(palletDeck.Planks.Count, material.PalletDecks[palletDeckIndex].Planks.Count);
                int plankIndex = 0;
                foreach (var plank in palletDeck.Planks)
                {
                    Assert.Equal(plank.MaterialId, material.MaterialId);
                    Assert.Equal(plank.PalletDeckIndex, palletDeck.PalletDeckIndex);
                    Assert.Equal(plank.PlankIndex, plankIndex);
                    ++plankIndex;
                }
                ++palletDeckIndex;
            }

            int sectionIndex = 0;
            foreach (var section in dto.Sections)
            {
                Assert.Equal(section.MaterialId, material.MaterialId);
                Assert.Equal(section.SectionIndex, sectionIndex);
                ++sectionIndex;
            }
        }

        private void DoToDataTest(string file)
        {
            var text = File.ReadAllText(file);
            var material = JsonSerializer.Deserialize<Material>(text);
            var dto = this.mapper.ConvertToDto(material);
            var data = this.mapper.ConvertToData(dto);

            Assert.Equal(data.MaterialId, material.MaterialId);

            Assert.Equal(data.Costings.Count, material.Costings.Count);

            Assert.Equal(data.Layers.Count, material.Layers.Count);
            int layerIndex = 0;
            foreach (var layer in data.Layers)
            {
                Assert.Equal(layer.Collations.Count, material.Layers[layerIndex].Collations.Count);
                ++layerIndex;
            }
            Assert.Equal(data.Layers.Count, material.Layers.Count);

            int palletDeckIndex = 0;
            foreach (var palletDeck in data.PalletDecks)
            {
                Assert.Equal(palletDeck.Planks.Count, material.PalletDecks[palletDeckIndex].Planks.Count);
                ++palletDeckIndex;
            }
        }
    }
}
