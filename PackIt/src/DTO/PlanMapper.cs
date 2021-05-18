// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackIt.Plan;

    /// <summary> Maps from and to DtoPlan </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.IPackItMapper{TData, TDtoData}"/>
    public class PlanMapper : IPackItMapper<Plan, DtoPlan.DtoPlan>
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static readonly MapperConfiguration configModelToDto = new(
            cfg =>
            {
                cfg.CreateMap<Limit, DtoPlan.DtoLimit>();
                cfg.CreateMap<Stage, DtoPlan.DtoStage>();
                cfg.CreateMap<Plan, DtoPlan.DtoPlan>().AfterMap(
                    (s, d) =>
                    {
                        foreach (var stage in d.Stages)
                        {
                            stage.PlanId = s.PlanId;

                            int limitIndex = 0;
                            foreach (var limit in stage.Limits)
                            {
                                limit.PlanId = s.PlanId;
                                limit.StageLevel = stage.StageLevel;
                                limit.LimitIndex = limitIndex++;
                            }
                        }
                    });
            });

        /// <summary> Configuration of map from Dto to Model. </summary>
        private static readonly MapperConfiguration configDtoToModel = new(
            cfg =>
            {
                cfg.CreateMap<DtoPlan.DtoPlan, Plan>();
                cfg.CreateMap<DtoPlan.DtoStage, Stage>();
                cfg.CreateMap<DtoPlan.DtoLimit, Limit>();
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
        public DtoPlan.DtoPlan ConvertToDto(Plan data)
        {
            return mapperModelToDto.Map<DtoPlan.DtoPlan>(data);
        }

        /// <summary> Converts a DTO to its Data. </summary>
        ///
        /// <param name="dtoData"> DTO to convert. </param>
        ///
        /// <returns> The converted Data. </returns>
        public Plan ConvertToData(DtoPlan.DtoPlan dtoData)
        {
            return mapperDtoToModel.Map<Plan>(dtoData);
        }

        /// <summary> Get Data key. </summary>
        ///
        /// <param name="data"> Data to get the key for. </param>
        ///
        /// <returns> The key. </returns>
        public string KeyForData(Plan data)
        {
            return data.PlanId;
        }
    }
}
