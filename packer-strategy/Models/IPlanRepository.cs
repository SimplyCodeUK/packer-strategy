//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;

namespace packer_strategy.Models
{
    /*!
     * \interface   IPlanRepository
     *
     * \brief   Interface for plan repository.
     */
    public interface IPlanRepository
    {
        /*!
         * \fn  void Add(Plan.Plan item);
         *
         * \brief   Adds item.
         *
         * \param   item    The item to add.
         */
        void Add(Plan.Plan item);

        /*!
         * \fn  IEnumerable<Plan.Plan> GetAll();
         *
         * \brief   Gets all items in this collection.
         *
         * \return  An enumerator that allows foreach to be used to process all items in this collection.
         */
        IEnumerable<Plan.Plan> GetAll();

        /*!
         * \fn  Plan.Plan Find(string key);
         *
         * \brief   Searches for the first match for the given string.
         *
         * \param   key The key.
         *
         * \return  A Plan.Plan.
         */
        Plan.Plan Find(string key);

        /*!
         * \fn  void Remove(string key);
         *
         * \brief   Removes the given key.
         *
         * \param   key The key to remove.
         */
        void Remove(string key);

        /*!
         * \fn  void Update(Plan.Plan item);
         *
         * \brief   Updates the given item.
         *
         * \param   item    The item.
         */
        void Update(Plan.Plan item);
    }
}
