// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.App.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using NUnit.Framework;
    using PackItUI.Areas.App.Controllers;
    using PackItUI.Areas.App.Models;
    using PackItUI.Areas.Materials.DTO;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Plans.DTO;
    using PackItUI.Areas.Uploads.DTO;

    /// <summary>   (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestHomeController
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

        /// <summary>   The controller under test. </summary>
        private HomeController controller;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.SetupDisconnected();
        }

        /// <summary> (Unit Test Method) index action. </summary>
        [Test]
        public void Index()
        {
            var result = this.controller.Index();
            Assert.IsInstanceOf<ViewResult>(result);

            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual("Index", viewResult.ViewName);
            Assert.IsNull(viewResult.ViewData.Model);
        }

        /// <summary> (Unit Test Method) about action when the services are not running. </summary>
        [Test]
        public void AboutServicesNotRunning()
        {
            var result = this.controller.About();
            result.Wait();
            Assert.IsInstanceOf<ViewResult>(result.Result);

            ViewResult viewResult = (ViewResult)result.Result;
            Assert.AreEqual("About", viewResult.ViewName);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<AboutViewModel>(viewResult.ViewData.Model);

            AboutViewModel model = (AboutViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Unknown", model.Services["Materials"].Version);
            Assert.AreEqual("Unknown", model.Services["Packs"].Version);
            Assert.AreEqual("Unknown", model.Services["Plans"].Version);
            Assert.AreEqual("Unknown", model.Services["Uploads"].Version);
            Assert.AreEqual("Service down!", model.Services["Materials"].About);
            Assert.AreEqual("Service down!", model.Services["Packs"].About);
            Assert.AreEqual("Service down!", model.Services["Plans"].About);
            Assert.AreEqual("Service down!", model.Services["Uploads"].About);
        }

        /// <summary> (Unit Test Method) about action when the services are running. </summary>
        [Test]
        public void AboutServicesRunning()
        {
            this.SetupConnected();

            var result = this.controller.About();
            result.Wait();
            Assert.IsInstanceOf<ViewResult>(result.Result);

            ViewResult viewResult = (ViewResult)result.Result;
            Assert.AreEqual("About", viewResult.ViewName);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<AboutViewModel>(viewResult.ViewData.Model);

            AboutViewModel model = (AboutViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("1", model.Services["Materials"].Version);
            Assert.AreEqual("1", model.Services["Packs"].Version);
            Assert.AreEqual("1", model.Services["Plans"].Version);
            Assert.AreEqual("1", model.Services["Uploads"].Version);
            Assert.AreEqual("Mock", model.Services["Materials"].About);
            Assert.AreEqual("Mock", model.Services["Packs"].About);
            Assert.AreEqual("Mock", model.Services["Plans"].About);
            Assert.AreEqual("Mock", model.Services["Uploads"].About);
        }

        /// <summary> (Unit Test Method) contact action. </summary>
        [Test]
        public void Contact()
        {
            var result = this.controller.Contact();
            Assert.IsInstanceOf<ViewResult>(result);

            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual("Contact", viewResult.ViewName);
            Assert.IsNull(viewResult.ViewData.Model);
        }

        /// <summary> (Unit Test Method) error action. </summary>
        [Test]
        public void Error()
        {
            var result = this.controller.Error();
            Assert.IsInstanceOf<ViewResult>(result);

            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual("Error", viewResult.ViewName);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<ErrorViewModel>(viewResult.ViewData.Model);
        }

        /// <summary> Setup for disconnected services. </summary>
        private void SetupDisconnected()
        {
            this.controller = new HomeController(
                new MaterialHandler(Options, TimeOut),
                new PackHandler(Options, TimeOut),
                new PlanHandler(Options, TimeOut),
                new UploadHandler(Options, TimeOut))
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            Assert.IsNotNull(this.controller);
        }

        /// <summary> Setup for connected services. </summary>
        private void SetupConnected()
        {
            string testContent = "{'Version': '1', 'About': 'Mock'}";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(testContent)
                }));

            this.controller = new HomeController(
                new MaterialHandler(Options, mockMessageHandler.Object),
                new PackHandler(Options, mockMessageHandler.Object),
                new PlanHandler(Options, mockMessageHandler.Object),
                new UploadHandler(Options, mockMessageHandler.Object))
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            Assert.IsNotNull(this.controller);
        }
    }
}
