// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoPack
{
    using PackIt.Helpers.Enums;

    /// <summary> A dto section. </summary>
    public class DtoSection
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

        /// <summary> Gets or sets the zero-based index of the Section. </summary>
        ///
        /// <value> The Section index. </value>
        public long SectionIndex { get; set; }

        /// <summary> Gets or sets the shape. </summary>
        ///
        /// <value> The shape. </value>
        public ShapeType Shape { get; set; }

        /// <summary> Gets or sets the section type. </summary>
        ///
        /// <value> The section type. </value>
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
