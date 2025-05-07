// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.DTO
{
    using AutoMapper;
    using PackItLib.Drawing;

    /// <summary> Maps from and to DtoPack </summary>
    ///
    /// <seealso cref="T:PackItLib.DTO.IPackItMapper{TData, TDtoData}"/>
    public class DrawingMapper : PackItLib.DTO.IPackItMapper<Drawing, PackItLib.DTO.DtoDrawing.DtoDrawing>
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static readonly MapperConfiguration configModelToDto = new(
            cfg =>
            {
                cfg.CreateMap<Drawing, PackItLib.DTO.DtoDrawing.DtoDrawing>().AfterMap(
                    (s, d) =>
                    {
                        d.Packs[0].PackId = s.DrawingId;
                        foreach (var costing in d.Packs[0].Costings)
                        {
                            costing.PackId = s.DrawingId;
                        }

                        long shapeIndex = 0;
                        foreach (var shape in d.Shapes)
                        {
                            shape.DrawingId = s.DrawingId;
                            shape.ShapeIndex = shapeIndex++;
                        }

                        foreach (var stage in d.Packs[0].Stages)
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
                cfg.CreateMap<PackItLib.Pack.Pack, PackItLib.DTO.DtoPack.DtoPack>();
                cfg.CreateMap<PackItLib.Pack.Costing, PackItLib.DTO.DtoPack.DtoCosting>();
                cfg.CreateMap<PackItLib.Pack.Stage, PackItLib.DTO.DtoPack.DtoStage>();
                cfg.CreateMap<PackItLib.Pack.Limit, PackItLib.DTO.DtoPack.DtoLimit>();
                cfg.CreateMap<PackItLib.Pack.Result, PackItLib.DTO.DtoPack.DtoResult>();
                cfg.CreateMap<PackItLib.Pack.Layer, PackItLib.DTO.DtoPack.DtoLayer>();
                cfg.CreateMap<PackItLib.Pack.Collation, PackItLib.DTO.DtoPack.DtoCollation>();
                cfg.CreateMap<PackItLib.Pack.Material, PackItLib.DTO.DtoPack.DtoMaterial>();
                cfg.CreateMap<PackItLib.Pack.DatabaseMaterial, PackItLib.DTO.DtoPack.DtoDatabaseMaterial>();
                cfg.CreateMap<PackItLib.Pack.Section, PackItLib.DTO.DtoPack.DtoSection>();
                cfg.CreateMap<Shape3D, PackItLib.DTO.DtoDrawing.DtoShape3D>();
            });

        /// <summary> Configuration of map from Dto to Model. </summary>
        private static readonly MapperConfiguration configDtoToModel = new(
            cfg =>
            {
                cfg.CreateMap<PackItLib.DTO.DtoDrawing.DtoDrawing, Drawing>();
                cfg.CreateMap<PackItLib.DTO.DtoDrawing.DtoShape3D, Shape3D>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoPack, PackItLib.Pack.Pack>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoCosting, PackItLib.Pack.Costing>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoStage, PackItLib.Pack.Stage>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoLimit, PackItLib.Pack.Limit>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoResult, PackItLib.Pack.Result>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoLayer, PackItLib.Pack.Layer>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoCollation, PackItLib.Pack.Collation>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoMaterial, PackItLib.Pack.Material>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoDatabaseMaterial, PackItLib.Pack.DatabaseMaterial>();
                cfg.CreateMap<PackItLib.DTO.DtoPack.DtoSection, PackItLib.Pack.Section>();
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
        public PackItLib.DTO.DtoDrawing.DtoDrawing ConvertToDto(Drawing data)
        {
            return mapperModelToDto.Map<PackItLib.DTO.DtoDrawing.DtoDrawing>(data);
        }

        /// <summary> Converts a DTO to its Data. </summary>
        ///
        /// <param name="dtoData"> DTO to convert. </param>
        ///
        /// <returns> The converted Data. </returns>
        public Drawing ConvertToData(PackItLib.DTO.DtoDrawing.DtoDrawing dtoData)
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
