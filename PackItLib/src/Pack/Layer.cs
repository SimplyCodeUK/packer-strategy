// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.Pack
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PackItLib.Helpers.Enums;

    /// <summary> A layer. </summary>
    [ExcludeFromCodeCoverage]
    public class Layer
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Layer" /> class.
        /// </summary>
        public Layer()
        {
            this.Collations = new List<Collation>();
        }

        /// <summary> Gets or sets the layers. </summary>
        ///
        /// <value> The layers. </value>
        public long Layers { get; set; }

        /// <summary> Gets or sets a value indicating whether there is output rotation. </summary>
        ///
        /// <value> True if output rotation, false if not. </value>
        public bool OutputRotation { get; set; }

        /// <summary> Gets or sets a value indicating whether there is parent rotation. </summary>
        ///
        /// <value> True if parent rotation, false if not. </value>
        public bool ParentRotation { get; set; }

        /// <summary> Gets or sets the rotation. </summary>
        ///
        /// <value> The rotation. </value>
        public long Rotation { get; set; }

        /// <summary> Gets or sets the percent. </summary>
        ///
        /// <value> The percent. </value>
        public double Percent { get; set; }

        /// <summary> Gets or sets the length. </summary>
        ///
        /// <value> The length. </value>
        public double Length { get; set; }

        /// <summary> Gets or sets the breadth. </summary>
        ///
        /// <value> The breadth. </value>
        public double Breadth { get; set; }

        /// <summary> Gets or sets the bond rotation. </summary>
        ///
        /// <value> The bond rotation. </value>
        public BondRotation BondRotation { get; set; }

        /// <summary> Gets or sets the bond east west. </summary>
        ///
        /// <value> The bond east west. </value>
        public double BondEastWest { get; set; }

        /// <summary> Gets or sets the bond north south. </summary>
        ///
        /// <value> The bond north south. </value>
        public double BondNorthSouth { get; set; }

        /// <summary> Gets or sets the bond 180 degrees. </summary>
        ///
        /// <value> The bond 180 degrees. </value>
        public double Bond180Degrees { get; set; }

        /// <summary> Gets or sets the bond 90 degrees. </summary>
        ///
        /// <value> The bond 90 degrees. </value>
        public double Bond90Degrees { get; set; }

        /// <summary> Gets or sets the line east west. </summary>
        ///
        /// <value> The line east west. </value>
        public double LineEastWest { get; set; }

        /// <summary> Gets or sets the line north south. </summary>
        ///
        /// <value> The line north south. </value>
        public double LineNorthSouth { get; set; }

        /// <summary> Gets or sets the line 180 degrees. </summary>
        ///
        /// <value> The line 180 degrees. </value>
        public double Line180Degrees { get; set; }

        /// <summary> Gets or sets the line 90. </summary>
        ///
        /// <value> The line 90. </value>
        public double Line90Degrees { get; set; }

        /// <summary> Gets or sets the number of.  </summary>
        ///
        /// <value> The count. </value>
        public long Count { get; set; }

        /// <summary> Gets or sets the collection of collations. </summary>
        ///
        /// <value> Collection of collations. </value>
        public IList<Collation> Collations { get; set; }
    }
}
