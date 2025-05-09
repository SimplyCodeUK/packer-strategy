// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.Material
{
    using System.Diagnostics.CodeAnalysis;
    using PackItLib.Helpers.Enums;

    /// <summary> A plank. </summary>
    [ExcludeFromCodeCoverage]
    public class Plank
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Plank" /> class.
        /// </summary>
        public Plank()
        {
            this.Shape = ShapeType.Rectangle;
        }

        /// <summary> Gets or sets the number to place along the x axis. </summary>
        ///
        /// <value> The number to place along the x axis. </value>
        public long NoX { get; set; }

        /// <summary> Gets or sets the number to place along the y axis. </summary>
        ///
        /// <value> The number to place along the y axis. </value>
        public long NoY { get; set; }

        /// <summary> Gets or sets the shape. </summary>
        ///
        /// <value> The shape. </value>
        public ShapeType Shape { get; set; }

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

        /// <summary> Gets or sets the length. </summary>
        ///
        /// <value> The length. </value>
        public double Length { get; set; }

        /// <summary> Gets or sets the breadth. </summary>
        ///
        /// <value> The breadth. </value>
        public double Breadth { get; set; }
    }
}
