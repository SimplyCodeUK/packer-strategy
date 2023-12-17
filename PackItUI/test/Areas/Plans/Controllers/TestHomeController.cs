// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.Plans.Controllers
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using NUnit.Framework;
    using PackIt.Models;
    using PackItMock.HttpMock;
    using PackItUI.Areas.Plans.Controllers;
    using PackItUI.Areas.Plans.DTO;
    using PackItUI.Areas.Plans.Models;

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
            this.SetupConnected();
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Test]
        public void IndexDisconnected()
        {
            this.SetupDisconnected();

            var result = this.controller.Index();
            result.Wait();
            Assert.That(result.Result, Is.TypeOf<ViewResult>());

            var viewResult = result.Result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Index"));
            Assert.That(viewResult.ViewData.Model, Is.Not.Null);
            Assert.That(viewResult.ViewData.Model, Is.TypeOf<HomeViewModel>());

            var model = viewResult.ViewData.Model as HomeViewModel;
            Assert.That(model.Information.Version, Is.EqualTo("Unknown"));
            Assert.That(model.Information.About, Is.EqualTo("Service down! http://localhost:8003/api/v1/"));
            Assert.That(model.Items, Is.Null);
        }

        /// <summary> (Unit Test Method) index action when the service is up. </summary>
        [Test]
        public void IndexConnected()
        {
            var result = this.controller.Index();
            result.Wait();
            Assert.That(result.Result, Is.InstanceOf<ViewResult>());

            var viewResult = result.Result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Index"));
            Assert.That(viewResult.ViewData.Model, Is.Not.Null);
            Assert.That(viewResult.ViewData.Model, Is.TypeOf<HomeViewModel>());

            var model = viewResult.ViewData.Model as HomeViewModel;
            Assert.That(model.Information.Version, Is.EqualTo("1"));
            Assert.That(model.Information.About, Is.EqualTo("Plans"));
            Assert.That(model.Items.Count, Is.EqualTo(2));
            Assert.That(model.Items[0].PlanId, Is.EqualTo("Id1"));
            Assert.That(model.Items[1].PlanId, Is.EqualTo("Id2"));
        }

        /// <summary> (Unit Test Method) create get action. </summary>
        [Test]
        public void CreateGet()
        {
            var result = this.controller.Create();
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
            Assert.That(viewResult.ViewData.Model, Is.Not.Null);
            Assert.That(viewResult.ViewData.Model, Is.TypeOf<PlanEditViewModel>());
        }

        /// <summary> (Unit Test Method) create post action. </summary>
        [Test]
        public void CreatePost()
        {
            var model = new PlanEditViewModel();
            var result = this.controller.Create(model);
            Assert.That(result.Result, Is.TypeOf<RedirectToActionResult>());

            var redirectResult = result.Result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
        }

        /// <summary> (Unit Test Method) create post action when disconnected. </summary>
        [Test]
        public void CreatePostDisconnected()
        {
            this.SetupDisconnected();

            var model = new PlanEditViewModel();
            var result = this.controller.Create(model);
            Assert.That(result.Result, Is.TypeOf<ViewResult>());

            var viewResult = result.Result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
        }

        /// <summary> (Unit Test Method) update get action. </summary>
        [Test]
        public void UpdateGet()
        {
            var result = this.controller.Update("Id1");
            Assert.That(result.Result, Is.TypeOf<ViewResult>());

            var viewResult = result.Result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Update"));
            Assert.That(viewResult.ViewData.Model, Is.Not.Null);
            Assert.That(viewResult.ViewData.Model, Is.TypeOf<PlanEditViewModel>());

            var viewModel = viewResult.ViewData.Model as PlanEditViewModel;
            Assert.That(viewModel.Data.PlanId, Is.EqualTo("Id1"));
        }

        /// <summary> (Unit Test Method) update post action. </summary>
        [Test]
        public void UpdatePost()
        {
            var model = new PlanEditViewModel();
            var result = this.controller.Update("Id1", model);
            Assert.That(result.Result, Is.TypeOf<RedirectToActionResult>());

            var redirectResult = result.Result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
        }

        /// <summary> (Unit Test Method) update post action when disconnected. </summary>
        [Test]
        public void UpdatePostDisconnected()
        {
            this.SetupDisconnected();

            var model = new PlanEditViewModel();
            var result = this.controller.Update("Id1", model);
            Assert.That(result.Result, Is.TypeOf<ViewResult>());

            var viewResult = result.Result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Update"));
        }

        /// <summary> (Unit Test Method) delete get action. </summary>
        [Test]
        public void DeleteGet()
        {
            var result = this.controller.Delete("Id1");
            Assert.That(result.Result, Is.TypeOf<ViewResult>());

            var viewResult = result.Result as ViewResult;
            Assert.That(viewResult.ViewName, Is.EqualTo("Delete"));
            Assert.That(viewResult.ViewData.Model, Is.Not.Null);
            Assert.That(viewResult.ViewData.Model, Is.TypeOf<PlanEditViewModel>());

            var viewModel = viewResult.ViewData.Model as PlanEditViewModel;
            Assert.That(viewModel.Data.PlanId, Is.EqualTo("Id1"));
        }

        /// <summary> (Unit Test Method) delete post action. </summary>
        [Test]
        public void DeletePost()
        {
            var result = this.controller.DoDelete("Id1");
            Assert.That(result.Result, Is.TypeOf<RedirectToActionResult>());

            var redirectResult = result.Result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
        }

        /// <summary> (Unit Test Method) delete post action when disconnected. </summary>
        [Test]
        public void DeletePostDisconnected()
        {
            this.SetupDisconnected();

            var result = this.controller.DoDelete("Id1");
            Assert.That(result.Result, Is.TypeOf<RedirectToActionResult>());

            var redirectResult = result.Result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
        }

        /// <summary> Setup for disconnected service. </summary>
        private void SetupDisconnected()
        {
            var httpHandler = new MockHttpClientHandler();
            this.controller = new(
                Mock.Of<ILogger<HomeController>>(),
                new PlanHandler(Options, httpHandler)
                {
                    TimeOut = TimeOut
                });
            Assert.That(this.controller, Is.Not.Null);
        }

        /// <summary> Setup for connected services. </summary>
        private void SetupConnected()
        {
            var root = Endpoints.Plans;
            var httpHandler = new MockHttpClientHandler();
            httpHandler
                .AddRequest(HttpMethod.Get, root)
                .ContentsJson("{\"Version\": \"1\", \"About\": \"Plans\"}");
            httpHandler
                .AddRequest(HttpMethod.Get, root + "Plans")
                .ContentsJson("[{\"PlanId\" : \"Id1\"}, {\"PlanId\" : \"Id2\"}]");
            httpHandler
                .AddRequest(HttpMethod.Post, root + "Plans");
            httpHandler
                .AddRequest(HttpMethod.Get, root + "Plans/Id1")
                .ContentsJson("{\"PlanId\" : \"Id1\"}");
            httpHandler
                .AddRequest(HttpMethod.Put, root + "Plans/Id1");
            httpHandler
                .AddRequest(HttpMethod.Delete, root + "Plans/Id1");

            this.controller = new(
                Mock.Of<ILogger<HomeController>>(),
                new PlanHandler(Options, httpHandler)
                {
                    TimeOut = TimeOut
                });
            Assert.That(this.controller, Is.Not.Null);
        }
    }
}
