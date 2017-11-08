// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Material
{
    using System.ComponentModel.DataAnnotations;
    using PackIt.Helpers.Enums;

    /// <summary> A section. </summary>
    public class Section
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Section" /> class.
        /// </summary>
        public Section()
        {
            this.Shape = ShapeType.Rectangle;
        }

        /// <summary> Gets or sets the shape. </summary>
        ///
        /// <value> The shape. </value>
        public ShapeType Shape { get; set; }

        /// <summary> Gets or sets the section type. </summary>
        ///
        /// <value> The section type. </value>
        [Display(Name = "Type")]
        public SectionTypes SectionType { get; set; }

        /// <summary> Gets or sets the dimension 0. </summary>
        ///
        /// <value> The dimension 0. </value>
        public double Dimension0 { get; set; }

        /// <summary> Gets or sets the dimension 1. </summary>
        ///
        /// <value> The dimension 1. </value>
        public double Dimension1 { get; set; }

        /// <summary> Gets or sets the dimension 2. </summary>
        ///
        /// <value> The dimension 2. </value>
        public double Dimension2 { get; set; }

        /// <summary> Gets or sets the dimension 3. </summary>
        ///
        /// <value> The dimension 3. </value>
        public double Dimension3 { get; set; }

        /// <summary> Gets or sets the dimension 4. </summary>
        ///
        /// <value> The dimension 4. </value>
        public double Dimension4 { get; set; }

        /// <summary> Gets or sets the dimension 5. </summary>
        ///
        /// <value> The dimension 5. </value>
        public double Dimension5 { get; set; }

        /// <summary> Gets or sets the dimension 6. </summary>
        ///
        /// <value> The dimension 6. </value>
        public double Dimension6 { get; set; }

        /// <summary> Gets or sets the dimension 7. </summary>
        ///
        /// <value> The dimension 7. </value>
        public double Dimension7 { get; set; }

        /// <summary> Gets or sets the height. </summary>
        ///
        /// <value> The height. </value>
        public double Height { get; set; }
    }
}
