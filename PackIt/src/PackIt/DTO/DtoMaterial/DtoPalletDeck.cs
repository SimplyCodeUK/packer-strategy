// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoMaterial
{
    using System.Collections.Generic;

    /// <summary>   A dto pallet deck. </summary>
    public class DtoPalletDeck
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DtoPalletDeck" /> class.
        /// </summary>
        public DtoPalletDeck()
        {
            this.Planks = new List<DtoPlank>();
        }

        /// <summary>   Gets or sets the identifier of the Pack owns this item. </summary>
        ///
        /// <value> The identifier of the Pack. </value>
        public string MaterialId { get; set; }

        /// <summary>   Gets or sets the zero-based index of the PalletDeck. </summary>
        ///
        /// <value> The PalletDeck index. </value>
        public long PalletDeckIndex { get; set; }

        /// <summary>   Gets or sets caliper. </summary>
        ///
        /// <value> The caliper. </value>
        public double Caliper { get; set; }

        /// <summary>   Gets or sets the collection of planks. </summary>
        ///
        /// <value> Collection of planks. </value>
        public IList<DtoPlank> Planks { get; set; }
    }
}
