﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackIt.DTO.DtoPack;
    using PackIt.Pack;

    /// <summary>
    /// Maps from and to DtoPack
    /// </summary>
    public static class PackMapper
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static MapperConfiguration configModelToDto = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Pack, DtoPack.DtoPack>();
                cfg.CreateMap<Costing, DtoCosting>();
                cfg.CreateMap<Stage, DtoStage>();
                cfg.CreateMap<Limit, DtoLimit>();
                cfg.CreateMap<Result, DtoResult>();
                cfg.CreateMap<Layer, DtoLayer>();
                cfg.CreateMap<Collation, DtoCollation>();
                cfg.CreateMap<Material, DtoPack.DtoMaterial>();
                cfg.CreateMap<DatabaseMaterial, DtoDatabaseMaterial>();
                cfg.CreateMap<Section, DtoSection>();
                cfg.CreateMap<Pack, DtoPack.DtoPack>().AfterMap(
                    (s, d) =>
                    {
                        foreach (DtoCosting costing in d.Costings)
                        {
                            costing.PackId = s.PackId;
                        }

                        foreach (DtoStage stage in d.Stages)
                        {
                            stage.PackId = s.PackId;

                            long limitIndex = 0;
                            foreach (DtoLimit limit in stage.Limits)
                            {
                                limit.PackId = s.PackId;
                                limit.StageLevel = stage.StageLevel;
                                limit.LimitIndex = limitIndex++;
                            }

                            long resultIndex = 0;
                            foreach (DtoResult result in stage.Results)
                            {
                                result.PackId = s.PackId;
                                result.StageLevel = stage.StageLevel;
                                result.ResultIndex = resultIndex++;

                                int layerIndex = 0;
                                foreach (DtoLayer layer in result.Layers)
                                {
                                    layer.PackId = s.PackId;
                                    layer.StageLevel = stage.StageLevel;
                                    layer.ResultIndex = result.ResultIndex;
                                    layer.LayerIndex = layerIndex++;

                                    int collationIndex = 0;
                                    foreach (DtoCollation collation in layer.Collations)
                                    {
                                        collation.PackId = s.PackId;
                                        collation.StageLevel = stage.StageLevel;
                                        collation.ResultIndex = result.ResultIndex;
                                        collation.LayerIndex = layer.LayerIndex;
                                        collation.CollationIndex = collationIndex++;
                                    }
                                }

                                long materialIndex = 0;
                                foreach (DtoPack.DtoMaterial material in result.Materials)
                                {
                                    material.PackId = s.PackId;
                                    material.StageLevel = stage.StageLevel;
                                    material.ResultIndex = result.ResultIndex;
                                    material.MaterialIndex = materialIndex++;

                                    int databaseMaterialIndex = 0;
                                    foreach (DtoDatabaseMaterial databaseMaterial in material.DatabaseMaterials)
                                    {
                                        databaseMaterial.PackId = s.PackId;
                                        databaseMaterial.StageLevel = stage.StageLevel;
                                        databaseMaterial.ResultIndex = result.ResultIndex;
                                        databaseMaterial.MaterialIndex = material.MaterialIndex;
                                        databaseMaterial.DatabaseMaterialIndex = databaseMaterialIndex++;
                                    }
                                }

                                long sectionIndex = 0;
                                foreach (DtoSection section in result.Sections)
                                {
                                    section.PackId = s.PackId;
                                    section.StageLevel = stage.StageLevel;
                                    section.ResultIndex = result.ResultIndex;
                                    section.SectionIndex = sectionIndex++;
                                }
                            }
                        }
                    });
            });

        /// <summary> Configuration of map from Dto to Model. </summary>
        private static MapperConfiguration configDtoToModel = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<DtoPack.DtoPack, Pack>();
                cfg.CreateMap<DtoCosting, Costing>();
                cfg.CreateMap<DtoStage, Stage>();
                cfg.CreateMap<DtoLimit, Limit>();
                cfg.CreateMap<DtoResult, Result>();
                cfg.CreateMap<DtoLayer, Layer>();
                cfg.CreateMap<DtoCollation, Collation>();
                cfg.CreateMap<DtoPack.DtoMaterial, Material>();
                cfg.CreateMap<DtoSection, Section>();
            });

        /// <summary> The mapper from Model to Dto. </summary>
        private static IMapper mapperModelToDto = configModelToDto.CreateMapper();

        /// <summary> The mapper from Dto to Model. </summary>
        private static IMapper mapperDtoToModel = configDtoToModel.CreateMapper();

        /// <summary> Converts a Pack to its DTO. </summary>
        /// 
        /// <param name="pack"> Pack to convert. </param>
        /// 
        /// <returns> The converted dto. </returns>
        public static DtoPack.DtoPack Convert(Pack pack)
        {
            DtoPack.DtoPack ret = mapperModelToDto.Map<DtoPack.DtoPack>(pack);

            return ret;
        }

        /// <summary> Converts a DTO to its Plan. </summary>
        ///
        /// <param name="pack"> Dto to convert. </param>
        ///
        /// <returns> The converted plan. </returns>
        public static Pack Convert(DtoPack.DtoPack pack)
        {
            Pack ret = mapperDtoToModel.Map<Pack>(pack);

            return ret;
        }
    }
}