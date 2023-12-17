// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Test.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using PackItDraw.Controllers;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestAboutController
    {
        /// <summary> The controller under test. </summary>
        private AboutController controller;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.controller = new(
                Mock.Of<ILogger<AboutController>>());
            Assert.That(this.controller, Is.Not.Null);
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            var result = this.controller.Get();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<AboutController.AboutService>());

            var item = objectResult.Value as AboutController.AboutService;
            Assert.That(item.Version, Is.Not.Empty);
            Assert.That(item.About, Is.Not.Empty);
        }
    }
}
