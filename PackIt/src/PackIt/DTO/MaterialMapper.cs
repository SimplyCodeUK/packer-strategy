// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackIt.DTO.DtoMaterial;
    using PackIt.Material;

    /// <summary>
    /// Maps from and to DtoPlan
    /// </summary>
    public static class MaterialMapper
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static MapperConfiguration configModelToDto = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Material, DtoMaterial.DtoMaterial>();
                cfg.CreateMap<Costing, DtoCosting>();
                cfg.CreateMap<Layer, DtoLayer>();
                cfg.CreateMap<Collation, DtoCollation>();
                cfg.CreateMap<Material, DtoMaterial.DtoMaterial>().AfterMap(
                    (s, d) =>
                    {
                        foreach (DtoCosting costing in d.Costings)
                        {
                            costing.MaterialId = s.MaterialId;
                        }

                        foreach (DtoLayer layer in d.Layers)
                        {
                            layer.MaterialId = s.MaterialId;

                            int collationIndex = 0;
                            foreach (DtoCollation collation in layer.Collations)
                            {
                                collation.MaterialId = s.MaterialId;
                                collation.LayerIndex = layer.LayerIndex;
                                collation.CollationIndex = collationIndex++;
                            }
                        }
                    });
            });

        /// <summary> Configuration of map from Dto to Model. </summary>
        private static MapperConfiguration configDtoToModel = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<DtoMaterial.DtoMaterial, Material>();
                cfg.CreateMap<DtoCosting, Costing>();
                cfg.CreateMap<DtoLayer, Layer>();
                cfg.CreateMap<DtoCollation, Collation>();
            });

        /// <summary> The mapper from Model to Dto.</summary>
        private static IMapper mapperModelToDto = configModelToDto.CreateMapper();

        /// <summary> The mapper from Dto to Model.</summary>
        private static IMapper mapperDtoToModel = configDtoToModel.CreateMapper();

        /// <summary> Converts a Material to its DTO. </summary>
        /// 
        /// <param name="material"> Material to convert. </param>
        /// 
        /// <returns> The converted dto. </returns>
        public static DtoMaterial.DtoMaterial Convert(Material material)
        {
            DtoMaterial.DtoMaterial ret = mapperModelToDto.Map<DtoMaterial.DtoMaterial>(material);

            return ret;
        }

        /// <summary> Converts a DTO to its Material. </summary>
        ///
        /// <param name="material"> Dto to convert. </param>
        ///
        /// <returns> The converted Material. </returns>
        public static Material Convert(DtoMaterial.DtoMaterial material)
        {
            Material ret = mapperDtoToModel.Map<Material>(material);

            return ret;
        }
    }
}
