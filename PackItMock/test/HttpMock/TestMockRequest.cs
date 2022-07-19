// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItMock.Test.HttpMock
{
    using NUnit.Framework;
    using System.Net;
    using PackItMock.HttpMock;

    /// <summary> (Unit Test Method) MockRequest. </summary>
    [TestFixture]
    public class TestHttpMock
    {
        /// <summary> (Unit Test Method) Successful database context lookup. </summary>
        [Test]
        public void TestMockHttpGet()
        {
            var request = new MockRequest(HttpMethod.Get, "url.co.uk");
            Assert.AreEqual(request.Method, HttpMethod.Get);
            Assert.AreEqual(request.RequestUri, new Uri("url.co.uk"));
            Assert.IsNull(request.Content);
            Assert.AreEqual(request.StatusCode, HttpStatusCode.OK);
        }
    }
}
