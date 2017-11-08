// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoPack
{
    using PackIt.Helpers.Enums;

    /// <summary> A dto database material. </summary>
    public class DtoDatabaseMaterial
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

        /// <summary> Gets or sets the zero-based index of the Material. </summary>
        ///
        /// <value> The Material index. </value>
        public long MaterialIndex { get; set; }

        /// <summary> Gets or sets the zero-based index of the Database Material. </summary>
        ///
        /// <value> The index. </value>
        public long DatabaseMaterialIndex { get; set; }

        /// <summary> Gets or sets the Material identifier. </summary>
        ///
        /// <value> The Material identifier. </value>
        public string MaterialId { get; set; }
    }
}
