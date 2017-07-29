//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Net;

using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using packer_strategy.Controllers;
using packer_strategy.Models;

namespace packer_strategy_test
{
    [TestFixture]
    public class TestStrategiesController
    {
        [Test]
        public void Create()
        {
            var repStub = new Mock<IStrategyRepository>();
            var controller = new StrategiesController(repStub.Object);
            Assert.IsNotNull(controller);
        }

        [Test]
        public void Post()
        {
            var repStub = new Mock<IStrategyRepository>();
            var controller = new StrategiesController(repStub.Object);
            var strategy = new Strategy();
            var result = controller.Post(strategy);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
        }

        [Test]
        public void PostBad()
        {
            var repStub = new Mock<IStrategyRepository>();
            var controller = new StrategiesController(repStub.Object);
            var result = controller.Post(null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }
    }
}
