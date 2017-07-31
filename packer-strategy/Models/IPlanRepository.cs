//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;

namespace packer_strategy.Models
{
    /** Interface for plan repository. */
    public interface IPlanRepository
    {
        /**
         * Adds item.
         *
         * @param   item    The item to add.
         */
        void Add(Plan.Plan item);

        /**
         * Gets all items in this collection.
         *
         * @return  An enumerator that allows foreach to be used to process all items in this collection.
         */
        IEnumerable<Plan.Plan> GetAll();

        /**
         * Searches for the first match for the given string.
         *
         * @param   key The key.
         *
         * @return  A Plan.Plan.
         */
        Plan.Plan Find(string key);

        /**
         * Removes the given key.
         *
         * @param   key The key to remove.
         */
        void Remove(string key);

        /**
         * Updates the given item.
         *
         * @param   item    The item.
         */
        void Update(Plan.Plan item);
    }
}
