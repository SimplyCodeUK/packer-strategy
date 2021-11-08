// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItBdd.Test.Drivers
{
    using NUnit.Framework;
    using PackItBdd.Drivers;
    using PackItMock.HttpMock;
    using System.Net.Http;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestsHttpHandler
    {
        /// <summary> The test root endpoint. </summary>
        private static readonly string root = "http://localhost:8001/api/v1/";

        private HttpHandler handler;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void Setup()
        {
            var httpHandler = new MockHttpClientHandler();
            httpHandler
                .AddRequest(HttpMethod.Get, root)
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Materials\"}");
            httpHandler
                .AddRequest(HttpMethod.Get, root + "Materials")
                .ContentsJson("[{\"MaterialId\" : \"Id1\"}, {\"MaterialId\" : \"Id2\"}]");
            httpHandler
                .AddRequest(HttpMethod.Post, root + "Materials");
            httpHandler
                .AddRequest(HttpMethod.Get, root + "Materials/Id1")
                .ContentsJson("{\"MaterialId\" : \"Id1\"}");
            httpHandler
                .AddRequest(HttpMethod.Put, root + "Materials/Id1");
            httpHandler
                .AddRequest(HttpMethod.Delete, root + "Materials/Id1");

            this.handler = new(httpHandler);
            Assert.IsNotNull(this.handler);
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Test]
        public void TestGet()
        {
            this.handler.Get(root);
            Assert.AreEqual(this.handler.ResponseStatusCode(), 200);
        }
    }
}
