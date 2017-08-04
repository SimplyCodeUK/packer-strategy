//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;
using System.Linq;

namespace packer_strategy.Models
{
    /// <summary>
    ///     A plan repository.
    /// </summary>
    public class PlanRepository : IPlanRepository
    {
        /// <summary>
        ///     The context.
        /// </summary>
        private readonly PlanContext _context;

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="context">  The context. </param>
        public PlanRepository(PlanContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Gets all items in this collection.
        /// </summary>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        ///
        /// <seealso cref="M:packer_strategy.Models.IPlanRepository.GetAll()"/>
        public IEnumerable<Plan.Plan> GetAll()
        {
            return _context.Plans.ToList();
        }

        /// <summary>
        ///     Adds item.
        /// </summary>
        ///
        /// <param name="item"> The item. </param>
        ///
        /// <seealso cref="M:packer_strategy.Models.IPlanRepository.Add(Plan.Plan)"/>
        public void Add(Plan.Plan item)
        {
            _context.Plans.Add(item);
            _context.SaveChanges();
        }

        /// <summary>
        ///     Searches for the first match for the given string.
        /// </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        ///
        /// <returns>
        ///     A Plan.Plan.
        /// </returns>
        ///
        /// <seealso cref="M:packer_strategy.Models.IPlanRepository.Find(string)"/>
        public Plan.Plan Find(string key)
        {
            return _context.Plans.FirstOrDefault(t => t.Id == key);
        }

        /// <summary>
        ///     Removes the given key.
        /// </summary>
        ///
        /// <param name="key">  The key to remove. </param>
        ///
        /// <seealso cref="M:packer_strategy.Models.IPlanRepository.Remove(string)"/>
        public void Remove(string key)
        {
            var entity = _context.Plans.First(t => t.Id == key);
            _context.Plans.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        ///     Updates the given item.
        /// </summary>
        ///
        /// <param name="item"> The item. </param>
        ///
        /// <seealso cref="M:packer_strategy.Models.IPlanRepository.Update(Plan.Plan)"/>
        public void Update(Plan.Plan item)
        {
            _context.Plans.Update(item);
            _context.SaveChanges();
        }
    }
}
