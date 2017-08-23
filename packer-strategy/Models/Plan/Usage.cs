//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Plan
{
    /// <summary>   Values that represent types. </summary>
    public enum UsageType
    {
        /// <summary>   An enum constant representing the Minimum option. </summary>
        Min,
        /// <summary>   An enum constant representing the outer option. </summary>
        Outer = Min,
        /// <summary>   An enum constant representing the base layer option. </summary>
        BaseLayer,
        /// <summary>   An enum constant representing the inter layer option. </summary>
        InterLayer,
        /// <summary>   An enum constant representing the top layer option. </summary>
        TopLayer,
        /// <summary>   An enum constant representing the other inserts option. </summary>
        OtherInserts,
        /// <summary>   An enum constant representing the trimmings option. </summary>
        Trimmings,
        /// <summary>   An enum constant representing the Maximum option. </summary>
        Max
    }
}
