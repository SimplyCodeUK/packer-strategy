﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Helpers.Enums
{
    using System;

    /// <summary> Values that represent cost types. </summary>
    [Flags]
    public enum SectionTypes
    {
        /// <summary> Undefined cross section. </summary>
        Undefined = 0,

        /// <summary> Major cross section. </summary>
        Major = 1,

        /// <summary> Minor cross section. </summary>
        Minor = 2,
    }
}
