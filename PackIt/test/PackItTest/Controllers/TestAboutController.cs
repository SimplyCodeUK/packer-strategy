// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItTest.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;
    using PackIt.Controllers;

    /// <summary>   (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestAboutController
    {
        /// <summary>   The controller under test. </summary>
        private AboutController controller;

        /// <summary>   Tests before. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.controller = new AboutController();
            Assert.IsNotNull(this.controller);
        }

        /// <summary>   (Unit Test Method) gets this object. </summary>
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
