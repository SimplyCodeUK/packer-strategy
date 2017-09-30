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
            var builder = new DbContextOptionsBuilder<PlanContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testplan");

            var context = new PlanContext(builder.Options);
            var repository = new PlanRepository(context);

            this.controller = new PlansController(repository);
            Assert.IsNotNull(this.controller);
        }

        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            var item = new Plan { PlanId = Guid.NewGuid().ToString() };
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
            var item = new Plan { PlanId = Guid.NewGuid().ToString() };
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
            const int ItemsToAdd = 10;
            var ids = new List<string>();

            for (int item = 0; item < ItemsToAdd; ++item)
            {
                string id = Guid.NewGuid().ToString();

                ids.Add(id);
                this.controller.Post(new Plan { PlanId = id });
            }

            IActionResult result = this.controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IList<Plan>>(objectResult.Value);

            foreach (Plan item in (IList<Plan>)objectResult.Value)
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
            const string StartName = "A name";
            const string StartNote = "Some notes";
            string id = Guid.NewGuid().ToString();
            var item = new Plan { PlanId = id, Name = StartName, Notes = StartNote };

            this.controller.Post(item);

            var result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);
            Assert.AreEqual(item.Name, StartName);
            Assert.AreEqual(item.Notes, StartNote);
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
            const string StartName = "A name";
            const string PutName = "B name";
            string id = Guid.NewGuid().ToString();
            var item = new Plan { PlanId = id, Name = StartName };

            this.controller.Post(item);

            item.Name = PutName;
            var result = this.controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the plan and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);
            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);
            Assert.AreEqual(item.Name, PutName);
        }

        /// <summary>   (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            string id = Guid.NewGuid().ToString();
            var item = new Plan();
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
            var item = new Plan { PlanId = id };

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
            const string StartName = "A name";
            const string PatchName = "B name";
            const string StartNote = "Some notes";
            string id = Guid.NewGuid().ToString();
            var item = new Plan { PlanId = id, Name = StartName, Notes = StartNote };

            // Create a new plan
            this.controller.Post(item);

            // Patch the plan with a new name
            var patch = new JsonPatchDocument<Plan>();
            patch.Replace(e => e.Name, PatchName);

            var result = this.controller.Patch(id, patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);
            Assert.AreEqual(item.Name, PatchName);
            Assert.AreEqual(item.Notes, StartNote);

            // Get the plan and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);
            Assert.AreEqual(item.Name, PatchName);
            Assert.AreEqual(item.Notes, StartNote);
        }

        /// <summary>   (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            const string StartNote = "Some notes";
            var item = new Plan { PlanId = Guid.NewGuid().ToString(), Name = StartName, Notes = StartNote };

            this.controller.Post(item);

            var patch = new JsonPatchDocument<Plan>();
            patch.Replace(e => e.Name, PatchName);

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
            const StageLevel Level = StageLevel.MultiPack;

            // Create a plan with a stage that has a limit
            var stage = new Stage { StageLevel = Level };
            stage.Limits.Add(new Limit());
            var item = new Plan { PlanId = id };
            item.Stages.Add(stage);

            var result = this.controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            // Get the plan
            result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            // Test the plan
            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.PlanId, id);

            // Test for one stage
            Assert.AreEqual(item.Stages.Count, 1);
            Assert.AreEqual(item.Stages[0].StageLevel, Level);

            // Test for one limit in the stage
            Assert.AreEqual(item.Stages[0].Limits.Count, 1);
        }
    }
}
