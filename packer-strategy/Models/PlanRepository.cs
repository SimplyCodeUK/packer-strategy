//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;
using System.Linq;

namespace packer_strategy.Models
{
    /*!
     * \class   PlanRepository
     *
     * \brief   A plan repository.
     */
    public class PlanRepository : IPlanRepository
    {
        /*! \brief   The context */
        private readonly PlanContext _context;

        /*!
         * \fn  public PlanRepository(PlanContext context)
         *
         * \brief   Constructor.
         *
         * \param   context The context.
         */
        public PlanRepository(PlanContext context)
        {
            _context = context;
            Add(new Plan.Plan { Name = "Item1" });
        }

        /*!
         * \fn  public IEnumerable<Plan.Plan> GetAll()
         *
         * \brief   Gets all items in this collection.
         *
         * \return  An enumerator that allows foreach to be used to process all items in this collection.
         */
        public IEnumerable<Plan.Plan> GetAll()
        {
            return _context.Strategies.ToList();
        }

        /*!
         * \fn  public void Add(Plan.Plan item)
         *
         * \brief   Adds item.
         *
         * \param   item    The item to add.
         */
        public void Add(Plan.Plan item)
        {
            _context.Strategies.Add(item);
            _context.SaveChanges();
        }

        /*!
         * \fn  public Plan.Plan Find(string key)
         *
         * \brief   Searches for the first match for the given string.
         *
         * \param   key The key.
         *
         * \return  A Plan.Plan.
         */
        public Plan.Plan Find(string key)
        {
            return _context.Strategies.FirstOrDefault(t => t.Id == key);
        }

        /*!
         * \fn  public void Remove(string key)
         *
         * \brief   Removes the given key.
         *
         * \param   key The key to remove.
         */
        public void Remove(string key)
        {
            var entity = _context.Strategies.First(t => t.Id == key);
            _context.Strategies.Remove(entity);
            _context.SaveChanges();
        }

        /*!
         * \fn  public void Update(Plan.Plan item)
         *
         * \brief   Updates the given item.
         *
         * \param   item    The item.
         */
        public void Update(Plan.Plan item)
        {
            _context.Strategies.Update(item);
            _context.SaveChanges();
        }
    }
}
