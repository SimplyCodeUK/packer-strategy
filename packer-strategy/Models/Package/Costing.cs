//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Package
{
    /// <summary>   A costing. </summary>
    public class Costing
    {
        /// <summary>   Gets or sets the identifier of the package. </summary>
        ///
        /// <value> The identifier of the package. </value>
        public string PackageId { get; set; }

        /// <summary>   Gets or sets the zero-based index of this object. </summary>
        ///
        /// <value> The index. </value>
        public long Index { get; set; }

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
