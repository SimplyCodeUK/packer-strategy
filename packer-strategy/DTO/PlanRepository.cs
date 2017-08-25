//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Plan;

    /// <summary>   A plan repository. </summary>
    ///
    /// <seealso cref="T:packer_strategy.DTO.IPlanRepository"/>
    public class PlanRepository : IPlanRepository
    {
        /// <summary>   The context. </summary>
        private readonly PlanContext _context;

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="context">  The context. </param>
        public PlanRepository(PlanContext context)
        {
            _context = context;
        }

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPlanRepository.GetAll()"/>
        public IEnumerable<Plan> GetAll()
        {
            return _context.GetPlans();
        }

        /// <summary>   Adds item. </summary>
        ///
        /// <param name="item"> The item. </param>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPlanRepository.Add(Plan)"/>
        public void Add(Plan item)
        {
            _context.AddPlan(item);
            _context.SaveChanges();
        }

        /// <summary>   Searches for the first match for the given string. </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        ///
        /// <returns>   A Plan. </returns>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPlanRepository.Find(string)"/>
        public Plan Find(string key)
        {
            return _context.FindPlan(key);
        }

        /// <summary>   Removes the given key. </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPlanRepository.Remove(string)"/>
        public void Remove(string key)
        {
            _context.RemovePlan(key);
            _context.SaveChanges();
        }

        /// <summary>   Updates the given item. </summary>
        ///
        /// <param name="item"> The item. </param>
        ///
        /// <seealso cref="M:packer_strategy.DTO.IPlanRepository.Update(Plan)"/>
        public void Update(Plan item)
        {
            _context.UpdatePlan(item);
            _context.SaveChanges();
        }
    }
}
