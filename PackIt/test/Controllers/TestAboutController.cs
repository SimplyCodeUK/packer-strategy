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
    using Xunit;
    using PackIt.Controllers;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    public class TestAboutController
    {
        /// <summary> The controller under test. </summary>
        private readonly AboutController controller;

        /// <summary> Setup for all unit tests here. </summary>
        public TestAboutController()
        {
            this.controller = new(
                Mock.Of<ILogger<AboutController>>());
            Assert.NotNull(this.controller);
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Fact]
        public void Get()
        {
            var result = this.controller.Get();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.IsType<AboutController.AboutService> (objectResult.Value);

            var item = objectResult.Value as AboutController.AboutService;
            Assert.NotEmpty(item.Version);
            Assert.NotEmpty(item.About);
        }
    }
}
