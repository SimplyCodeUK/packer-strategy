// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackItLib.Pack;

    /// <summary> Maps from and to DtoPack </summary>
    ///
    /// <seealso cref="T:PackItLib.DTO.IPackItMapper{TData, TDtoData}"/>
    public class PackMapper : PackItLib.DTO.IPackItMapper<Pack, PackItLib.DTO.DtoPack.DtoPack>
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static readonly MapperConfiguration configModelToDto = new(
            cfg =>
            {
                cfg.CreateMap<Costing, PackItLib.DTO.DtoPack.DtoCosting>();
                cfg.CreateMap<Stage, PackItLib.DTO.DtoPack.DtoStage>();
                cfg.CreateMap<Limit, PackItLib.DTO.DtoPack.DtoLimit>();
                cfg.CreateMap<Result, PackItLib.DTO.DtoPack.DtoResult>();
                cfg.CreateMap<Layer, PackItLib.DTO.DtoPack.DtoLayer>();
                cfg.CreateMap<Collation, PackItLib.DTO.DtoPack.DtoCollation>();
                cfg.CreateMap<Material, PackItLib.DTO.DtoPack.DtoMaterial>();
                cfg.CreateMap<DatabaseMaterial, PackItLib.DTO.DtoPack.DtoDatabaseMaterial>();
                cfg.CreateMap<Section, PackItLib.DTO.DtoPack.DtoSection>();
                cfg.CreateMap<Pack, PackItLib.DTO.DtoPack.DtoPack>().AfterMap(
                    (s, d) =>
                    {
                        foreach (var costing in d.Costings)
                        {
                            costing.PackId = s.PackId;
                        }

                        foreach (var stage in d.Stages)
                        {
                            stage.PackId = s.PackId;

                            long limitIndex = 0;
                            foreach (var limit in stage.Limits)
                            {
                                limit.PackId = s.PackId;
                                limit.StageLevel = stage.StageLevel;
                                limit.LimitIndex = limitIndex++;
                            }

                            long resultIndex = 0;
                            foreach (var result in stage.Results)
                            {
                                result.PackId = s.PackId;
                                result.StageLevel = stage.StageLevel;
                                result.ResultIndex = resultIndex++;

                                int layerIndex = 0;
                                foreach (var layer in result.Layers)
                                {
                                    layer.PackId = s.PackId;
                                    layer.StageLevel = stage.StageLevel;
                                    layer.ResultIndex = result.ResultIndex;
                                    layer.LayerIndex = layerIndex++;

                                    int collationIndex = 0;
                                    foreach (var collation in layer.Collations)
                                    {
                                        collation.PackId = s.PackId;
                                        collation.StageLevel = stage.StageLevel;
                                        collation.ResultIndex = result.ResultIndex;
                                        collation.LayerIndex = layer.LayerIndex;
                                        collation.CollationIndex = collationIndex++;
                                    }
                                }

                                long materialIndex = 0;
                                foreach (var material in result.Materials)
                                {
                                    material.PackId = s.PackId;
                                    material.StageLevel = stage.StageLevel;
                                    material.ResultIndex = result.ResultIndex;
                                    material.MaterialIndex = materialIndex++;

                                    int databaseMaterialIndex = 0;
                                    foreach (var databaseMaterial in material.DatabaseMaterials)
                                    {
                                        databaseMaterial.PackId = s.PackId;
                                        databaseMaterial.StageLevel = stage.StageLevel;
                                        databaseMaterial.ResultIndex = result.ResultIndex;
                                        databaseMaterial.MaterialIndex = material.MaterialIndex;
                                        databaseMaterial.DatabaseMaterialIndex = databaseMaterialIndex++;
                                    }
                                }

                                long sectionIndex = 0;
                                foreach (var section in result.Sections)
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
        private static readonly MapperConfiguration configDtoToModel = new(
            cfg =>
            {
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoPack, Pack>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoCosting, Costing>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoStage, Stage>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoLimit, Limit>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoResult, Result>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoLayer, Layer>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoCollation, Collation>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoMaterial, Material>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoDatabaseMaterial, DatabaseMaterial>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoSection, Section>();
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
        public PackItLib.DTO.DtoPack.DtoPack ConvertToDto(Pack data)
        {
            return mapperModelToDto.Map<PackItLib.DTO.DtoPack.DtoPack>(data);
        }

        /// <summary> Converts a DTO to its Data. </summary>
        ///
        /// <param name="dtoData"> DTO to convert. </param>
        ///
        /// <returns> The converted Data. </returns>
        public Pack ConvertToData(PackItLib.DTO.DtoPack.DtoPack dtoData)
        {
            return mapperDtoToModel.Map<Pack>(dtoData);
        }

        /// <summary> Get Data key. </summary>
        ///
        /// <param name="data"> Data to get the key for. </param>
        ///
        /// <returns> The key. </returns>
        public string KeyForData(Pack data)
        {
            return data.PackId;
        }
    }
}
