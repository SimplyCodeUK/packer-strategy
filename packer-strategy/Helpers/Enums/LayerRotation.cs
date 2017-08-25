//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Helpers.Enums
{
    /// <summary>   Values that represent layer rotations. </summary>
    public enum LayerRotation
    {
        /// <summary>   An enum constant representing the unknown option. </summary>
        Unknown,
        /// <summary>   An enum constant representing the Minimum option. </summary>
        Min = 1,
        /// <summary>   An enum constant representing the east west option. </summary>
        EastWest = Min,
        /// <summary>   An enum constant representing the north south option. </summary>
        NorthSouth,
        /// <summary>   An enum constant representing the degrees 180 option. </summary>
        Degrees180,
        /// <summary>   An enum constant representing the degrees 90 option. </summary>
        Degrees90,
        /// <summary>   An enum constant representing the none option. </summary>
        None,
        /// <summary>   An enum constant representing the Maximum option. </summary>
        Max
    }
}
