// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Test.Models
{
    using Xunit;
    using PackIt.Helpers.Masks;
    using static PackIt.Models.DoDrawingHelpers;

    /// <summary> (Unit Test Method) Convert a Pack to it's DTO. </summary>
    public class TestDoDrawingHelper
    {
        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void TestRotateResultHeightToHeight()
        {
            PackIt.Pack.Result result = new()
            {
                ExternalLength = 100,
                ExternalBreadth = 200,
                ExternalHeight = 300
            };
            Dims dims = RotateResult(result, ResultRotation.ParentHeightToHeight);
            Assert.Equal(result.ExternalLength, dims.Width);
            Assert.Equal(result.ExternalBreadth, dims.Depth);
            Assert.Equal(result.ExternalHeight, dims.Height);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void TestRotateResultLenghtToHeight()
        {
            PackIt.Pack.Result result = new()
            {
                ExternalLength = 100,
                ExternalBreadth = 200,
                ExternalHeight = 300
            };
            Dims dims = RotateResult(result, ResultRotation.ParentLengthToHeight);
            Assert.Equal(result.ExternalLength, dims.Depth);
            Assert.Equal(result.ExternalBreadth, dims.Width);
            Assert.Equal(result.ExternalHeight, dims.Height);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void TestRotateResultBreadthToHeight()
        {
            PackIt.Pack.Result result = new()
            {
                ExternalLength = 100,
                ExternalBreadth = 200,
                ExternalHeight = 300
            };
            Dims dims = RotateResult(result, ResultRotation.ParentBreadthToHeight);
            Assert.Equal(result.ExternalLength, dims.Width);
            Assert.Equal(result.ExternalBreadth, dims.Height);
            Assert.Equal(result.ExternalHeight, dims.Depth);
        }
    }
}
