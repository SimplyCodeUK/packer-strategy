// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.App.Controllers
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using NUnit.Framework;
    using PackIt.Models;
    using PackItMock.HttpMock;
    using PackItUI.Areas.App.Controllers;
    using PackItUI.Areas.App.Models;
    using PackItUI.Areas.Materials.DTO;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Plans.DTO;
    using PackItUI.Areas.Uploads.DTO;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestHomeController
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

        /// <summary> The controller under test. </summary>
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
            Assert.That(result, Is.TypeOf<ViewResult>());

            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Index"));
            Assert.That(viewResult.ViewData.Model, Is.Null);
        }

        /// <summary> (Unit Test Method) about action when the services are not running. </summary>
        [Test]
        public void AboutServicesNotRunning()
        {
            var result = this.controller.About();
            result.Wait();
            Assert.That(result.Result, Is.TypeOf<ViewResult>());

            ViewResult viewResult = result.Result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("About"));
            Assert.That(viewResult.ViewData.Model, Is.Not.Null);
            Assert.That(viewResult.ViewData.Model, Is.TypeOf<AboutViewModel>());

            AboutViewModel model = viewResult.ViewData.Model as AboutViewModel;
            Assert.That(model.Services["Materials"].Version, Is.EqualTo("Unknown"));
            Assert.That(model.Services["Packs"].Version, Is.EqualTo("Unknown"));
            Assert.That(model.Services["Plans"].Version, Is.EqualTo("Unknown"));
            Assert.That(model.Services["Uploads"].Version, Is.EqualTo("Unknown"));
            Assert.That(model.Services["Materials"].About, Is.EqualTo("Service down! http://localhost:8001/api/v1/"));
            Assert.That(model.Services["Packs"].About, Is.EqualTo("Service down! http://localhost:8002/api/v1/"));
            Assert.That(model.Services["Plans"].About, Is.EqualTo("Service down! http://localhost:8003/api/v1/"));
            Assert.That(model.Services["Uploads"].About, Is.EqualTo("Service down! http://localhost:8004/api/v1/"));
        }

        /// <summary> (Unit Test Method) about action when the services are running. </summary>
        [Test]
        public void AboutServicesRunning()
        {
            this.SetupConnected();

            var result = this.controller.About();
            result.Wait();
            Assert.That(result.Result, Is.TypeOf<ViewResult>());

            ViewResult viewResult = result.Result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("About"));
            Assert.That(viewResult.ViewData.Model, Is.Not.Null);
            Assert.That(viewResult.ViewData.Model, Is.TypeOf<AboutViewModel>());

            AboutViewModel model = viewResult.ViewData.Model as AboutViewModel;
            Assert.That(model.Services["Materials"].Version, Is.EqualTo("1"));
            Assert.That(model.Services["Packs"].Version, Is.EqualTo("1"));
            Assert.That(model.Services["Plans"].Version, Is.EqualTo("1"));
            Assert.That(model.Services["Uploads"].Version, Is.EqualTo("1"));
            Assert.That(model.Services["Drawings"].Version, Is.EqualTo("1"));

            Assert.That(model.Services["Materials"].About, Is.EqualTo("Materials"));
            Assert.That(model.Services["Packs"].About, Is.EqualTo("Packs"));
            Assert.That(model.Services["Plans"].About, Is.EqualTo("Plans"));
            Assert.That(model.Services["Uploads"].About, Is.EqualTo("Uploads"));
            Assert.That(model.Services["Drawings"].About, Is.EqualTo("Draw"));
        }

        /// <summary> (Unit Test Method) contact action. </summary>
        [Test]
        public void Contact()
        {
            var result = this.controller.Contact();
            Assert.That(result, Is.TypeOf<ViewResult>());

            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Contact"));
            Assert.That(viewResult.ViewData.Model, Is.Null);
        }

        /// <summary> (Unit Test Method) error action. </summary>
        [Test]
        public void Error()
        {
            var result = this.controller.Error();
            Assert.That(result, Is.TypeOf<ViewResult>());

            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Error"));
            Assert.That(viewResult.ViewData.Model, Is.Not.Null);
            Assert.That(viewResult.ViewData.Model, Is.TypeOf<ErrorViewModel>());
        }

        /// <summary> Setup for disconnected services. </summary>
        private void SetupDisconnected()
        {
            this.controller = new(
                Mock.Of<ILogger<HomeController>>(),
                new MaterialHandler(Options)
                {
                    TimeOut = TimeOut
                },
                new PackHandler(Options)
                {
                    TimeOut = TimeOut
                },
                new PlanHandler(Options)
                {
                    TimeOut = TimeOut
                },
                new UploadHandler(Options)
                {
                    TimeOut = TimeOut
                },
                new DrawHandler(Options)
                {
                    TimeOut = TimeOut
                })
            {
                ControllerContext = new()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            Assert.That(this.controller, Is.Not.Null);
        }

        /// <summary> Setup for connected services. </summary>
        private void SetupConnected()
        {
            var httpHandler = new MockHttpClientHandler();
            httpHandler
                .AddRequest(HttpMethod.Get, "http://localhost:8001/api/v1/")
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Materials\"}");
            httpHandler
                .AddRequest(HttpMethod.Get, "http://localhost:8002/api/v1/")
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Packs\"}");
            httpHandler
                .AddRequest(HttpMethod.Get, "http://localhost:8003/api/v1/")
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Plans\"}");
            httpHandler
                .AddRequest(HttpMethod.Get, "http://localhost:8004/api/v1/")
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Uploads\"}");
            httpHandler
                .AddRequest(HttpMethod.Get, "http://localhost:8005/api/v1/")
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Draw\"}");

            this.controller = new(
                Mock.Of<ILogger<HomeController>>(),
                new MaterialHandler(Options, httpHandler),
                new PackHandler(Options, httpHandler),
                new PlanHandler(Options, httpHandler),
                new UploadHandler(Options, httpHandler),
                new DrawHandler(Options, httpHandler))
            {
                ControllerContext = new()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            Assert.That(this.controller, Is.Not.Null);
        }
    }
}
