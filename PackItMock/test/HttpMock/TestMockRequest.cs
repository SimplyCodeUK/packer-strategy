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
            Assert.That(request.Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(request.RequestUri, Is.EqualTo(new Uri("http://url.co.uk/")));
            Assert.That(request.Content, Is.Null);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary> (Unit Test Method) Mock HTTP Post. </summary>
        [Test]
        public void TestMockHttpPost()
        {
            var request = new MockRequest(HttpMethod.Post, "http://url.co.uk/");
            Assert.That(request.Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(request.RequestUri, Is.EqualTo(new Uri("http://url.co.uk/")));
            Assert.That(request.Content, Is.Null);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        /// <summary> (Unit Test Method) Mock HTTP Put. </summary>
        [Test]
        public void TestMockHttpPut()
        {
            var request = new MockRequest(HttpMethod.Put, "http://url.co.uk/");
            Assert.That(request.Method, Is.EqualTo(HttpMethod.Put));
            Assert.That(request.RequestUri, Is.EqualTo(new Uri("http://url.co.uk/")));
            Assert.That(request.Content, Is.Null);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        /// <summary> (Unit Test Method) Mock HTTP Delete. </summary>
        [Test]
        public void TestMockHttpDelete()
        {
            var request = new MockRequest(HttpMethod.Delete, "http://url.co.uk/");
            Assert.That(request.Method, Is.EqualTo(HttpMethod.Delete));
            Assert.That(request.RequestUri, Is.EqualTo(new Uri("http://url.co.uk/")));
            Assert.That(request.Content, Is.Null);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.Gone));
        }


        /// <summary> (Unit Test Method) Mock HTTP Post json contents. </summary>
        [Test]
        public void TestMockHttpPostContents()
        {
            var request = new MockRequest(HttpMethod.Post, "http://url.co.uk/");
            request.ContentsJson("{}");
            Assert.That(request.Content.Headers.ContentType, Is.Not.Null);
#pragma warning disable CS8602, CS8604 // Dereference of a possibly null reference.
            Assert.That(request.Content.Headers.ContentType.MediaType, Is.Not.Null);
            Assert.That(request.Content.Headers.ContentType.MediaType, Is.EqualTo("application/json"));
            Assert.That(request.Content.Headers.ContentType.CharSet, Is.EqualTo("utf-8"));
            Assert.That(request.Content.Headers.ContentLength, Is.EqualTo(2));
#pragma warning restore CS8602, CS8604 // Dereference of a possibly null reference.
        }
    }
}
