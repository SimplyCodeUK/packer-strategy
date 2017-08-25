﻿//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Helpers.Enums
{
    /// <summary>   Values that represent cost types. </summary>
    public enum CostType
    {
        /// <summary>   An enum constant representing the Minimum option. </summary>
        Min,
        /// <summary>   An enum constant representing the unit option. </summary>
        Unit = Min,
        /// <summary>   An enum constant representing the area option. </summary>
        Area,
        /// <summary>   An enum constant representing the volume option. </summary>
        Volume,
        /// <summary>   An enum constant representing the Maximum option. </summary>
        Max
    }
}