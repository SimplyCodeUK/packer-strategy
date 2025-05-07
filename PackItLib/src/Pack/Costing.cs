// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.Pack
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary> A costing. </summary>
    [ExcludeFromCodeCoverage]
    public class Costing
    {
        /// <summary> Gets or sets the required quantity. </summary>
        ///
        /// <value> The required quantity. </value>
        [Display(Name = "Required Quantity", Prompt = "Enter Required Quantity")]
        public long RequiredQuantity { get; set; }

        /// <summary> Gets or sets the required weight. </summary>
        ///
        /// <value> The required weight. </value>
        [Display(Name = "Required Weight", Prompt = "Enter Required Weight")]
        public double RequiredWeight { get; set; }
    }
}
