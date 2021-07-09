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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using NUnit.Framework;
    using PackIt.Models;
    using PackItUI.Areas.Packs.Controllers;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Packs.Models;
    using PackItUI.Test.HttpMock;

    /// <summary> (Unit Test Fixture) a controller for handling test packs. </summary>
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
            Drawings = "http://localhost:5000/api/v1/"
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
            this.SetupConnected();
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Test]
        public void IndexDisconnected()
        {
            this.SetupDisconnected();

            var result = this.controller.Index();
            result.Wait();
            Assert.IsInstanceOf<ViewResult>(result.Result);

            var viewResult = result.Result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<HomeViewModel>(viewResult.ViewData.Model);

            var model = viewResult.ViewData.Model as HomeViewModel;
            Assert.AreEqual("Unknown", model.Information.Version);
            Assert.AreEqual("Service down! http://localhost:8002/api/v1/", model.Information.About);
            Assert.IsNull(model.Items);
        }

        /// <summary> (Unit Test Method) index action when the service is up. </summary>
        [Test]
        public void IndexConnected()
        {
            var result = this.controller.Index();
            result.Wait();
            Assert.IsInstanceOf<ViewResult>(result.Result);

            var viewResult = result.Result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<HomeViewModel>(viewResult.ViewData.Model);

            var model = viewResult.ViewData.Model as HomeViewModel;
            Assert.AreEqual("1", model.Information.Version);
            Assert.AreEqual("Packs", model.Information.About);
            Assert.AreEqual(2, model.Items.Count);
            Assert.AreEqual("Id1", model.Items[0].PackId);
            Assert.AreEqual("Id2", model.Items[1].PackId);
        }

        /// <summary> (Unit Test Method) create get action. </summary>
        [Test]
        public void CreateGet()
        {
            var result = this.controller.Create();
            Assert.IsInstanceOf<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<PackEditViewModel>(viewResult.ViewData.Model);
        }

        /// <summary> (Unit Test Method) create post action. </summary>
        [Test]
        public void CreatePost()
        {
            var model = new PackEditViewModel();
            var result = this.controller.Create(model);
            Assert.IsInstanceOf<RedirectToActionResult>(result.Result);

            var redirectResult = result.Result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) create post action when disconnected. </summary>
        [Test]
        public void CreatePostDisconnected()
        {
            this.SetupDisconnected();

            var model = new PackEditViewModel();
            var result = this.controller.Create(model);
            Assert.IsInstanceOf<ViewResult>(result.Result);

            var viewResult = result.Result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        /// <summary> (Unit Test Method) update get action. </summary>
        [Test]
        public void UpdateGet()
        {
            var result = this.controller.Update("Id1");
            Assert.IsInstanceOf<ViewResult>(result.Result);

            var viewResult = result.Result as ViewResult;
            Assert.AreEqual("Update", viewResult.ViewName);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<PackEditViewModel>(viewResult.ViewData.Model);

            var viewModel = viewResult.ViewData.Model as PackEditViewModel;
            Assert.AreEqual("Id1", viewModel.Data.PackId);
        }

        /// <summary> (Unit Test Method) update post action. </summary>
        [Test]
        public void UpdatePost()
        {
            var model = new PackEditViewModel();
            var result = this.controller.Update("Id1", model);
            Assert.IsInstanceOf<RedirectToActionResult>(result.Result);

            var redirectResult = result.Result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) update post action when disconnected. </summary>
        [Test]
        public void UpdatePostDisconnected()
        {
            this.SetupDisconnected();

            var model = new PackEditViewModel();
            var result = this.controller.Update("Id1", model);
            Assert.IsInstanceOf<ViewResult>(result.Result);

            var viewResult = result.Result as ViewResult;
            Assert.AreEqual("Update", viewResult.ViewName);
        }

        /// <summary> (Unit Test Method) delete get action. </summary>
        [Test]
        public void DeleteGet()
        {
            var result = this.controller.Delete("Id1");
            Assert.IsInstanceOf<ViewResult>(result.Result);

            var viewResult = result.Result as ViewResult;
            Assert.AreEqual("Delete", viewResult.ViewName);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<PackEditViewModel>(viewResult.ViewData.Model);

            var viewModel = viewResult.ViewData.Model as PackEditViewModel;
            Assert.AreEqual("Id1", viewModel.Data.PackId);
        }

        /// <summary> (Unit Test Method) delete post action. </summary>
        [Test]
        public void DeletePost()
        {
            var result = this.controller.DoDelete("Id1");
            Assert.IsInstanceOf<RedirectToActionResult>(result.Result);

            var redirectResult = result.Result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) delete post action when disconnected. </summary>
        [Test]
        public void DeletePostDisconnected()
        {
            this.SetupDisconnected();

            var result = this.controller.DoDelete("Id1");
            Assert.IsInstanceOf<RedirectToActionResult>(result.Result);

            var redirectResult = result.Result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) display get action. </summary>
        [Test]
        public void DisplayGet()
        {
            var result = this.controller.Display("Id1");
            Assert.IsInstanceOf<ViewResult>(result.Result);

            var viewResult = result.Result as ViewResult;
            Assert.AreEqual("Display", viewResult.ViewName);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<PackDisplayViewModel>(viewResult.ViewData.Model);

            var viewModel = viewResult.ViewData.Model as PackDisplayViewModel;
            Assert.AreEqual("Id1", viewModel.Pack.PackId);
        }

        /// <summary> (Unit Test Method) update post action. </summary>
        [Test]
        public void DisplayPost()
        {
            var model = new PackEditViewModel();
            var result = this.controller.Display("Id1", model);
            Assert.IsInstanceOf<RedirectToActionResult>(result.Result);

            var redirectResult = result.Result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        /// <summary> (Unit Test Method) update post action disconnected. </summary>
        [Test]
        public void DisplayPostDisconnected()
        {
            this.SetupDisconnected();

            var model = new PackEditViewModel();
            var result = this.controller.Display("Id1", model);
            Assert.IsInstanceOf<ViewResult>(result.Result);

            var viewResult = result.Result as ViewResult;
            Assert.AreEqual("Update", viewResult.ViewName);
        }

        /// <summary> (Unit Test Method) costing row post action. </summary>
        [Test]
        public void CostingRowPost()
        {
            var body = JsonDocument.Parse("{\"index\": 5}");
            var result = this.controller.CostingRow(body);

            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.IsInstanceOf<PackIt.Pack.Costing>((result as PartialViewResult).Model);
            Assert.AreEqual("EditorTemplates/Costing", (result as PartialViewResult).ViewName);
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
            Assert.IsNotNull(this.controller);
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
            Assert.IsNotNull(this.controller);
        }
    }
}
