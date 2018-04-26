// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Material
{
    using System.Collections.Generic;

    /// <summary> A pallet deck. </summary>
    public class PalletDeck
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PalletDeck" /> class.
        /// </summary>
        public PalletDeck()
        {
            this.Planks = new List<Plank>();
        }

        /// <summary> Gets or sets caliper. </summary>
        ///
        /// <value> The caliper. </value>
        public double Caliper { get; set; }

        /// <summary> Gets or sets the collection of planks. </summary>
        ///
        /// <value> Collection of planks. </value>
        public IList<Plank> Planks { get; set; }
    }
}
