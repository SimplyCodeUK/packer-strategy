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
    using NUnit.Framework;
    using PackIt.Controllers;
    using PackIt.Models;

    /// <summary> (Unit Test Fixture) a controller for handling test plans. </summary>
    [TestFixture]
    public class TestUploadsController
    {
        /// <summary> The service endpoints. </summary>
        private static readonly ServiceEndpoints Endpoints = new()
        {
            Materials = "http://localhost:8001/api/v1/",
            Packs = "http://localhost:8002/api/v1/",
            Plans = "http://localhost:8003/api/v1/",
            Uploads = "http://localhost:8004/api/v1/",
            Drawings = "http://localhost:5000/api/v1/"
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
        private UploadsController.Bulk bulk;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
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
        [Test]
        public void PostSuccessful()
        {
            this.SetupServicesRunning();

            var result = this.controller.Post(this.bulk);
            result.Wait();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<ObjectResult>(result.Result);

            var obj = result.Result as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.Created, obj.StatusCode);

            Assert.IsInstanceOf<Dictionary<string, List<string>>>(obj.Value);
            var ret = obj.Value as Dictionary<string, List<string>>;
            Assert.AreEqual(
                this.bulk.Materials.Count + this.bulk.Packs.Count + this.bulk.Plans.Count,
                ret["pass"].Count);
            Assert.AreEqual(
                0,
                ret["fail"].Count);
        }

        /// <summary> (Unit Test Method) posts no data. </summary>
        [Test]
        public void PostNoData()
        {
            var result = this.controller.Post(null);
            result.Wait();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (result.Result as BadRequestResult).StatusCode);
        }

        /// <summary> (Unit Test Method) post disconnected. </summary>
        [Test]
        public void PostDisconnected()
        {
            var result = this.controller.Post(this.bulk);
            result.Wait();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<ObjectResult>(result.Result);

            var obj = result.Result as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.Conflict, obj.StatusCode);

            Assert.IsInstanceOf<Dictionary<string, List<string>>>(obj.Value);
            var ret = obj.Value as Dictionary<string, List<string>>;
            Assert.AreEqual(
                0,
                ret["pass"].Count);
            Assert.AreEqual(
                this.bulk.Materials.Count + this.bulk.Packs.Count + this.bulk.Plans.Count,
                ret["fail"].Count);
        }

        /// <summary> Setup the controller as if the services are not running. </summary>
        private void SetupServicesNotRunning()
        {
            this.controller = new(
                Mock.Of<ILogger<UploadsController>>(),
                Options);
            Assert.IsNotNull(this.controller);
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
            Assert.IsNotNull(this.controller);
        }
    }
}
