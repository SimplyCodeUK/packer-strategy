// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Helpers.MaterialType
{
    using System;
    using System.Collections.Generic;

    /// <summary> Material Type factory class. </summary>
    public static partial class Factory
    {
        /// <summary>
        ///  Dictionary of Capabilities
        /// </summary>
        private static Dictionary<Enums.MaterialType, Capabilities> dictionary = new Dictionary<Enums.MaterialType, Capabilities>()
        {
            { Enums.MaterialType.Pallet, new Pallet() }
        };

        /// <summary> Create Material Type factory function. </summary>
        ///
        /// <param name="type"> The Material Type enum. </param>
        ///
        /// <returns> The Material Type capabilities. </returns>
        public static Capabilities Create(Enums.MaterialType type)
        {
            if (dictionary.ContainsKey(type))
            {
                return dictionary[type];
            }

            return new Default();
        }
    }
}
