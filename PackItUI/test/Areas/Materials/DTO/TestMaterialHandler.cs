// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.Materials.DTO
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Xunit;
    using PackItLib.Models;
    using PackItMock.HttpMock;
    using PackItUI.Areas.Materials.DTO;
    using PackItUI.Services;

    /// <summary> (Unit Test Fixture) a handler for test materials. </summary>
    public class TestMaterialHandler
    {
        /// <summary> The service endpoints. </summary>
        private static readonly ServiceEndpoints Endpoints = new()
        {
            Materials = "http://localhost:8001/api/v1/",
            Packs = "http://localhost:8002/api/v1/",
            Plans = "http://localhost:8003/api/v1/",
            Uploads = "http://localhost:8004/api/v1/",
            Drawings = "http://localhost:8005/api/v1/"
        };

        /// <summary> The application settings. </summary>
        private static readonly AppSettings AppSettings = new()
        {
            ServiceEndpoints = Endpoints
        };

        /// <summary> The options. </summary>
        private static readonly IOptions<AppSettings> Options = new OptionsWrapper<AppSettings>(AppSettings);

        /// <summary> The time out for disconnected services. </summary>
        private static readonly TimeSpan TimeOut = new(0, 0, 0, 0, 20);

        /// <summary> The handler under test. </summary>
        private MaterialHandler handler;

        /// <summary> Setup for all unit tests here. </summary>
        public TestMaterialHandler()
        {
            this.SetupConnected();
        }

        /// <summary> (Unit Test Method) index action when the service is up. </summary>
        [Fact]
        public async Task ConnectedInformationAsync()
        {
            var result = await this.handler.InformationAsync();
            Assert.IsType<ServiceInfo>(result);
            Assert.Equal("1", result.Version);
            Assert.Equal("Materials", result.About);
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Fact]
        public async Task DisconnectedInformationAsync()
        {
            this.SetupDisconnected();
            var result = await this.handler.InformationAsync();
            Assert.IsType<ServiceInfo>(result);
            Assert.Equal("Unknown", result.Version);
            Assert.Equal("Service down! http://localhost:8001/api/v1/", result.About);
        }

        /// <summary> Setup for disconnected service. </summary>
        private void SetupDisconnected()
        {
            this.handler = new(Options)
            {
                TimeOut = TimeOut
            };
            Assert.NotNull(this.handler);
            Assert.Equal(TimeOut, this.handler.TimeOut);
        }

        /// <summary> Setup for connected services. </summary>
        private void SetupConnected()
        {
            var root = Endpoints.Materials;
            var httpHandler = new MockHttpClientHandler();
            httpHandler
                .AddRequest(HttpMethod.Get, root)
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Materials\"}");
            this.handler = new(Options, httpHandler)
            {
                TimeOut = TimeOut
            };
            Assert.NotNull(this.handler);
            Assert.Equal(TimeOut, this.handler.TimeOut);
        }
    }
}
