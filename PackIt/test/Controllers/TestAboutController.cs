// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using PackIt.Controllers;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestAboutController
    {
        /// <summary> The controller logger. </summary>
        private ILogger<AboutController> logger;

        /// <summary> The controller under test. </summary>
        private AboutController controller;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.logger = Mock.Of<ILogger<AboutController>>();
            this.controller = new AboutController(this.logger);
            Assert.IsNotNull(this.controller);
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            var result = this.controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<AboutController.AboutService>(objectResult.Value);

            var item = (AboutController.AboutService)objectResult.Value;
            Assert.IsNotEmpty(item.Version);
            Assert.IsNotEmpty(item.About);
        }
    }
}
