// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.DTO.DtoDrawing
{
    using PackItLib.Helpers.Enums;

    /// <summary> A dto 3D shape. </summary>
    public class DtoShape3D
    {
        /// <summary> Gets or sets the drawing id. </summary>
        ///
        /// <value> The drawing id. </value>
        public string DrawingId { get; set; }

        /// <summary> Gets or sets the zero-based index of this 3D Shape. </summary>
        ///
        /// <value> The Shape index. </value>
        public long ShapeIndex { get; set; }

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
