//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Package
{
    using System.Collections.Generic;

    /// <summary>   A material. </summary>
    public class Material : Models.Material.Material
    {
        /// <summary>   Default constructor. </summary>
        public Material()
        {
        }

        /// <summary>   Gets or sets the number of.  </summary>
        ///
        /// <value> The count. </value>
        public long Count { get; set;  }
    }
}
