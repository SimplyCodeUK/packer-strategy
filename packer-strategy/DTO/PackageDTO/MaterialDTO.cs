﻿//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO.PackageDTO
{
    using System.Collections.Generic;

    /// <summary>   A material. </summary>
    public class MaterialDTO : Models.Material.Material
    {
        /// <summary>   Default constructor. </summary>
        public MaterialDTO()
        {
            Layers = new List<LayerDTO>();
        }

        /// <summary>   Gets or sets the number of.  </summary>
        ///
        /// <value> The count. </value>
        public long Count { get; set;  }

        /// <summary>   Gets or sets the layers. </summary>
        ///
        /// <value> The layers. </value>
        public List<LayerDTO>  Layers { get; set; }
    }
}