// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Helpers.Enums
{
    /// <summary>   Values that represent pattern generators. </summary>
    public enum PatternGenerator
    {
        /// <summary>   An enum constant representing the Minimum option. </summary>
        Min,

        /// <summary>   An enum constant representing the split option. </summary>
        Split = Min,

        /// <summary>   An enum constant representing the split 2 option. </summary>
        Split2,

        /// <summary>   An enum constant representing the step option. </summary>
        Step,

        /// <summary>   An enum constant representing the nested option. </summary>
        Nested,

        /// <summary>   An enum constant representing the around option. </summary>
        Around,

        /// <summary>   An enum constant representing the odd spiral option. </summary>
        OddSpiral,

        /// <summary>   An enum constant representing the variable option. </summary>
        Variable,

        /// <summary>   An enum constant representing the two step option. </summary>
        TwoStep,

        /// <summary>   An enum constant representing the Maximum option. </summary>
        Max
    }
}
