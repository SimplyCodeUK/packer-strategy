// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using PackIt.Plan;

    /// <summary> A plan repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.Repository{TData, TDtoData}"/>
    /// <seealso cref="T:PackIt.DTO.IPlanRepository"/>
    public class PlanRepository : Repository<Plan, DtoPlan.DtoPlan, PlanMapper>, IPlanRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlanRepository" /> class.
        /// </summary>
        ///
        /// <param name="context"> The context. </param>
        public PlanRepository(PlanContext context) : base(context)
        {
        }
    }
}
