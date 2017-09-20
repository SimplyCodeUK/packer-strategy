// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItTest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using PackIt.Controllers;
    using PackIt.DTO;
    using PackIt.Helpers.Enums;
    using PackIt.Plan;

    /// <summary>   (Unit Test Fixture) a controller for handling test plans. </summary>
    [TestFixture]
    public class TestPlansController
    {
        /// <summary>   The controller under test. </summary>
        private PlansController controller;

        /// <summary>   Tests before. </summary>
        [SetUp]
        public void BeforeTest()
        {
            DbContextOptionsBuilder<PlanContext> builder = new DbContextOptionsBuilder<PlanContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testplan");

            PlanContext context = new PlanContext(builder.Options);
            PlanRepository repository = new PlanRepository(context);

            this.controller = new PlansController(repository);
            Assert.IsNotNull(this.controller);
        }

        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            Plan item = new Plan { PlanId = Guid.NewGuid().ToString() };
            var result = this.controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
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

        /// <summary>   (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            Plan item = new Plan { PlanId = Guid.NewGuid().ToString() };
            var result = this.controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Conflict, ((StatusCodeResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) gets all. </summary>
        [Test]
        public void GetAll()
        {
            int itemsToAdd = 10;
            List<string> ids = new List<string>();

            for (int item = 0; item < itemsToAdd; ++item)
            {
                string id = Guid.NewGuid().ToString();

                ids.Add(id);
                this.controller.Post(new Plan { PlanId = id });
            }

            IActionResult result = this.controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IEnumerable<Plan>>(objectResult.Value);

            IEnumerable<Plan> items = (IEnumerable<Plan>)objectResult.Value;
            foreach (Plan item in items)
            {
                if (ids.Contains(item.PlanId))
                {
                    ids.Remove(item.PlanId);
                }
            }

            Assert.IsEmpty(ids, "IDS not found " + string.Join(",", ids));
        }

        /// <summary>   (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            string startName = "A name";
            string startNote = "Some notes";
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan { PlanId = id, Name = startName, Notes = startNote };

            this.controller.Post(item);

            var result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);
            Assert.AreEqual(item.Name, startName);
            Assert.AreEqual(item.Notes, startNote);
        }

        /// <summary>   (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) puts this object. </summary>
        [Test]
        public void Put()
        {
            string startName = "A name";
            string putName = "B name";
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan { PlanId = id, Name = startName };

            this.controller.Post(item);

            item.Name = putName;
            var result = this.controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the plan and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);
            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);
            Assert.AreEqual(item.Name, putName);
        }

        /// <summary>   (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan();
            var result = this.controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan { PlanId = id };

            this.controller.Post(item);

            var result = this.controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) patches this object. </summary>
        [Test]
        public void Patch()
        {
            string startName = "A name";
            string patchName = "B name";
            string startNote = "Some notes";
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan { PlanId = id, Name = startName, Notes = startNote };

            // Create a new plan
            this.controller.Post(item);

            // Patch the plan with a new name
            JsonPatchDocument<Plan> patch = new JsonPatchDocument<Plan>();
            patch.Replace(e => e.Name, patchName);

            var result = this.controller.Patch(id, patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);
            Assert.AreEqual(item.Name, patchName);
            Assert.AreEqual(item.Notes, startNote);

            // Get the plan and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);
            Assert.AreEqual(item.Name, patchName);
            Assert.AreEqual(item.Notes, startNote);
        }

        /// <summary>   (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            string startName = "A name";
            string patchName = "B name";
            string startNote = "Some notes";
            Plan item = new Plan { PlanId = Guid.NewGuid().ToString(), Name = startName, Notes = startNote };

            this.controller.Post(item);

            JsonPatchDocument<Plan> patch = new JsonPatchDocument<Plan>();
            patch.Replace(e => e.Name, patchName);

            var result = this.controller.Patch(Guid.NewGuid().ToString(), patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts a complex plan. </summary>
        [Test]
        public void PostComplexPlan()
        {
            string id = Guid.NewGuid().ToString();
            StageLevel level = StageLevel.MultiPack;

            // Create a plan with a stage that has a limit
            Stage stage = new Stage { StageLevel = level };
            stage.Limits.Add(new Limit());
            Plan item = new Plan { PlanId = id };
            item.Stages.Add(stage);

            var result = this.controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            // Get the plan
            result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            // Test the plan
            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);

            // Test for one stage
            Assert.AreEqual(item.Stages.Count, 1);
            Assert.AreEqual(item.Stages[0].StageLevel, level);

            // Test for one limit in the stage
            Assert.AreEqual(item.Stages[0].Limits.Count, 1);
        }
    }
}
