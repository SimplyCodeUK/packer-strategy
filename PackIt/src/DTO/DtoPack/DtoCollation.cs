// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoPack
{
    using PackIt.Helpers.Enums;

    /// <summary> A dto collation. </summary>
    public class DtoCollation
    {
        /// <summary> Gets or sets the identifier of the Pack owns this item. </summary>
        ///
        /// <value> The identifier of the Pack. </value>
        public string PackId { get; set; }

        /// <summary> Gets or sets the level. </summary>
        ///
        /// <value> The level. </value>
        public StageLevel StageLevel { get; set; }

        /// <summary> Gets or sets the zero-based index of the Result index. </summary>
        ///
        /// <value> The Result index. </value>
        public long ResultIndex { get; set; }

        /// <summary> Gets or sets the zero-based index of the Layer index. </summary>
        ///
        /// <value> The Layer index. </value>
        public long LayerIndex { get; set; }

        /// <summary> Gets or sets the zero-based index of the Collation. </summary>
        ///
        /// <value> The Collation index. </value>
        public long CollationIndex { get; set; }

        /// <summary> Gets or sets the position x coordinate. </summary>
        ///
        /// <value> The position x coordinate. </value>
        public double PosX { get; set; }

        /// <summary> Gets or sets the position y coordinate. </summary>
        ///
        /// <value> The position y coordinate. </value>
        public double PosY { get; set; }

        /// <summary> Gets or sets the gap x coordinate. </summary>
        ///
        /// <value> The gap x coordinate. </value>
        public double GapX { get; set; }

        /// <summary> Gets or sets the gap y coordinate. </summary>
        ///
        /// <value> The gap y coordinate. </value>
        public double GapY { get; set; }

        /// <summary> Gets or sets the total number of x coordinate. </summary>
        ///
        /// <value> The total number of x coordinate. </value>
        public long CountX { get; set; }

        /// <summary> Gets or sets the total number of y coordinate. </summary>
        ///
        /// <value> The total number of y coordinate. </value>
        public long CountY { get; set; }

        /// <summary> Gets or sets the even. </summary>
        ///
        /// <value> The even. </value>
        public double Even { get; set; }

        /// <summary> Gets or sets the nested. </summary>
        ///
        /// <value> The nested. </value>
        public long Nested { get; set; }

        /// <summary> Gets or sets a value indicating whether the along breadth. </summary>
        ///
        /// <value> True if along breadth, false if not. </value>
        public bool AlongBreadth { get; set; }

        /// <summary> Gets or sets the rotation. </summary>
        ///
        /// <value> The rotation. </value>
        public long Rotation { get; set; }
    }
}
