// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using AutoMapper;
    using PackItLib.Plan;

    /// <summary> Maps from and to DtoPlan </summary>
    ///
    /// <seealso cref="T:PackItLib.DTO.IPackItMapper{TData, TDtoData}"/>
    public class PlanMapper : PackItLib.DTO.IPackItMapper<Plan, PackItLib.DTO.DtoPlan.DtoPlan>
    {
        /// <summary> Configuration of map from Model to Dto. </summary>
        private static readonly MapperConfiguration configModelToDto = new(
            cfg =>
            {
                cfg.CreateMap<Limit, PackItLib.DTO.DtoPlan.DtoLimit>();
                cfg.CreateMap<Stage, PackItLib.DTO.DtoPlan.DtoStage>();
                cfg.CreateMap<Plan, PackItLib.DTO.DtoPlan.DtoPlan>().AfterMap(
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
                cfg.CreateMap<PackItLib.DTO.DtoPlan.DtoPlan, Plan>();
                cfg.CreateMap<PackItLib.DTO.DtoPlan.DtoStage, Stage>();
                cfg.CreateMap<PackItLib.DTO.DtoPlan.DtoLimit, Limit>();
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
        public PackItLib.DTO.DtoPlan.DtoPlan ConvertToDto(Plan data)
        {
            return mapperModelToDto.Map<PackItLib.DTO.DtoPlan.DtoPlan>(data);
        }

        /// <summary> Converts a DTO to its Data. </summary>
        ///
        /// <param name="dtoData"> DTO to convert. </param>
        ///
        /// <returns> The converted Data. </returns>
        public Plan ConvertToData(PackItLib.DTO.DtoPlan.DtoPlan dtoData)
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
