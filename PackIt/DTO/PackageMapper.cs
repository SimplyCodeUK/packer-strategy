// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackIt.DTO.DtoPackage;
    using PackIt.Models.Package;

    /// <summary>
    /// Maps from and to DtoPackage
    /// </summary>
    public static class PackageMapper
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static MapperConfiguration configModelToDto = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Package, DtoPackage.DtoPackage>();
                cfg.CreateMap<Costing, DtoCosting>();
                cfg.CreateMap<Stage, DtoStage>();
                cfg.CreateMap<Limit, DtoLimit>();
                cfg.CreateMap<Result, DtoResult>();
                cfg.CreateMap<Layer, DtoLayer>();
                cfg.CreateMap<Collation, DtoCollation>();
                cfg.CreateMap<Material, DtoPackage.DtoMaterial>();
                cfg.CreateMap<DatabaseMaterial, DtoDatabaseMaterial>();
                cfg.CreateMap<Section, DtoSection>();
                cfg.CreateMap<Package, DtoPackage.DtoPackage>().AfterMap(
                    (s, d) =>
                    {
                        foreach (DtoCosting costing in d.Costings)
                        {
                            costing.PackageId = s.Id;
                        }

                        foreach (DtoStage stage in d.Stages)
                        {
                            stage.PackageId = s.Id;

                            long limitIndex = 0;
                            foreach (DtoLimit limit in stage.Limits)
                            {
                                limit.PackageId = s.Id;
                                limit.StageLevel = stage.Level;
                                limit.Index = limitIndex++;
                            }

                            long resultIndex = 0;
                            foreach (DtoResult result in stage.Results)
                            {
                                result.PackageId = s.Id;
                                result.StageLevel = stage.Level;
                                result.Index = resultIndex++;

                                int layerIndex = 0;
                                foreach (DtoLayer layer in result.Layers)
                                {
                                    layer.PackageId = s.Id;
                                    layer.StageLevel = stage.Level;
                                    layer.ResultIndex = result.Index;
                                    layer.Index = layerIndex++;

                                    int collationIndex = 0;
                                    foreach (DtoCollation collation in layer.Collations)
                                    {
                                        collation.PackageId = s.Id;
                                        collation.StageLevel = stage.Level;
                                        collation.ResultIndex = result.Index;
                                        collation.LayerIndex = layer.Index;
                                        collation.Index = collationIndex++;
                                    }
                                }

                                long materialIndex = 0;
                                foreach (DtoPackage.DtoMaterial material in result.Materials)
                                {
                                    material.PackageId = s.Id;
                                    material.StageLevel = stage.Level;
                                    material.ResultIndex = result.Index;
                                    material.Index = materialIndex++;

                                    int databaseMaterialIndex = 0;
                                    foreach (DtoDatabaseMaterial databaseMaterial in material.DatabaseMaterials)
                                    {
                                        databaseMaterial.PackageId = s.Id;
                                        databaseMaterial.StageLevel = stage.Level;
                                        databaseMaterial.ResultIndex = result.Index;
                                        databaseMaterial.MaterialIndex = material.Index;
                                        databaseMaterial.Index = databaseMaterialIndex++;
                                    }
                                }

                                long sectionIndex = 0;
                                foreach (DtoSection section in result.Sections)
                                {
                                    section.PackageId = s.Id;
                                    section.StageLevel = stage.Level;
                                    section.ResultIndex = result.Index;
                                    section.Index = sectionIndex++;
                                }
                            }
                        }
                    });
            });

        /// <summary> Configuration of map from Dto to Model. </summary>
        private static MapperConfiguration configDtoToModel = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<DtoPackage.DtoPackage, Package>();
                cfg.CreateMap<DtoCosting, Costing>();
                cfg.CreateMap<DtoStage, Stage>();
                cfg.CreateMap<DtoLimit, Limit>();
                cfg.CreateMap<DtoResult, Result>();
                cfg.CreateMap<DtoLayer, Layer>();
                cfg.CreateMap<DtoCollation, Collation>();
                cfg.CreateMap<DtoPackage.DtoMaterial, Material>();
                cfg.CreateMap<DtoSection, Section>();
            });

        /// <summary> The mapper from Model to Dto. </summary>
        private static IMapper mapperModelToDto = configModelToDto.CreateMapper();

        /// <summary> The mapper from Dto to Model. </summary>
        private static IMapper mapperDtoToModel = configDtoToModel.CreateMapper();

        /// <summary> Converts a Plan to its DTO. </summary>
        /// 
        /// <param name="package"> Plan to convert. </param>
        /// 
        /// <returns> The converted dto. </returns>
        public static DtoPackage.DtoPackage Convert(Package package)
        {
            DtoPackage.DtoPackage ret = mapperModelToDto.Map<DtoPackage.DtoPackage>(package);

            foreach (DtoCosting costing in ret.Costings)
            {
                costing.PackageId = ret.Id;
            }

            foreach (DtoStage stage in ret.Stages)
            {
                stage.PackageId = ret.Id;

                long limitIndex = 0;
                foreach (DtoLimit limit in stage.Limits)
                {
                    limit.PackageId = ret.Id;
                    limit.StageLevel = stage.Level;
                    limit.Index = limitIndex;
                    ++limitIndex;
                }

                long resultIndex = 0;
                foreach (DtoResult result in stage.Results)
                {
                    result.PackageId = ret.Id;
                    result.StageLevel = stage.Level;
                    result.Index = resultIndex;

                    int layerIndex = 0;
                    foreach (DtoLayer layer in result.Layers)
                    {
                        layer.PackageId = ret.Id;
                        layer.StageLevel = stage.Level;
                        layer.ResultIndex = result.Index;
                        layer.Index = layerIndex;

                        int collationIndex = 0;
                        foreach (DtoCollation collation in layer.Collations)
                        {
                            collation.PackageId = ret.Id;
                            collation.StageLevel = stage.Level;
                            collation.ResultIndex = result.Index;
                            collation.LayerIndex = layer.Index;
                            collation.Index = collationIndex;
                            ++collationIndex;
                        }

                        ++layerIndex;
                    }

                    long materialIndex = 0;
                    foreach (DtoPackage.DtoMaterial material in result.Materials)
                    {
                        material.PackageId = ret.Id;
                        material.StageLevel = stage.Level;
                        material.ResultIndex = result.Index;
                        material.Index = materialIndex;

                        int databaseMaterialIndex = 0;
                        foreach (DtoDatabaseMaterial databaseMaterial in material.DatabaseMaterials)
                        {
                            databaseMaterial.PackageId = ret.Id;
                            databaseMaterial.StageLevel = stage.Level;
                            databaseMaterial.ResultIndex = result.Index;
                            databaseMaterial.MaterialIndex = material.Index;
                            databaseMaterial.Index = databaseMaterialIndex;
                            ++databaseMaterialIndex;
                        }

                        ++materialIndex;
                    }

                    long sectionIndex = 0;
                    foreach (DtoSection section in result.Sections)
                    {
                        section.PackageId = ret.Id;
                        section.StageLevel = stage.Level;
                        section.ResultIndex = result.Index;
                        section.Index = sectionIndex;
                        ++sectionIndex;
                    }

                    ++resultIndex;
                }
            }

            return ret;
        }

        /// <summary> Converts a DTO to its Plan. </summary>
        ///
        /// <param name="package"> Dto to convert. </param>
        ///
        /// <returns> The converted plan. </returns>
        public static Package Convert(DtoPackage.DtoPackage package)
        {
            Package ret = mapperDtoToModel.Map<Package>(package);

            return ret;
        }
    }
}
