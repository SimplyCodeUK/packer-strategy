// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.Packs.Controllers
{
    using System;
    using System.Text.Json;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;
    using PackIt.Models;
    using PackItMock.HttpMock;
    using PackItUI.Areas.Packs.Controllers;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Packs.Models;

    /// <summary> (Unit Test Fixture) a controller for handling test packs. </summary>
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
            this.SetupConnected();
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Fact]
        public async Task IndexDisconnected()
        {
            this.SetupDisconnected();

            var result = await this.controller.Index();
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("Index", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<HomeViewModel>(viewResult.ViewData.Model);

            var model = viewResult.ViewData.Model as HomeViewModel;
            Assert.Equal("Unknown", model.Information.Version);
            Assert.Equal("Service down! http://localhost:8002/api/v1/", model.Information.About);
            Assert.Null(model.Items);
        }

        /// <summary> (Unit Test Method) index action when the service is up. </summary>
        [Fact]
        public async Task IndexConnected()
        {
            var result = await this.controller.Index();
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("Index", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<HomeViewModel>(viewResult.ViewData.Model);

            var model = viewResult.ViewData.Model as HomeViewModel;
            Assert.Equal("1", model.Information.Version);
            Assert.Equal("Packs", model.Information.About);
            Assert.Equal(2, model.Items.Count);
            Assert.Equal("Id1", model.Items[0].PackId);
            Assert.Equal("Id2", model.Items[1].PackId);
        }

        /// <summary> (Unit Test Method) create get action. </summary>
        [Fact]
        public void CreateGet()
        {
            var result = this.controller.Create();
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("Create", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<PackEditViewModel>(viewResult.ViewData.Model);
        }

        /// <summary> (Unit Test Method) create post action. </summary>
        [Fact]
        public async Task CreatePost()
        {
            var model = new PackEditViewModel();
            var result = await this.controller.Create(model);
            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) create post action when disconnected. </summary>
        [Fact]
        public async Task CreatePostDisconnected()
        {
            this.SetupDisconnected();

            var model = new PackEditViewModel();
            var result = await this.controller.Create(model);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("Create", viewResult.ViewName);
        }

        /// <summary> (Unit Test Method) update get action. </summary>
        [Fact]
        public async Task UpdateGet()
        {
            var result = await this.controller.Update("Id1");
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("Update", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<PackEditViewModel>(viewResult.ViewData.Model);

            var viewModel = viewResult.ViewData.Model as PackEditViewModel;
            Assert.Equal("Id1", viewModel.Data.PackId);
        }

        /// <summary> (Unit Test Method) update post action. </summary>
        [Fact]
        public async Task UpdatePost()
        {
            var model = new PackEditViewModel();
            var result = await this.controller.Update("Id1", model);
            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) update post action when disconnected. </summary>
        [Fact]
        public async Task UpdatePostDisconnected()
        {
            this.SetupDisconnected();

            var model = new PackEditViewModel();
            var result = await this.controller.Update("Id1", model);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("Update", viewResult.ViewName);
        }

        /// <summary> (Unit Test Method) delete get action. </summary>
        [Fact]
        public async Task DeleteGet()
        {
            var result = await this.controller.Delete("Id1");
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("Delete", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<PackEditViewModel>(viewResult.ViewData.Model);

            var viewModel = viewResult.ViewData.Model as PackEditViewModel;
            Assert.Equal("Id1", viewModel.Data.PackId);
        }

        /// <summary> (Unit Test Method) delete post action. </summary>
        [Fact]
        public async Task DeletePost()
        {
            var result = await this.controller.DoDelete("Id1");
            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) delete post action when disconnected. </summary>
        [Fact]
        public async Task DeletePostDisconnected()
        {
            this.SetupDisconnected();

            var result = await this.controller.DoDelete("Id1");
            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) display get action. </summary>
        [Fact]
        public async Task DisplayGet()
        {
            var result = await this.controller.Display("Id1");
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("Display", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<PackDisplayViewModel>(viewResult.ViewData.Model);

            var viewModel = viewResult.ViewData.Model as PackDisplayViewModel;
            Assert.Equal("Id1", viewModel.Pack.PackId);
        }

        /// <summary> (Unit Test Method) update post action. </summary>
        [Fact]
        public async Task DisplayPost()
        {
            var model = new PackEditViewModel();
            var result = await this.controller.Display("Id1", model);
            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) update post action disconnected. </summary>
        [Fact]
        public async Task DisplayPostDisconnected()
        {
            this.SetupDisconnected();

            var model = new PackEditViewModel();
            var result = await this.controller.Display("Id1", model);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("Update", viewResult.ViewName);
        }

        /// <summary> (Unit Test Method) costing row post action. </summary>
        [Fact]
        public void CostingRowPost()
        {
            var body = JsonDocument.Parse("{\"index\": 5}");
            var result = this.controller.CostingRow(body);

            Assert.IsType<PartialViewResult>(result);
            Assert.IsType<PackIt.Pack.Costing>((result as PartialViewResult).Model);
            Assert.Equal("EditorTemplates/Costing", (result as PartialViewResult).ViewName);
        }

        /// <summary> Setup for disconnected service. </summary>
        private void SetupDisconnected()
        {
            var httpHandler = new MockHttpClientHandler();
            this.controller = new(
                Mock.Of<ILogger<HomeController>>(),
                new PackHandler(Options, httpHandler)
                {
                    TimeOut = TimeOut
                },
                new DrawHandler(Options, httpHandler)
                {
                    TimeOut = TimeOut
                });
            Assert.NotNull(this.controller);
        }

        /// <summary> Setup for connected services. </summary>
        private void SetupConnected()
        {
            var rootPacks = Endpoints.Packs;
            var rootDrawings = Endpoints.Drawings;
            var httpHandler = new MockHttpClientHandler();
            httpHandler
                .AddRequest(HttpMethod.Get, rootPacks)
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Packs\"}");
            httpHandler
                .AddRequest(HttpMethod.Get, rootPacks + "Packs")
                .ContentsJson("[{\"PackId\" : \"Id1\"}, {\"PackId\" : \"Id2\"}]");
            httpHandler
                .AddRequest(HttpMethod.Post, rootPacks + "Packs");
            httpHandler
                .AddRequest(HttpMethod.Get, rootPacks + "Packs/Id1")
                .ContentsJson("{\"PackId\" : \"Id1\"}");
            httpHandler
                .AddRequest(HttpMethod.Put, rootPacks + "Packs/Id1");
            httpHandler
                .AddRequest(HttpMethod.Delete, rootPacks + "Packs/Id1");
            httpHandler
                .AddRequest(HttpMethod.Post, rootDrawings + "Drawings")
                .ContentsJson("{\"DrawingId\": \"1111-2222-3333-4444\"}");

            this.controller = new(
                Mock.Of<ILogger<HomeController>>(),
                new PackHandler(Options, httpHandler)
                {
                    TimeOut = TimeOut
                },
                new DrawHandler(Options, httpHandler)
                {
                    TimeOut = TimeOut
                });
            Assert.NotNull(this.controller);
        }
    }
}
