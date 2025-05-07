// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Models
{
    using PackItLib.Helpers.Masks;
    using PackItLib.Pack;

    /// <summary> Helper methods for drawing. </summary>
    public class DoDrawingHelpers
    {
        /// <summary> 3D Dimensions. </summary>
        public class Dims
        {
            /// <summary> Width </summary>
            public double Width { get; set; }

            /// <summary> Height </summary>
            public double Height { get; set; }

            /// <summary> Depth</summary>
            public double Depth { get; set; }
        }

        /// <summary> Rotate a result. </summary>
        ///
        /// <param name="result"> The result to rotate </param>
        /// <param name="rotation"> How to rotate it </param>
        /// <returns> The rotated dimensions </returns>
        public static Dims RotateResult(Result result, long rotation)
        {
            Dims ret = new()
            {
                Width = result.ExternalLength,
                Height = result.ExternalHeight,
                Depth = result.ExternalBreadth
            };
            if ((rotation & ResultRotation.ParentLengthToHeight) != 0)
            {
                ret.Width = result.ExternalBreadth;
                ret.Height = result.ExternalHeight;
                ret.Depth = result.ExternalLength;
            }
            else
            {
                if ((rotation & ResultRotation.ParentBreadthToHeight) != 0)
                {
                    ret.Width = result.ExternalLength;
                    ret.Height = result.ExternalBreadth;
                    ret.Depth = result.ExternalHeight;
                } // if
            }

            return ret;
        }
    }
}
