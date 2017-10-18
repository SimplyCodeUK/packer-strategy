// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Helpers.MaterialType
{
    /// <summary> Material Type capabilities. </summary>
    public abstract class Capabilities
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Capabilities"/> class.
        /// </summary>
        ///
        /// <param name="hasPalletDecks"> Does the material type support pallet decks. </param>
        protected Capabilities(bool hasPalletDecks = false)
        {
            this.HasPalletDecks = hasPalletDecks;
        }

        /// <summary> Gets a value indicating whether the material type supports pallet decks. </summary>
        ///
        /// <value> Flag indicating if the material type supports pallet decks. </value>
        public bool HasPalletDecks { get; }
    }
}
