// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Models.Plan
{
    using System.Collections.Generic;
    using PackIt.Helpers.Enums;

    /// <summary>   A plan. </summary>
    public class Plan
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Plan" /> class.
        /// </summary>
        public Plan()
        {
            this.Stages = new List<Stage>();
        }

        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        public string Id { get; set; }

        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>   Gets or sets the notes. </summary>
        ///
        /// <value> The notes. </value>
        public string Notes { get; set; }

        /// <summary>   Gets or sets the stages. </summary>
        ///
        /// <value> The stages. </value>
        public List<Stage> Stages { get; set; }
    }
}
