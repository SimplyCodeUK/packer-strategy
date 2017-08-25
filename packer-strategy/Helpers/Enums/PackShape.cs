//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Helpers.Enums
{
    /// <summary>   Values that represent pack shapes. </summary>
    public enum PackShape
    {
        /// <summary>   An enum constant representing the Minimum option. </summary>
        Min,
        /// <summary>   An enum constant representing the rectangle option. </summary>
        Rectangle = Min,
        /// <summary>   An enum constant representing the circle option. </summary>
        Circle,
        /// <summary>   An enum constant representing the oval option. </summary>
        Oval,
        /// <summary>   An enum constant representing the round rectangle option. </summary>
        RoundRectangle,
        /// <summary>   An enum constant representing the Maximum option. </summary>
        Max
    };
}
