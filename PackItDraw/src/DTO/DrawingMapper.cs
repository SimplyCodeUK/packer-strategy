// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackIt.Drawing;

    /// <summary> Maps from and to DtoPack </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.IPackItMapper{TData, TDtoData}"/>
    public class DrawingMapper : IPackItMapper<Drawing, DtoDrawing.DtoDrawing>
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static readonly MapperConfiguration configModelToDto = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Pack.Pack, DtoPack.DtoPack>();
                cfg.CreateMap<Pack.Costing, DtoPack.DtoCosting>();
                cfg.CreateMap<Pack.Stage, DtoPack.DtoStage>();
                cfg.CreateMap<Pack.Limit, DtoPack.DtoLimit>();
                cfg.CreateMap<Pack.Result, DtoPack.DtoResult>();
                cfg.CreateMap<Pack.Layer, DtoPack.DtoLayer>();
                cfg.CreateMap<Pack.Collation, DtoPack.DtoCollation>();
                cfg.CreateMap<Pack.Material, DtoPack.DtoMaterial>();
                cfg.CreateMap<Pack.DatabaseMaterial, DtoPack.DtoDatabaseMaterial>();
                cfg.CreateMap<Pack.Section, DtoPack.DtoSection>();
                cfg.CreateMap<Drawing, DtoDrawing.DtoDrawing>().AfterMap(
                    (s, d) =>
                    {
                        d.Pack.PackId = s.DrawingId;
                        foreach (var costing in d.Pack.Costings)
                        {
                            costing.PackId = s.DrawingId;
                        }

                        foreach (var stage in d.Pack.Stages)
                        {
                            stage.PackId = s.DrawingId;

                            long limitIndex = 0;
                            foreach (var limit in stage.Limits)
                            {
                                limit.PackId = s.DrawingId;
                                limit.StageLevel = stage.StageLevel;
                                limit.LimitIndex = limitIndex++;
                            }

                            long resultIndex = 0;
                            foreach (var result in stage.Results)
                            {
                                result.PackId = s.DrawingId;
                                result.StageLevel = stage.StageLevel;
                                result.ResultIndex = resultIndex++;

                                int layerIndex = 0;
                                foreach (var layer in result.Layers)
                                {
                                    layer.PackId = s.DrawingId;
                                    layer.StageLevel = stage.StageLevel;
                                    layer.ResultIndex = result.ResultIndex;
                                    layer.LayerIndex = layerIndex++;

                                    int collationIndex = 0;
                                    foreach (var collation in layer.Collations)
                                    {
                                        collation.PackId = s.DrawingId;
                                        collation.StageLevel = stage.StageLevel;
                                        collation.ResultIndex = result.ResultIndex;
                                        collation.LayerIndex = layer.LayerIndex;
                                        collation.CollationIndex = collationIndex++;
                                    }
                                }

                                long materialIndex = 0;
                                foreach (var material in result.Materials)
                                {
                                    material.PackId = s.DrawingId;
                                    material.StageLevel = stage.StageLevel;
                                    material.ResultIndex = result.ResultIndex;
                                    material.MaterialIndex = materialIndex++;

                                    int databaseMaterialIndex = 0;
                                    foreach (var databaseMaterial in material.DatabaseMaterials)
                                    {
                                        databaseMaterial.PackId = s.DrawingId;
                                        databaseMaterial.StageLevel = stage.StageLevel;
                                        databaseMaterial.ResultIndex = result.ResultIndex;
                                        databaseMaterial.MaterialIndex = material.MaterialIndex;
                                        databaseMaterial.DatabaseMaterialIndex = databaseMaterialIndex++;
                                    }
                                }

                                long sectionIndex = 0;
                                foreach (var section in result.Sections)
                                {
                                    section.PackId = s.DrawingId;
                                    section.StageLevel = stage.StageLevel;
                                    section.ResultIndex = result.ResultIndex;
                                    section.SectionIndex = sectionIndex++;
                                }
                            }
                        }
                    });
            });

        /// <summary> Configuration of map from Dto to Model. </summary>
        private static readonly MapperConfiguration configDtoToModel = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<DtoDrawing.DtoDrawing, Drawing>();
                cfg.CreateMap<DtoPack.DtoPack, Pack.Pack>();
                cfg.CreateMap<DtoPack.DtoCosting, Pack.Costing>();
                cfg.CreateMap<DtoPack.DtoStage, Pack.Stage>();
                cfg.CreateMap<DtoPack.DtoLimit, Pack.Limit>();
                cfg.CreateMap<DtoPack.DtoResult, Pack.Result>();
                cfg.CreateMap<DtoPack.DtoLayer, Pack.Layer>();
                cfg.CreateMap<DtoPack.DtoCollation, Pack.Collation>();
                cfg.CreateMap<DtoPack.DtoMaterial, Pack.Material>();
                cfg.CreateMap<DtoPack.DtoSection, Pack.Section>();
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
        public DtoDrawing.DtoDrawing ConvertToDto(Drawing data)
        {
            return mapperModelToDto.Map<DtoDrawing.DtoDrawing>(data);
        }

        /// <summary> Converts a DTO to its Data. </summary>
        ///
        /// <param name="dtoData"> DTO to convert. </param>
        ///
        /// <returns> The converted Data. </returns>
        public Drawing ConvertToData(DtoDrawing.DtoDrawing dtoData)
        {
            return mapperDtoToModel.Map<Drawing>(dtoData);
        }

        /// <summary> Get Data key. </summary>
        ///
        /// <param name="data"> Data to get the key for. </param>
        ///
        /// <returns> The key. </returns>
        public string KeyForData(Drawing data)
        {
            return data.DrawingId;
        }
    }
}
