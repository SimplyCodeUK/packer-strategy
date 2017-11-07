// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Helpers.Enums
{
    using System;

    /// <summary>   Values that represent cost types. </summary>
    [Flags]
    public enum SectionType
    {
        /// <summary> Major cross section. </summary>
        Major = 1,

        /// <summary> Minor cross section. </summary>
        Minor = 2,

        /// <summary> Shoulder cross section. </summary>
        Shoulder = 4,

        /// <summary> Finish cross section. </summary>
        Finish = 8,

        /// <summary> Neck cross section. </summary>
        Neck = 16
    }
}
