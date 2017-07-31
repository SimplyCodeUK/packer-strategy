//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;
using System.Linq;

namespace packer_strategy.Models
{
    /** A plan repository. */
    public class PlanRepository : IPlanRepository
    {
        private readonly PlanContext _context;  ///< The context

        /**
         * Constructor.
         *
         * @param   context The context.
         */
        public PlanRepository(PlanContext context)
        {
            _context = context;
            Add(new Plan.Plan { Name = "Item1" });
        }

        /**
         * Gets all items in this collection.
         *
         * @return  An enumerator that allows foreach to be used to process all items in this collection.
         */
        public IEnumerable<Plan.Plan> GetAll()
        {
            return _context.Plans.ToList();
        }

        /**
         * Adds item.
         *
         * @param   item    The item to add.
         */
        public void Add(Plan.Plan item)
        {
            _context.Plans.Add(item);
            _context.SaveChanges();
        }

        /**
         * Searches for the first match for the given string.
         *
         * @param   key The key.
         *
         * @return  A Plan.Plan.
         */
        public Plan.Plan Find(string key)
        {
            return _context.Plans.FirstOrDefault(t => t.Id == key);
        }

        /**
         * Removes the given key.
         *
         * @param   key The key to remove.
         */
        public void Remove(string key)
        {
            var entity = _context.Plans.First(t => t.Id == key);
            _context.Plans.Remove(entity);
            _context.SaveChanges();
        }

        /**
         * Updates the given item.
         *
         * @param   item    The item.
         */
        public void Update(Plan.Plan item)
        {
            _context.Plans.Update(item);
            _context.SaveChanges();
        }
    }
}
