// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Plan
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary> A plan. </summary>
    public class Plan
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Plan" /> class.
        /// </summary>
        public Plan()
        {
            this.Stages = new List<Stage>();
        }

        /// <summary> Gets or sets the Plan identifier. </summary>
        ///
        /// <value> The Plan identifier. </value>
        [Required]
        [Display(Name = "ID", Prompt = "Enter Plan Id")]
        public string PlanId { get; set; }

        /// <summary> Gets or sets the name. </summary>
        ///
        /// <value> The plan name. </value>
        [Display(Prompt = "Enter Plan Name")]
        public string Name { get; set; }

        /// <summary> Gets or sets the collection of stages. </summary>
        ///
        /// <value> Collection of stages. </value>
        public IList<Stage> Stages { get; set; }
    }
}
