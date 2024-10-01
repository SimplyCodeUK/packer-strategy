// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItMock.Test.HttpMock
{
    using Xunit;
    using PackItMock.HttpMock;
    using System.Net;

    /// <summary> (Unit Test Method) MockRequest. </summary>
    public class TestHttpMock
    {
        /// <summary> (Unit Test Method) Mock HTTP Get. </summary>
        [Fact]
        public void TestMockHttpGet()
        {
            var request = new MockRequest(HttpMethod.Get, "http://url.co.uk/");
            Assert.Equal(request.Method, HttpMethod.Get);
            Assert.Equal(request.RequestUri, new Uri("http://url.co.uk/"));
            Assert.Null(request.Content);
            Assert.Equal(HttpStatusCode.OK, request.StatusCode);
        }

        /// <summary> (Unit Test Method) Mock HTTP Post. </summary>
        [Fact]
        public void TestMockHttpPost()
        {
            var request = new MockRequest(HttpMethod.Post, "http://url.co.uk/");
            Assert.Equal(request.Method, HttpMethod.Post);
            Assert.Equal(request.RequestUri, new Uri("http://url.co.uk/"));
            Assert.Null(request.Content);
            Assert.Equal(HttpStatusCode.Created, request.StatusCode);
        }

        /// <summary> (Unit Test Method) Mock HTTP Put. </summary>
        [Fact]
        public void TestMockHttpPut()
        {
            var request = new MockRequest(HttpMethod.Put, "http://url.co.uk/");
            Assert.Equal(request.Method, HttpMethod.Put);
            Assert.Equal(request.RequestUri, new Uri("http://url.co.uk/"));
            Assert.Null(request.Content);
            Assert.Equal(HttpStatusCode.Created, request.StatusCode);
        }

        /// <summary> (Unit Test Method) Mock HTTP Delete. </summary>
        [Fact]
        public void TestMockHttpDelete()
        {
            var request = new MockRequest(HttpMethod.Delete, "http://url.co.uk/");
            Assert.Equal(request.Method, HttpMethod.Delete);
            Assert.Equal(request.RequestUri, new Uri("http://url.co.uk/"));
            Assert.Null(request.Content);
            Assert.Equal(HttpStatusCode.Gone, request.StatusCode);
        }


        /// <summary> (Unit Test Method) Mock HTTP Post json contents. </summary>
        [Fact]
        public void TestMockHttpPostContents()
        {
            var request = new MockRequest(HttpMethod.Post, "http://url.co.uk/");
            request.ContentsJson("{}");
            Assert.NotNull(request.Content.Headers.ContentType);
#pragma warning disable CS8602, CS8604 // Dereference of a possibly null reference.
            Assert.NotNull(request.Content.Headers.ContentType.MediaType);
            Assert.Equal("application/json", request.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", request.Content.Headers.ContentType.CharSet);
            Assert.Equal(request.Content.Headers.ContentLength, 2);
#pragma warning restore CS8602, CS8604 // Dereference of a possibly null reference.
        }
    }
}
