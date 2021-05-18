// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Drawing
{
    using System.Diagnostics.CodeAnalysis;
    using PackIt.Helpers.Enums;

    /// <summary> A 3D shape. </summary>
    [ExcludeFromCodeCoverage]
    public class Shape3D
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Shape3D" /> class.
        /// </summary>
        public Shape3D(FormType type)
        {
            this.Type = type;
            this.Length = 0.0;
            this.Breadth = 0.0;
            this.Height = 0.0;
            this.X = 0.0;
            this.Y = 0.0;
            this.Y = 0.0;
        }

        /// <summary> Gets or sets the length of the shape. </summary>
        ///
        /// <value> The length of the shape. </value>
        public double Length { get; set; }

        /// <summary> Gets or sets the breadth of the shape. </summary>
        ///
        /// <value> The breadth of the shape. </value>
        public double Breadth { get; set; }

        /// <summary> Gets or sets the height of the shape. </summary>
        ///
        /// <value> The height of the shape. </value>
        public double Height { get; set; }

        /// <summary> Gets or sets the X coordinate of the shape. </summary>
        ///
        /// <value> The X coordinate of the shape. </value>
        public double X { get; set; }

        /// <summary> Gets or sets the Y coordinate of the shape. </summary>
        ///
        /// <value> The Y coordinate of the shape. </value>
        public double Y { get; set; }

        /// <summary> Gets or sets the Z coordinate of the shape. </summary>
        ///
        /// <value> The Z coordinate of the shape. </value>
        public double Z { get; set; }

        /// <summary> The shape type. </summary>
        public FormType Type { get; }
    }
}
