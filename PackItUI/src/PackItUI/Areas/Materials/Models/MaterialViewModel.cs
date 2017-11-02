// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Models
{
    /// <summary> Material home view model. </summary>
    public class MaterialViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialViewModel" /> class.
        /// </summary>
        public MaterialViewModel()
        {
            this.Data = new PackIt.Material.Material();
        }

        /// <summary> Gets or sets the material data. </summary>
        ///
        /// <value> The material data. </value>
        public PackIt.Material.Material Data { get; set; }
    }
}
