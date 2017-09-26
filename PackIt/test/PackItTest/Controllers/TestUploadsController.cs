// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItTest.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using PackIt.Controllers;
    using PackIt.DTO;

    /// <summary>   (Unit Test Fixture) a controller for handling test plans. </summary>
    [TestFixture]
    public class TestUploadsController
    {
        /// <summary>   The controller under test. </summary>
        private UploadsController controller;

        /// <summary>   Tests before. </summary>
        [SetUp]
        public void BeforeTest()
        {
            var planBuilder = new DbContextOptionsBuilder<PlanContext>();
            planBuilder.EnableSensitiveDataLogging();
            planBuilder.UseInMemoryDatabase("testplan");

            var materialBuilder = new DbContextOptionsBuilder<MaterialContext>();
            materialBuilder.EnableSensitiveDataLogging();
            materialBuilder.UseInMemoryDatabase("testmaterial");

            var packBuilder = new DbContextOptionsBuilder<PackContext>();
            packBuilder.EnableSensitiveDataLogging();
            packBuilder.UseInMemoryDatabase("testpack");

            var planContext = new PlanContext(planBuilder.Options);
            var planRepo = new PlanRepository(planContext);
            var materialContext = new MaterialContext(materialBuilder.Options);
            var materialRepo = new MaterialRepository(materialContext);
            var packContext = new PackContext(packBuilder.Options);
            var packRepo = new PackRepository(packContext);

            this.controller = new UploadsController(planRepo, materialRepo, packRepo);
            Assert.IsNotNull(this.controller);
        }

        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            string text = File.ReadAllText("Controllers/TestData/uploadsPass.json");
            UploadsController.Bulk bulk = JsonConvert.DeserializeObject<UploadsController.Bulk>(text);

            // make sure IDs are unique
            foreach (PackIt.Plan.Plan item in bulk.Plans)
            {
                item.PlanId = Guid.NewGuid().ToString();
            }

            foreach (PackIt.Material.Material item in bulk.Materials)
            {
                item.MaterialId = Guid.NewGuid().ToString();
            }

            foreach (PackIt.Pack.Pack item in bulk.Packs)
            {
                item.PackId = Guid.NewGuid().ToString();
            }

            var result = this.controller.Post(bulk);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((ObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            var result = this.controller.Post(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }
    }
}
