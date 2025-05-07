// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.App.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;
    using PackItLib.Models;
    using PackItMock.HttpMock;
    using PackItUI.Areas.App.Controllers;
    using PackItUI.Areas.App.Models;
    using PackItUI.Areas.Materials.DTO;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Plans.DTO;
    using PackItUI.Areas.Uploads.DTO;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
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
        public TestHomeController()
        {
            this.SetupDisconnected();
        }

        /// <summary> (Unit Test Method) index action. </summary>
        [Fact]
        public void Index()
        {
            var result = this.controller.Index();
            Assert.IsType<ViewResult>(result);

            ViewResult viewResult = result as ViewResult;
            Assert.Equal("Index", viewResult.ViewName);
            Assert.Null(viewResult.ViewData.Model);
        }

        /// <summary> (Unit Test Method) about action when the services are not running. </summary>
        [Fact]
        public async Task AboutServicesNotRunning()
        {
            var result = await this.controller.About();
            Assert.IsType<ViewResult>(result);

            ViewResult viewResult = result as ViewResult;
            Assert.Equal("About", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<AboutViewModel>(viewResult.ViewData.Model);

            AboutViewModel model = viewResult.ViewData.Model as AboutViewModel;
            Assert.Equal("Unknown", model.Services["Materials"].Version);
            Assert.Equal("Unknown", model.Services["Packs"].Version);
            Assert.Equal("Unknown", model.Services["Plans"].Version);
            Assert.Equal("Unknown", model.Services["Uploads"].Version);
            Assert.Equal("Service down! http://localhost:8001/api/v1/", model.Services["Materials"].About);
            Assert.Equal("Service down! http://localhost:8002/api/v1/", model.Services["Packs"].About);
            Assert.Equal("Service down! http://localhost:8003/api/v1/", model.Services["Plans"].About);
            Assert.Equal("Service down! http://localhost:8004/api/v1/", model.Services["Uploads"].About);
        }

        /// <summary> (Unit Test Method) about action when the services are running. </summary>
        [Fact]
        public async Task AboutServicesRunning()
        {
            this.SetupConnected();

            var result = await this.controller.About();
            Assert.IsType<ViewResult>(result);

            ViewResult viewResult = result as ViewResult;
            Assert.Equal("About", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<AboutViewModel>(viewResult.ViewData.Model);

            AboutViewModel model = viewResult.ViewData.Model as AboutViewModel;
            Assert.Equal("1", model.Services["Materials"].Version);
            Assert.Equal("1", model.Services["Packs"].Version);
            Assert.Equal("1", model.Services["Plans"].Version);
            Assert.Equal("1", model.Services["Uploads"].Version);
            Assert.Equal("1", model.Services["Drawings"].Version);

            Assert.Equal("Materials", model.Services["Materials"].About);
            Assert.Equal("Packs", model.Services["Packs"].About);
            Assert.Equal("Plans", model.Services["Plans"].About);
            Assert.Equal("Uploads", model.Services["Uploads"].About);
            Assert.Equal("Draw", model.Services["Drawings"].About);
        }

        /// <summary> (Unit Test Method) contact action. </summary>
        [Fact]
        public void Contact()
        {
            var result = this.controller.Contact();
            Assert.IsType<ViewResult>(result);

            ViewResult viewResult = result as ViewResult;
            Assert.Equal("Contact", viewResult.ViewName);
            Assert.Null(viewResult.ViewData.Model);
        }

        /// <summary> (Unit Test Method) error action. </summary>
        [Fact]
        public void Error()
        {
            var result = this.controller.Error();
            Assert.IsType<ViewResult>(result);

            ViewResult viewResult = result as ViewResult;
            Assert.Equal("Error", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<ErrorViewModel>(viewResult.ViewData.Model);
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
            Assert.NotNull(this.controller);
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
            Assert.NotNull(this.controller);
        }
    }
}
