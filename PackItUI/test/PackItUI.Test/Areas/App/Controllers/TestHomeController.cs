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
        /// <summary>   The controller under test. </summary>
        private HomeController controller;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            ServiceEndpoints endpoints = new ServiceEndpoints
            {
                Materials = "http://localhost:8001/api/v1/",
                Packs = "http://localhost:8002/api/v1/",
                Plans = "http://localhost:8003/api/v1/"
            };
            AppSettings appSettings = new AppSettings
            {
                ServiceEndpoints = endpoints
            };

            var options = new OptionsWrapper<AppSettings>(appSettings);

            this.controller = new HomeController(options);
            Assert.IsNotNull(this.controller);

            this.controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        /// <summary> (Unit Test Method) index action. </summary>
        [Test]
        public void Index()
        {
            var result = this.controller.Index();
            Assert.IsInstanceOf<ViewResult>(result);
        }

        /// <summary> (Unit Test Method) about action. </summary>
        [Test]
        public void About()
        {
            var result = this.controller.About();
            result.Wait();
            Assert.IsInstanceOf<ViewResult>(result.Result);
        }

        /// <summary> (Unit Test Method) contact action. </summary>
        [Test]
        public void Contact()
        {
            var result = this.controller.Contact();
            Assert.IsInstanceOf<ViewResult>(result);
        }

        /// <summary> (Unit Test Method) error action. </summary>
        [Test]
        public void Error()
        {
            var result = this.controller.Error();
            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
