//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Material
{
    /// <summary>   A design. </summary>
    public class Design
    {
        /// <summary>   Values that represent design types. </summary>
        public enum Type
        {
            /// <summary>   An enum constant representing the Minimum option. </summary>
            Min,
            /// <summary>   An enum constant representing the fefco option. </summary>
            Fefco = Min,
            /// <summary>   An enum constant representing the ecma option. </summary>
            Ecma,
            /// <summary>   An enum constant representing the user option. </summary>
            User,
            /// <summary>   An enum constant representing the Maximum option. </summary>
            Max
        }
    }
}
