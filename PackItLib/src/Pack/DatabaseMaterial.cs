// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.Pack
{
    using System.Diagnostics.CodeAnalysis;
    using PackItLib.Helpers.Enums;

    /// <summary> A key to a Material in its database. </summary>
    [ExcludeFromCodeCoverage]
    public class DatabaseMaterial
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DatabaseMaterial" /> class.
        /// </summary>
        public DatabaseMaterial()
        {
            this.MaterialType = MaterialType.Bottle;
        }

        /// <summary> Gets or sets the type of the Material. </summary>
        ///
        /// <value> The type of the Material. </value>
        public MaterialType MaterialType { get; set; }

        /// <summary> Gets or sets the Material identifier. </summary>
        ///
        /// <value> The Material identifier. </value>
        public string MaterialId { get; set; }
    }
}
