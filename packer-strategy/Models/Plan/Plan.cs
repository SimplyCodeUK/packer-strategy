//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;

namespace packer_strategy.Models.Plan
{
    /// <summary>
    ///     A plan.
    /// </summary>
    public class Plan
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        ///
        /// <value>
        ///     The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        ///
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the notes.
        /// </summary>
        ///
        /// <value>
        ///     The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        ///     Gets or sets the Date/Time of the time.
        /// </summary>
        ///
        /// <value>
        ///     The time.
        /// </value>
        public DateTime Time { get; set; }

        /// <summary>
        ///     Gets or sets the stages.
        /// </summary>
        ///
        /// <value>
        ///     The stages.
        /// </value>
        public List<Stage> Stages { get; set; }
    }
}
