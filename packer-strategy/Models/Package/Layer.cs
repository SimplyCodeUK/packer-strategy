//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Package
{
    using System.Collections.Generic;

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

        public long Index { get; set; }
        public long Layers { get; set; }
        public bool OutputRotation { get; set; }
        public bool ParentRotation { get; set; }
        public long Rotation { get; set; }
        public double Percent { get; set; }
        public double Length { get; set; }
        public double Breadth { get; set; }
        public LayerRotation BondRotation { get; set; }
        public double Bond_EW { get; set; }
        public double Bond_NS { get; set; }
        public double Bond_180 { get; set; }
        public double Bond_90 { get; set; }
        public double Line_EW { get; set; }
        public double Line_NS { get; set; }
        public double Line_180 { get; set; }
        public double Line_90 { get; set; }
        public long Count { get; set; }

        public List<Collation> Collations { get; set; }
    }
}
