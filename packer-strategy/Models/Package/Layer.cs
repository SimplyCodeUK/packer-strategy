//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Package
{
    using System.Collections.Generic;

    /// <summary>   A layer. </summary>
    public class Layer
    {
        /// <summary>   Values that represent layer rotations. </summary>
        public enum LayerRotation
        {
            /// <summary>   An enum constant representing the unknown option. </summary>
            Unknown,
            /// <summary>   An enum constant representing the Minimum option. </summary>
            Min = 1,
            /// <summary>   An enum constant representing the east west option. </summary>
            EastWest = Min,
            /// <summary>   An enum constant representing the north south option. </summary>
            NorthSouth,
            /// <summary>   An enum constant representing the degrees 180 option. </summary>
            Degrees180,
            /// <summary>   An enum constant representing the degrees 90 option. </summary>
            Degrees90,
            /// <summary>   An enum constant representing the none option. </summary>
            None,
            /// <summary>   An enum constant representing the Maximum option. </summary>
            Max
        }

        /// <summary>   Default constructor. </summary>
        public Layer()
        {
            Collations = new List<Collation>();
        }

        /// <summary>   Gets or sets the identifier that owns this item. </summary>
        ///
        /// <value> The identifier of the owner. </value>
        public string OwnerId { get; set; }

        /// <summary>   Gets or sets the level. </summary>
        ///
        /// <value> The level. </value>
        public Plan.Stage.StageLevel Level { get; set; }

        /// <summary>   Gets or sets the zero-based index of this object. </summary>
        ///
        /// <value> The index. </value>
        public long ResultIndex { get; set; }

        /// <summary>   Gets or sets the zero-based index of this object. </summary>
        ///
        /// <value> The index. </value>
        public long Index { get; set; }

        /// <summary>   Gets or sets the layers. </summary>
        ///
        /// <value> The layers. </value>
        public long Layers { get; set; }

        /// <summary>   Gets or sets a value indicating whether the output rotation. </summary>
        ///
        /// <value> True if output rotation, false if not. </value>
        public bool OutputRotation { get; set; }

        /// <summary>   Gets or sets a value indicating whether the parent rotation. </summary>
        ///
        /// <value> True if parent rotation, false if not. </value>
        public bool ParentRotation { get; set; }

        /// <summary>   Gets or sets the rotation. </summary>
        ///
        /// <value> The rotation. </value>
        public long Rotation { get; set; }

        /// <summary>   Gets or sets the percent. </summary>
        ///
        /// <value> The percent. </value>
        public double Percent { get; set; }

        /// <summary>   Gets or sets the length. </summary>
        ///
        /// <value> The length. </value>
        public double Length { get; set; }

        /// <summary>   Gets or sets the breadth. </summary>
        ///
        /// <value> The breadth. </value>
        public double Breadth { get; set; }

        /// <summary>   Gets or sets the bond rotation. </summary>
        ///
        /// <value> The bond rotation. </value>
        public LayerRotation BondRotation { get; set; }

        /// <summary>   Gets or sets the bond east west. </summary>
        ///
        /// <value> The bond east west. </value>
        public double BondEastWest { get; set; }

        /// <summary>   Gets or sets the bond north south. </summary>
        ///
        /// <value> The bond north south. </value>
        public double BondNorthSouth { get; set; }

        /// <summary>   Gets or sets the bond 180 degrees. </summary>
        ///
        /// <value> The bond 180 degrees. </value>
        public double Bond180Degrees { get; set; }

        /// <summary>   Gets or sets the bond 90 degrees. </summary>
        ///
        /// <value> The bond 90 degrees. </value>
        public double Bond90Degrees { get; set; }

        /// <summary>   Gets or sets the line east west. </summary>
        ///
        /// <value> The line east west. </value>
        public double LineEastWest { get; set; }

        /// <summary>   Gets or sets the line north south. </summary>
        ///
        /// <value> The line north south. </value>
        public double LineNorthSouth { get; set; }

        /// <summary>   Gets or sets the line 180 degrees. </summary>
        ///
        /// <value> The line 180 degrees. </value>
        public double Line180Degrees { get; set; }

        /// <summary>   Gets or sets the line 90. </summary>
        ///
        /// <value> The line 90. </value>
        public double Line90Degrees { get; set; }

        /// <summary>   Gets or sets the number of.  </summary>
        ///
        /// <value> The count. </value>
        public long Count { get; set; }

        /// <summary>   Gets or sets the collations. </summary>
        ///
        /// <value> The collations. </value>
        public List<Collation> Collations { get; set; }
    }
}
