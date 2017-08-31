// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoPackage
{
    using System.Collections.Generic;

    /// <summary>   A dto material. </summary>
    public class DtoMaterial
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DtoMaterial" /> class.
        /// </summary>
        public DtoMaterial()
        {
            this.Layers = new List<DtoLayer>();
        }

        /// <summary>   Gets or sets the number of.  </summary>
        ///
        /// <value> The count. </value>
        public long Count { get; set;  }

        /// <summary>   Gets or sets the layers. </summary>
        ///
        /// <value> The layers. </value>
        public List<DtoLayer> Layers { get; set; }
    }
}
