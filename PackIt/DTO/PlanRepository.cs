// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using PackIt.Models.Plan;

    /// <summary>   A plan repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.IPlanRepository"/>
    public class PlanRepository : IPlanRepository
    {
        /// <summary>   The context. </summary>
        private readonly PlanContext context;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlanRepository" /> class.
        /// </summary>
        ///
        /// <param name="context">  The context. </param>
        public PlanRepository(PlanContext context)
        {
            this.context = context;
        }

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        ///
        /// <seealso cref="M:PackIt.DTO.IPlanRepository.GetAll()"/>
        public IEnumerable<Plan> GetAll()
        {
            return this.context.GetPlans();
        }

        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IPlanRepository.Add(Plan)"/>
        public void Add(Plan item)
        {
            this.context.AddPlan(item);
            this.context.SaveChanges();
        }

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        ///
        /// <returns>   A Plan. </returns>
        ///
        /// <seealso cref="M:PackIt.DTO.IPlanRepository.Find(string)"/>
        public Plan Find(string key)
        {
            Plan ret = this.context.FindPlan(key);
            return ret;
        }

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IPlanRepository.Remove(string)"/>
        public void Remove(string key)
        {
            this.context.RemovePlan(key);
            this.context.SaveChanges();
        }

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item. </param>
        ///
        /// <seealso cref="M:PackIt.DTO.IPlanRepository.Update(Plan)"/>
        public void Update(Plan item)
        {
            this.context.UpdatePlan(item);
            this.context.SaveChanges();
        }
    }
}
