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
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using PackIt.Controllers;
    using PackIt.Models;

    /// <summary> (Unit Test Fixture) a controller for handling test plans. </summary>
    [TestFixture]
    public class TestUploadsController
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
        private static readonly OptionsWrapper<AppSettings> Options = new OptionsWrapper<AppSettings>(AppSettings);

        /// <summary> The controller logger. </summary>
        private ILogger<UploadsController> logger;

        /// <summary> The controller under test. </summary>
        private UploadsController controller;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.SetupServicesNotRunning();
        }

        /// <summary> (Unit Test Method) post successful. </summary>
        [Test]
        public void PostSuccessful()
        {
            this.SetupServicesRunning();

            string text = File.ReadAllText("Controllers/TestData/uploadsPass.json");
            UploadsController.Bulk bulk = JsonConvert.DeserializeObject<UploadsController.Bulk>(text);

            // make sure IDs are unique
            foreach (Plan.Plan item in bulk.Plans)
            {
                item.PlanId = Guid.NewGuid().ToString();
            }

            foreach (Material.Material item in bulk.Materials)
            {
                item.MaterialId = Guid.NewGuid().ToString();
            }

            foreach (Pack.Pack item in bulk.Packs)
            {
                item.PackId = Guid.NewGuid().ToString();
            }

            var result = this.controller.Post(bulk);
            result.Wait();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<ObjectResult>(result.Result);

            ObjectResult obj = (ObjectResult)result.Result;
            Assert.AreEqual((int)HttpStatusCode.Created, obj.StatusCode);

            Assert.IsInstanceOf<List<string>>(obj.Value);
            List<string> added = (List<string>)obj.Value;
            Assert.AreEqual(
                bulk.Materials.Count + bulk.Packs.Count + bulk.Plans.Count,
                added.Count);
        }

        /// <summary> (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            var result = this.controller.Post(null);
            result.Wait();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result.Result).StatusCode);
        }

        /// <summary> Setup the controller as if the services are not running. </summary>
        private void SetupServicesNotRunning()
        {
            this.logger = Mock.Of<ILogger<UploadsController>>();
            this.controller = new UploadsController(this.logger, Options);
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

            this.logger = Mock.Of<ILogger<UploadsController>>();
            this.controller = new UploadsController(this.logger, Options, handler.Object);

            Assert.IsNotNull(this.controller);
        }
    }
}
