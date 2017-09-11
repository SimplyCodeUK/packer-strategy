// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackIt.DTO.DtoPlan;
    using PackIt.Models.Plan;

    /// <summary>
    /// Maps from and to DtoPlan
    /// </summary>
    public static class PlanMapper
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static MapperConfiguration configModelToDto = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Plan, DtoPlan.DtoPlan>();
                cfg.CreateMap<Limit, DtoLimit>();
                cfg.CreateMap<Stage, DtoStage>();
                cfg.CreateMap<Plan, DtoPlan.DtoPlan>().AfterMap(
                    (s, d) =>
                    {
                        foreach (DtoStage stage in d.Stages)
                        {
                            stage.PlanId = s.PlanId;
                            foreach (DtoLimit limit in stage.Limits)
                            {
                                limit.PlanId = s.PlanId;
                                limit.StageLevel = stage.StageLevel;
                            }
                        }
                    });
            });

        /// <summary> Configuration of map from Dto to Model. </summary>
        private static MapperConfiguration configDtoToModel = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<DtoPlan.DtoPlan, Plan>();
                cfg.CreateMap<DtoStage, Stage>();
                cfg.CreateMap<DtoLimit, Limit>();
            });

        /// <summary> The mapper from Model to Dto. </summary>
        private static IMapper mapperModelToDto = configModelToDto.CreateMapper();

        /// <summary> The mapper from Dto to Model. </summary>
        private static IMapper mapperDtoToModel = configDtoToModel.CreateMapper();

        /// <summary> Converts a Plan to its DTO. </summary>
        /// 
        /// <param name="plan"> Plan to convert. </param>
        /// 
        /// <returns> The converted dto. </returns>
        public static DtoPlan.DtoPlan Convert(Plan plan)
        {
            DtoPlan.DtoPlan ret = mapperModelToDto.Map<DtoPlan.DtoPlan>(plan);

            return ret;
        }

        /// <summary> Converts a DTO to its Plan. </summary>
        ///
        /// <param name="plan"> Dto to convert. </param>
        ///
        /// <returns> The converted plan. </returns>
        public static Plan Convert(DtoPlan.DtoPlan plan)
        {
            Plan ret = mapperDtoToModel.Map<Plan>(plan);

            return ret;
        }
    }
}
