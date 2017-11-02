// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.App.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using NUnit.Framework;
    using PackItUI.Areas.App.Controllers;
    using PackItUI.Areas.App.Models;

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
        private static readonly OptionsWrapper<AppSettings> Options = new OptionsWrapper<AppSettings>(AppSettings);

        /// <summary>   The controller under test. </summary>
        private HomeController controller;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.controller = new HomeController(Options)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            Assert.IsNotNull(this.controller);
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
    }
}
