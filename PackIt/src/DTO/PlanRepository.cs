// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using PackItLib.Plan;

    /// <summary> A plan repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.Repository{TData, TDtoData, TMapper}"/>
    /// <seealso cref="T:PackIt.DTO.IPlanRepository"/>
    /// <remarks>
    /// Initialises a new instance of the <see cref="PlanRepository" /> class.
    /// </remarks>
    ///
    /// <param name="context"> The context. </param>
    public class PlanRepository(PlanContext context) : PackItLib.DTO.Repository<Plan, PackItLib.DTO.DtoPlan.DtoPlan, PlanMapper>(context), IPlanRepository
    {
    }
}
