// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Models.Package
{
    /// <summary>   A costing. </summary>
    public class Costing
    {
        /// <summary>   Gets or sets the identifier that owns this item. </summary>
        ///
        /// <value> The identifier of the owner. </value>
        public string OwnerId { get; set; }

        /// <summary>   Gets or sets the required quantity. </summary>
        ///
        /// <value> The required quantity. </value>
        public long RequiredQuantity { get; set; }

        /// <summary>   Gets or sets the required weight. </summary>
        ///
        /// <value> The required weight. </value>
        public double RequiredWeight { get; set; }
    }
}
