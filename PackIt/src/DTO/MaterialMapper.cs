// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackIt.Material;

    /// <summary> Maps from and to DtoMaterial </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.IPackItMapper{TData, TDtoData}"/>
    public class MaterialMapper : IPackItMapper<Material, DtoMaterial.DtoMaterial>
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static readonly MapperConfiguration configModelToDto = new(
            cfg =>
            {
                cfg.CreateMap<Costing, DtoMaterial.DtoCosting>();
                cfg.CreateMap<Layer, DtoMaterial.DtoLayer>();
                cfg.CreateMap<PalletDeck, DtoMaterial.DtoPalletDeck>();
                cfg.CreateMap<Section, DtoMaterial.DtoSection>();
                cfg.CreateMap<Collation, DtoMaterial.DtoCollation>();
                cfg.CreateMap<Plank, DtoMaterial.DtoPlank>();
                cfg.CreateMap<Material, DtoMaterial.DtoMaterial>().AfterMap(
                    (s, d) =>
                    {
                        foreach (var costing in d.Costings)
                        {
                            costing.MaterialId = s.MaterialId;
                        }

                        int layerIndex = 0;
                        foreach (var layer in d.Layers)
                        {
                            layer.MaterialId = s.MaterialId;
                            layer.LayerIndex = layerIndex++;

                            int collationIndex = 0;
                            foreach (var collation in layer.Collations)
                            {
                                collation.MaterialId = s.MaterialId;
                                collation.LayerIndex = layer.LayerIndex;
                                collation.CollationIndex = collationIndex++;
                            }
                        }

                        int palletDeckIndex = 0;
                        foreach (var palletDeck in d.PalletDecks)
                        {
                            palletDeck.MaterialId = s.MaterialId;
                            palletDeck.PalletDeckIndex = palletDeckIndex++;

                            int plankIndex = 0;
                            foreach (var plank in palletDeck.Planks)
                            {
                                plank.MaterialId = s.MaterialId;
                                plank.PalletDeckIndex = palletDeck.PalletDeckIndex;
                                plank.PlankIndex = plankIndex++;
                            }
                        }

                        int sectionIndex = 0;
                        foreach (var section in d.Sections)
                        {
                            section.MaterialId = s.MaterialId;
                            section.SectionIndex = sectionIndex++;
                        }
                    });
            });

        /// <summary> Configuration of map from Dto to Model. </summary>
        private static readonly MapperConfiguration configDtoToModel = new(
            cfg =>
            {
                cfg.CreateMap<DtoMaterial.DtoMaterial, Material>();
                cfg.CreateMap<DtoMaterial.DtoCosting, Costing>();
                cfg.CreateMap<DtoMaterial.DtoLayer, Layer>();
                cfg.CreateMap<DtoMaterial.DtoPalletDeck, PalletDeck>();
                cfg.CreateMap<DtoMaterial.DtoSection, Section>();
                cfg.CreateMap<DtoMaterial.DtoCollation, Collation>();
                cfg.CreateMap<DtoMaterial.DtoPlank, Plank>();
            });

        /// <summary> The mapper from Model to Dto. </summary>
        private static readonly IMapper mapperModelToDto = configModelToDto.CreateMapper();

        /// <summary> The mapper from Dto to Model. </summary>
        private static readonly IMapper mapperDtoToModel = configDtoToModel.CreateMapper();

        /// <summary> Converts a Data to its DTO. </summary>
        /// 
        /// <param name="data"> Data to convert. </param>
        /// 
        /// <returns> The converted DTO. </returns>
        public DtoMaterial.DtoMaterial ConvertToDto(Material data)
        {
            return mapperModelToDto.Map<DtoMaterial.DtoMaterial>(data);
        }

        /// <summary> Converts a DTO to its Data. </summary>
        ///
        /// <param name="dtoData"> DTO to convert. </param>
        ///
        /// <returns> The converted Data. </returns>
        public Material ConvertToData(DtoMaterial.DtoMaterial dtoData)
        {
            return mapperDtoToModel.Map<Material>(dtoData);
        }

        /// <summary> Get Data key. </summary>
        ///
        /// <param name="data"> Data to get the key for. </param>
        ///
        /// <returns> The key. </returns>
        public string KeyForData(Material data)
        {
            return data.MaterialId;
        }
    }
}
