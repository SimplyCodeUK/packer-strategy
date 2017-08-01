//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Plan
{
    /// <summary>
    ///     A stage.
    /// </summary>
    public class Stage
    {
        /// <summary>
        ///     Gets or sets the identifier of the strategy.
        /// </summary>
        ///
        /// <value>
        ///     The identifier of the strategy.
        /// </value>
        public string StrategyId { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        ///
        /// <value>
        ///     The level.
        /// </value>
        public int Level { get; set; }

        /// <summary>
        ///     Gets or sets the collation.
        /// </summary>
        ///
        /// <value>
        ///     The collation.
        /// </value>
        public int Collation { get; set; }
    }
}
