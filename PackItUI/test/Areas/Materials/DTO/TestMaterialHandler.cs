// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.Materials.DTO
{
    using System;
    using System.Net.Http;
    using Microsoft.Extensions.Options;
    using NUnit.Framework;
    using PackItUI.Areas.App.Models;
    using PackItUI.Areas.Materials.DTO;
    using PackItUI.Services;
    using PackItUI.Test.HttpMock;

    /// <summary> (Unit Test Fixture) a handler for test materials. </summary>
    [TestFixture]
    public class TestMaterialHandler
    {
        /// <summary> The service endpoints. </summary>
        private static readonly ServiceEndpoints Endpoints = new ServiceEndpoints
        {
            Materials = "http://localhost:8001/api/v1/",
            Packs = "http://localhost:8002/api/v1/",
            Plans = "http://localhost:8003/api/v1/",
            Uploads = "http://localhost:8004/api/v1/"
        };

        /// <summary> The application settings. </summary>
        private static readonly AppSettings AppSettings = new AppSettings
        {
            ServiceEndpoints = Endpoints
        };

        /// <summary> The options. </summary>
        private static readonly IOptions<AppSettings> Options = new OptionsWrapper<AppSettings>(AppSettings);

        /// <summary> The time out for disconnected services. </summary>
        private static readonly TimeSpan TimeOut = new TimeSpan(0, 0, 0, 0, 20);

        /// <summary> The handler under test. </summary>
        private MaterialHandler handler;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.SetupConnected();
        }

        /// <summary> (Unit Test Method) index action when the service is up. </summary>
        [Test]
        public void ConnectedInformationAsync()
        {
            var result = this.handler.InformationAsync();
            result.Wait();
            Assert.IsInstanceOf<ServiceInfo>(result.Result);
            Assert.AreEqual(result.Result.Version, "1");
            Assert.AreEqual(result.Result.About, "Materials");
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Test]
        public void DisconnectedInformationAsync()
        {
            this.SetupDisconnected();
            var result = this.handler.InformationAsync();
            result.Wait();
            Assert.IsInstanceOf<ServiceInfo>(result.Result);
            Assert.AreEqual(result.Result.Version, "Unknown");
            Assert.AreEqual(result.Result.About, "Service down! http://localhost:8001/api/v1/");
        }

        /// <summary> Setup for disconnected service. </summary>
        private void SetupDisconnected()
        {
            this.handler = new MaterialHandler(Options)
            {
                TimeOut = TimeOut
            };
            Assert.IsNotNull(this.handler);
            Assert.AreEqual(this.handler.TimeOut, TimeOut);
        }

        /// <summary> Setup for connected services. </summary>
        private void SetupConnected()
        {
            var root = Endpoints.Materials;
            var httpHandler = new MockHttpClientHandler();
            httpHandler
                .AddRequest(HttpMethod.Get, root)
                .ContentsJson("{'Version': '1', 'About': 'Materials'}");
            this.handler = new MaterialHandler(Options, httpHandler)
                {
                    TimeOut = TimeOut
                };
            Assert.IsNotNull(this.handler);
            Assert.AreEqual(this.handler.TimeOut, TimeOut);
        }
    }
}
