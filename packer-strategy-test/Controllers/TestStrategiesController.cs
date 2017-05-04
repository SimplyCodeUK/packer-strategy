using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using packer_strategy.Controllers;
using packer_strategy.Models;

namespace packer_strategy_test
{
    [TestClass]
    public class TestStrategiesController
    {
        [TestMethod]
        public void Create()
        {
            var repStub = new Mock<IStrategyRepository>();
            var controller = new StrategiesController(repStub.Object);
            Assert.IsNotNull(controller);
        }
    }
}
