// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.Packs.DTO
{
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Xunit;
    using PackIt.Models;
    using PackItMock.HttpMock;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Services;

    /// <summary> (Unit Test Fixture) a handler for test drawings. </summary>
    public class TestDrawHandler
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
        private DrawHandler handler;

        /// <summary> Setup for all unit tests here. </summary>
        public TestDrawHandler()
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
            Assert.Equal("Drawings", result.About);
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Fact]
        public async Task  DisconnectedInformationAsync()
        {
            this.SetupDisconnected();
            var result = await this.handler.InformationAsync();

            Assert.IsType<ServiceInfo>(result);
            Assert.Equal("Unknown", result.Version);
            Assert.Equal("Service down! http://localhost:8005/api/v1/", result.About);
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Fact]
        public async Task CreateAsync()
        {
            var result = await this.handler.CreateAsync(new());

            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(System.Net.HttpStatusCode.Created, result.StatusCode);
            var content = await result.Content.ReadAsStringAsync();

            JsonDocument cont = JsonDocument.Parse(content);
            Assert.Equal("1111-2222-3333-4444", cont.RootElement.GetProperty("id").GetString());
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Fact]
        public async Task CreateAsyncDisconnected()
        {
            this.SetupDisconnected();
            var result = await this.handler.CreateAsync(new());
            Assert.Null(result);
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Fact]
        public async Task DrawAsync()
        {
            var result = await this.handler.ReadAsync("id");
            Assert.NotNull(result);
            Assert.IsType<PackIt.Drawing.Drawing>(result);
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Fact]
        public async Task DrawAsyncDisconnected()
        {
            this.SetupDisconnected();
            var result = await this.handler.ReadAsync("");
            Assert.Null(result);
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
            var root = Endpoints.Drawings;
            var httpHandler = new MockHttpClientHandler();
            httpHandler
                .AddRequest(HttpMethod.Get, root)
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Drawings\"}");
            httpHandler
                .AddRequest(HttpMethod.Get, root + "Drawings/id")
                .ContentsJson("{\"DrawingId\": \"1234-567-89\", \"Computed\": false}");
            httpHandler
                .AddRequest(HttpMethod.Post, root + "Drawings")
                .ContentsJson("{\"id\": \"1111-2222-3333-4444\"}");
            this.handler = new(Options, httpHandler)
            {
                TimeOut = TimeOut
            };
            Assert.NotNull(this.handler);
            Assert.Equal(TimeOut, this.handler.TimeOut);
        }
    }
}
