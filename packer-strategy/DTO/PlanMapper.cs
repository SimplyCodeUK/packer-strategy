// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackIt.Models.Plan;

    /// <summary>
    /// Maps from and to DtoPlan
    /// </summary>
    public static class PlanMapper
    {
        /// <summary> Configuration of map from plan to dto. </summary>
        private static MapperConfiguration configPlanToDto = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Plan, DtoPlan.DtoPlan>();
                cfg.CreateMap<Limit, DtoPlan.DtoLimit>();
                cfg.CreateMap<Stage, DtoPlan.DtoStage>();
                cfg.CreateMap<Plan, DtoPlan.DtoPlan>().AfterMap(
                    (s, d) =>
                    {
                        foreach (DtoPlan.DtoStage stage in d.Stages)
                        {
                            stage.PlanId = s.Id;
                            foreach (DtoPlan.DtoLimit limit in stage.Limits)
                            {
                                limit.PlanId = s.Id;
                                limit.StageLevel = stage.Level;
                            }
                        }
                    });
            });

        /// <summary> Configuration of map from dto to plan. </summary>
        private static MapperConfiguration configDtoToPlan = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<DtoPlan.DtoPlan, Plan>();
                cfg.CreateMap<DtoPlan.DtoStage, Stage>();
                cfg.CreateMap<DtoPlan.DtoLimit, Limit>();
            });

        /// <summary> The mapper from plan to dto.</summary>
        private static IMapper mapperPlanToDto = configPlanToDto.CreateMapper();

        /// <summary> The mapper from dto to plan.</summary>
        private static IMapper mapperDtoToPlan = configDtoToPlan.CreateMapper();

        /// <summary> Converts a Plan to its DTO. </summary>
        /// 
        /// <param name="plan">Plan to convert</param>
        /// 
        /// <returns> The converted dto. </returns>
        public static DtoPlan.DtoPlan Convert(Plan plan)
        {
            DtoPlan.DtoPlan ret = mapperPlanToDto.Map<DtoPlan.DtoPlan>(plan);

            return ret;
        }

        /// <summary> Converts a DTO to its Plan. </summary>
        ///
        /// <param name="plan">Dto to convert</param>
        ///
        /// <returns> The converted plan. </returns>
        public static Plan Convert(DtoPlan.DtoPlan plan)
        {
            Plan ret = mapperDtoToPlan.Map<Plan>(plan);

            return ret;
        }
    }
}
