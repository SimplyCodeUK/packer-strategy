// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Helpers.MaterialType
{
    /// <summary> Material Type factory class. </summary>
    public static partial class Factory
    {
        /// <summary>   Values that represent material type Pallet. </summary>
        private class Pallet : Capabilities
        {
            /// <summary>
            /// Initialises a new instance of the <see cref="Pallet"/> class.
            /// </summary>
            public Pallet() : base(hasPalletDecks: true)
            {
            }
        }
    }
}
