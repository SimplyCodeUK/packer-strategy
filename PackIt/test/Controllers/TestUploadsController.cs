// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Moq.Protected;
    using Xunit;
    using PackItLib.Models;
    using PackIt.Controllers;

    /// <summary> (Unit Test Fixture) a controller for handling test plans. </summary>
    public class TestUploadsController
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
        private static readonly OptionsWrapper<AppSettings> Options = new(AppSettings);

        /// <summary> The controller under test. </summary>
        private UploadsController controller;

        /// <summary> Data to upload. </summary>
        private readonly UploadsController.Bulk bulk;

        /// <summary> Setup for all unit tests here. </summary>
        public TestUploadsController()
        {
            this.SetupServicesNotRunning();

            var text = File.ReadAllText("Controllers/TestData/uploadsPass.json");
            this.bulk = JsonSerializer.Deserialize<UploadsController.Bulk>(text);

            // make sure IDs are unique
            foreach (var item in this.bulk.Plans)
            {
                item.PlanId = Guid.NewGuid().ToString();
            }

            foreach (var item in this.bulk.Materials)
            {
                item.MaterialId = Guid.NewGuid().ToString();
            }

            foreach (var item in this.bulk.Packs)
            {
                item.PackId = Guid.NewGuid().ToString();
            }
        }

        /// <summary> (Unit Test Method) post successful. </summary>
        [Fact]
        public async Task PostSuccessful()
        {
            this.SetupServicesRunning();

            var result = await this.controller.Post(this.bulk);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);

            var obj = result as ObjectResult;
            Assert.Equal(obj.StatusCode, (int)HttpStatusCode.Created);

            Assert.IsType<Dictionary<string, List<string>>>(obj.Value);
            var ret = obj.Value as Dictionary<string, List<string>>;
            Assert.Equal(
                ret["pass"].Count,
                this.bulk.Materials.Count + this.bulk.Packs.Count + this.bulk.Plans.Count
            );
            Assert.Empty(ret["fail"]);
        }

        /// <summary> (Unit Test Method) posts no data. </summary>
        [Fact]
        public async Task PostNoData()
        {
            var result = await this.controller.Post(null);

            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, (result as BadRequestResult).StatusCode);
        }

        /// <summary> (Unit Test Method) post disconnected. </summary>
        [Fact]
        public async Task PostDisconnected()
        {
            var result = await this.controller.Post(this.bulk);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);

            ObjectResult obj = result as ObjectResult;
            Assert.Equal(obj.StatusCode, (int)HttpStatusCode.Conflict);

            Assert.IsType<Dictionary<string, List<string>>>(obj.Value);
            var ret = obj.Value as Dictionary<string, List<string>>;
            Assert.Empty(ret["pass"]);
            Assert.Equal(
                ret["fail"].Count,
                this.bulk.Materials.Count + this.bulk.Packs.Count + this.bulk.Plans.Count
            );
        }

        /// <summary> Setup the controller as if the services are not running. </summary>
        private void SetupServicesNotRunning()
        {
            this.controller = new(
                Mock.Of<ILogger<UploadsController>>(),
                Options);
            Assert.NotNull(this.controller);
        }

        /// <summary> Setup the controller as if the services are running. </summary>
        private void SetupServicesRunning()
        {
            var handler = new Mock<HttpMessageHandler>();

            // Mock protected method SendAsync
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(
                    Task<HttpResponseMessage>.Factory.StartNew(
                        () =>
                        {
                            return new HttpResponseMessage(HttpStatusCode.OK);
                        }));

            this.controller = new(
                Mock.Of<ILogger<UploadsController>>(),
                Options,
                handler.Object);
            Assert.NotNull(this.controller);
        }
    }
}
