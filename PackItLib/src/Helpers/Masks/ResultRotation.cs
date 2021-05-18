// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Helpers.Masks
{
    /// <summary> Values that represent result rotations. </summary>
    public class ResultRotation
    {
        /// <summary> A mask constant representing outout rotation of height to height. </summary>
        public static readonly int OutputHeightToHeight = 0x0001;

        /// <summary> A mask constant representing outout rotation of breadth to height. </summary>
        public static readonly int OutputBreadthToHeight = 0x0002;

        /// <summary> A mask constant representing outout rotation of length to height. </summary>
        public static readonly int OutputLengthToHeight = 0x0004;

        /// <summary> A mask constant representing parent rotation of height to height. </summary>
        public static readonly int ParentHeightToHeight = 0x0008;

        /// <summary> A mask constant representing parent rotation of breadth to height. </summary>
        public static readonly int ParentBreadthToHeight = 0x0010;

        /// <summary> A mask constant representing parent rotation of length to height. </summary>
        public static readonly int ParentLengthToHeight = 0x0020;

        /// <summary> A mask constant representing product rotation of height to height. </summary>
        public static readonly int ProductHeightToHeight = 0x0040;

        /// <summary> A mask constant representing product rotation of breadth to height. </summary>
        public static readonly int ProductBreadthToHeight = 0x0080;

        /// <summary> A mask constant representing product rotation of length to height. </summary>
        public static readonly int ProductLengthToHeight = 0x0100;

        /// <summary> A mask constant representing along length. </summary>
        public static readonly int AlongLength = 0x0200;

        /// <summary> A mask constant representing along breadth. </summary>
        public static readonly int AlongBreadth = 0x0400;
    }
}
