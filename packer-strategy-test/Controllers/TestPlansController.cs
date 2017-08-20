//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy_test
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using packer_strategy.Controllers;
    using packer_strategy.Models;
    using packer_strategy.Models.Plan;

    /// <summary>   (Unit Test Fixture) a controller for handling test plans. </summary>
    [TestFixture]
    public class TestPlansController
    {
        /// <summary>   The plan repository. </summary>
        private PlanRepository _repository;

        /// <summary>   Tests before. </summary>
        [SetUp]
        public void BeforeTest()
        {
            DbContextOptionsBuilder<PlanContext> builder = new DbContextOptionsBuilder<PlanContext>();
            builder.UseInMemoryDatabase("testplan");

            PlanContext context = new PlanContext(builder.Options);

            _repository = new PlanRepository(context);
        }

        /// <summary>   (Unit Test Method) creates this object. </summary>
        [Test]
        public void Create()
        {
            PlansController controller = new PlansController(_repository);

            Assert.IsNotNull(controller);
        }

        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            PlansController controller = new PlansController(_repository);
            Plan item = new Plan { Id = Guid.NewGuid().ToString() };
            var result = controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            PlansController controller = new PlansController(_repository);
            var result = controller.Post(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            PlansController controller = new PlansController(_repository);
            Plan item = new Plan { Id = Guid.NewGuid().ToString() };
            var result = controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            result = controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Conflict, ((StatusCodeResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) gets all. </summary>
        [Test]
        public void GetAll()
        {
            int itemsToAdd = 10;
            PlansController controller = new PlansController(_repository);
            List<string> ids = new List<string>();

            for (int item = 0; item < itemsToAdd; ++item)
            {
                string id = Guid.NewGuid().ToString();

                ids.Add(id);
                controller.Post(new Plan { Id = id });
            }

            IActionResult result = controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IEnumerable<Plan>>(objectResult.Value);

            IEnumerable<Plan> items = (IEnumerable<Plan>)objectResult.Value;
            foreach (Plan item in items)
            {
                if (ids.Contains(item.Id))
                {
                    ids.Remove(item.Id);
                }
            }
            Assert.IsEmpty(ids, "IDS not found " + String.Join(",", ids));
        }

        /// <summary>   (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            string startName = "A name";
            string startNote = "Some notes";
            PlansController controller = new PlansController(_repository);
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan { Id = id, Name = startName, Notes = startNote };

            controller.Post(item);

            var result = controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, startName);
            Assert.AreEqual(item.Notes, startNote);
        }

        /// <summary>   (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            PlansController controller = new PlansController(_repository);
            string id = Guid.NewGuid().ToString();
            var result = controller.Get(id);

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
            PlansController controller = new PlansController(_repository);
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan { Id = id, Name = startName };

            controller.Post(item);

            item.Name = putName;
            var result = controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the plan and check the returned object has the new Name
            result = controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);
            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, putName);
        }

        /// <summary>   (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            PlansController controller = new PlansController(_repository);
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan();
            var result = controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            PlansController controller = new PlansController(_repository);
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan { Id = id };

            controller.Post(item);

            var result = controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            PlansController controller = new PlansController(_repository);
            string id = Guid.NewGuid().ToString();
            var result = controller.Delete(id);

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
            PlansController controller = new PlansController(_repository);
            string id = Guid.NewGuid().ToString();
            Plan item = new Plan { Id = id, Name = startName, Notes = startNote };

            // Create a new plan
            controller.Post(item);

            // Patch the plan with a new name
            JsonPatchDocument<Plan> patch = new JsonPatchDocument<Plan>();
            patch.Replace(e => e.Name, patchName);

            var result = controller.Patch(id, patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, patchName);
            Assert.AreEqual(item.Notes, startNote);

            // Get the plan and check the returned object has the same Note and new Name
            result = controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.Id, id);
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
            PlansController controller = new PlansController(_repository);
            Plan item = new Plan { Id = Guid.NewGuid().ToString(), Name = startName, Notes = startNote };

            controller.Post(item);

            JsonPatchDocument<Plan> patch = new JsonPatchDocument<Plan>();
            patch.Replace(e => e.Name, patchName);

            var result = controller.Patch(Guid.NewGuid().ToString(), patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts a complex pan. </summary>
        [Test]
        public void PostComplexPan()
        {
            PlansController controller = new PlansController(_repository);
            string id = Guid.NewGuid().ToString();
            Stage.StageLevel level = Stage.StageLevel.MultiPack;

            // Create a plan with a stage that has a limit
            Stage stage = new Stage();
            stage.Limits.Add(new Limit());
            stage.Level = level;

            Plan item = new Plan();
            item.Stages.Add(stage);

            item.Id = id;

            var result = controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            // Get the plan
            result = controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            // Test the plan
            item = (Plan)objectResult.Value;
            Assert.AreEqual(item.Id, id);

            // Test for one stage
            Assert.AreEqual(item.Stages.Count, 1);
            Assert.AreEqual(item.Stages[0].OwnerId, id);
            Assert.AreEqual(item.Stages[0].Level, level);

            // Test for one limit in the stage
            Assert.AreEqual(item.Stages[0].Limits.Count, 1);
            Assert.AreEqual(item.Stages[0].Limits[0].OwnerId, id);
            Assert.AreEqual(item.Stages[0].Limits[0].StageLevel, level);
        }
    }
}
