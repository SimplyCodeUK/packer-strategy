// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Test.Models
{
    using NUnit.Framework;
    using PackIt.Helpers.Masks;
    using static PackIt.Models.DoDrawingHelpers;

    /// <summary> (Unit Test Method) Convert a Pack to it's DTO. </summary>
    [TestFixture]
    public class TestDoDrawingHelper
    {
        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            // Nothing to do at the moment
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void TestRotateResultHeightToHeight()
        {
            PackIt.Pack.Result result = new()
            {
                ExternalLength = 100,
                ExternalBreadth = 200,
                ExternalHeight = 300
            };
            Dims dims = RotateResult(result, ResultRotation.ParentHeightToHeight);
            Assert.That(dims.Width, Is.EqualTo(result.ExternalLength));
            Assert.That(dims.Depth, Is.EqualTo(result.ExternalBreadth));
            Assert.That(dims.Height, Is.EqualTo(result.ExternalHeight));
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void TestRotateResultLenghtToHeight()
        {
            PackIt.Pack.Result result = new()
            {
                ExternalLength = 100,
                ExternalBreadth = 200,
                ExternalHeight = 300
            };
            Dims dims = RotateResult(result, ResultRotation.ParentLengthToHeight);
            Assert.That(dims.Depth, Is.EqualTo(result.ExternalLength));
            Assert.That(dims.Width, Is.EqualTo(result.ExternalBreadth));
            Assert.That(dims.Height, Is.EqualTo(result.ExternalHeight));
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void TestRotateResultBreadthToHeight()
        {
            PackIt.Pack.Result result = new()
            {
                ExternalLength = 100,
                ExternalBreadth = 200,
                ExternalHeight = 300
            };
            Dims dims = RotateResult(result, ResultRotation.ParentBreadthToHeight);
            Assert.That(dims.Width, Is.EqualTo(result.ExternalLength));
            Assert.That(dims.Height, Is.EqualTo(result.ExternalBreadth));
            Assert.That(dims.Depth, Is.EqualTo(result.ExternalHeight));
        }
    }
}
