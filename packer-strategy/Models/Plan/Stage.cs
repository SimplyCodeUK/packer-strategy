//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Plan
{
    /*! A stage. */
    public class Stage
    {
        /*!
         * Gets or sets the identifier of the strategy.
         *
         * @return  The identifier of the strategy.
         */
        public string StrategyId { get; set; }

        /*!
         * Gets or sets the level.
         *
         * @return  The level.
         */
        public int Level { get; set; }

        /*!
         * Gets or sets the collation.
         *
         * @return  The collation.
         */
        public int Collation { get; set; }
    }
}
