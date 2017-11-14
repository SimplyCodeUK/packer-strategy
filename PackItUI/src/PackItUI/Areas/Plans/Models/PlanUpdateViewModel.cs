// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.Models
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    /// <summary> Plan home view model. </summary>
    public class PlanUpdateViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlanUpdateViewModel"/> class.
        /// </summary>
        public PlanUpdateViewModel()
        {
            this.Data = new Plan();
        }

        /// <summary> Gets or sets the plan data. </summary>
        ///
        /// <value> The plan data. </value>
        public Plan Data { get; set; }

        /// <summary>
        /// Data for plan view model
        /// </summary>
        public class Plan
        {
            /// <summary> Gets or sets the Plan identifier. </summary>
            ///
            /// <value> The Plan identifier. </value>
            [Required]
            [Display(Name = "ID", Prompt = "Enter Plan Id")]
            public string PlanId { get; set; }

            /// <summary> Gets or sets the name. </summary>
            ///
            /// <value> The name. </value>
            [Display(Prompt = "Enter Material Name")]
            public string Name { get; set; }
        }
    }
}
