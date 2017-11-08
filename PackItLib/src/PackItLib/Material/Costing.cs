// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Material
{
    using System.ComponentModel.DataAnnotations;

    /// <summary> A costing. </summary>
    public class Costing
    {
        /// <summary> Gets or sets the quantity. </summary>
        ///
        /// <value> The quantity. </value>
        [Display(Prompt = "Enter Quantity")]
        public long Quantity { get; set; }

        /// <summary> Gets or sets the quantity cost. </summary>
        ///
        /// <value> The quantity cost. </value>
        [Display(Name = "Quantity Cost", Prompt = "Enter Quantity Cost")]
        public double QuantityCost { get; set; }

        /// <summary> Gets or sets the volume cost. </summary>
        ///
        /// <value> The volume cost. </value>
        [Display(Name = "Volume Cost", Prompt = "Enter Volume Cost")]
        public double VolumeCost { get; set; }

        /// <summary> Gets or sets the area cost. </summary>
        ///
        /// <value> The area cost. </value>
        [Display(Name = "Area Cost", Prompt = "Enter Area Cost")]
        public double AreaCost { get; set; }

        /// <summary> Gets or sets the length cost. </summary>
        ///
        /// <value> The length cost. </value>
        [Display(Name = "Length Cost", Prompt = "Enter Length Cost")]
        public double LengthCost { get; set; }
    }
}
