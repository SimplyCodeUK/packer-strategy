// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItMock.Test.HttpMock
{
    using NUnit.Framework;
    using PackItMock.HttpMock;
    using System.Net;

    /// <summary> (Unit Test Method) MockRequest. </summary>
    [TestFixture]
    public class TestHttpMock
    {
        /// <summary> (Unit Test Method) Mock HTTP Get. </summary>
        [Test]
        public void TestMockHttpGet()
        {
            var request = new MockRequest(HttpMethod.Get, "http://url.co.uk/");
            Assert.AreEqual(request.Method, HttpMethod.Get);
            Assert.AreEqual(request.RequestUri, new Uri("http://url.co.uk/"));
            Assert.IsNull(request.Content);
            Assert.AreEqual(request.StatusCode, HttpStatusCode.OK);
        }

        /// <summary> (Unit Test Method) Mock HTTP Post. </summary>
        [Test]
        public void TestMockHttpPost()
        {
            var request = new MockRequest(HttpMethod.Post, "http://url.co.uk/");
            Assert.AreEqual(request.Method, HttpMethod.Post);
            Assert.AreEqual(request.RequestUri, new Uri("http://url.co.uk/"));
            Assert.IsNull(request.Content);
            Assert.AreEqual(request.StatusCode, HttpStatusCode.Created);
        }

        /// <summary> (Unit Test Method) Mock HTTP Put. </summary>
        [Test]
        public void TestMockHttpPut()
        {
            var request = new MockRequest(HttpMethod.Put, "http://url.co.uk/");
            Assert.AreEqual(request.Method, HttpMethod.Put);
            Assert.AreEqual(request.RequestUri, new Uri("http://url.co.uk/"));
            Assert.IsNull(request.Content);
            Assert.AreEqual(request.StatusCode, HttpStatusCode.Created);
        }

        /// <summary> (Unit Test Method) Mock HTTP Delete. </summary>
        [Test]
        public void TestMockHttpDelete()
        {
            var request = new MockRequest(HttpMethod.Delete, "http://url.co.uk/");
            Assert.AreEqual(request.Method, HttpMethod.Delete);
            Assert.AreEqual(request.RequestUri, new Uri("http://url.co.uk/"));
            Assert.IsNull(request.Content);
            Assert.AreEqual(request.StatusCode, HttpStatusCode.Gone);
        }


        /// <summary> (Unit Test Method) Mock HTTP Post json contents. </summary>
        [Test]
        public void TestMockHttpPostContents()
        {
            var request = new MockRequest(HttpMethod.Post, "http://url.co.uk/");
            request.ContentsJson("{}");
            Assert.NotNull(request.Content.Headers.ContentType);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.AreEqual(request.Content.Headers.ContentType.MediaType, "application/json");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Assert.AreEqual(request.Content.Headers.ContentType.CharSet, "utf-8");
            Assert.AreEqual(request.Content.Headers.ContentLength, 2);
        }
    }
}
