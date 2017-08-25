//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO.DtoMaterial
{
    using Helpers.Enums;

    /// <summary>   A dto costing. </summary>
    public class DtoCosting
    {
        /// <summary>   Gets or sets the type of the material identifier. </summary>
        ///
        /// <value> The type of the material identifier. </value>
        public MaterialType MaterialIdType { get; set; }

        /// <summary>   Gets or sets the identifier of the material. </summary>
        ///
        /// <value> The identifier of the material. </value>
        public string MaterialId { get; set; }

        /// <summary>   Gets or sets the quantity. </summary>
        ///
        /// <value> The quantity. </value>
        public long Quantity { get; set; }

        /// <summary>   Gets or sets the quantity cost. </summary>
        ///
        /// <value> The quantity cost. </value>
        public double QuantityCost { get; set; }

        /// <summary>   Gets or sets the volume cost. </summary>
        ///
        /// <value> The volume cost. </value>
        public double VolumeCost { get; set; }

        /// <summary>   Gets or sets the area cost. </summary>
        ///
        /// <value> The area cost. </value>
        public double AreaCost { get; set; }

        /// <summary>   Gets or sets the length cost. </summary>
        ///
        /// <value> The length cost. </value>
        public double LengthCost { get; set; }
    }
}
