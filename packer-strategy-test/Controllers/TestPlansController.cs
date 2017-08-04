//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
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

namespace packer_strategy_test
{
    /// <summary>   (Unit Test Fixture) a controller for handling test plans. </summary>
    [TestFixture]
    public class TestPlansController
    {
        /// <summary>   The plan repository. </summary>
        private PlanRepository _planRepository;

        /// <summary>   Tests before. </summary>
        [SetUp]
        public void BeforeTest()
        {
            DbContextOptionsBuilder<PlanContext> builder = new DbContextOptionsBuilder<PlanContext>();
            builder.UseInMemoryDatabase();

            PlanContext planContext = new PlanContext(builder.Options);

            _planRepository = new PlanRepository(planContext);
        }

        /// <summary>   (Unit Test Method) creates this object. </summary>
        [Test]
        public void Create()
        {
            PlansController controller = new PlansController(_planRepository);

            Assert.IsNotNull(controller);
        }

        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            PlansController controller = new PlansController(_planRepository);
            Plan            plan = new Plan { Id = Guid.NewGuid().ToString() };
            var             result = controller.Post(plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the bad. </summary>
        [Test]
        public void PostBad()
        {
            PlansController controller = new PlansController(_planRepository);
            var             result = controller.Post(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            PlansController controller = new PlansController(_planRepository);
            Plan            plan = new Plan { Id = Guid.NewGuid().ToString() };
            var             result = controller.Post(plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            result = controller.Post(plan);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Conflict, ((StatusCodeResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) gets all. </summary>
        [Test]
        public void GetAll()
        {
            int             plansToAdd = 10;
            PlansController controller = new PlansController(_planRepository);
            List<string>    ids = new List<string>();

            for (int plan=0; plan<plansToAdd; ++plan )
            {
                string id = Guid.NewGuid().ToString();

                ids.Add(id);
                controller.Post(new Plan { Id = id });
            }

            IEnumerable<Plan> plans = controller.Get();

            Assert.IsNotNull(plans);
            foreach (Plan plan in plans)
            {
                if (ids.Contains(plan.Id))
                {
                    ids.Remove(plan.Id);
                }
            }
            Assert.IsEmpty(ids, "IDS not found " + String.Join(",", ids));
        }

        /// <summary>   (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            string          startName = "A name";
            string          startNote = "Some notes";
            PlansController controller = new PlansController(_planRepository);
            string          id = Guid.NewGuid().ToString();
            Plan            plan = new Plan { Id = id, Name = startName, Notes = startNote };

            controller.Post(plan);

            var result = controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            plan = (Plan)objectResult.Value;
            Assert.AreEqual(plan.Id, id);
            Assert.AreEqual(plan.Name, startName);
            Assert.AreEqual(plan.Notes, startNote);
        }

        /// <summary>   (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            PlansController controller = new PlansController(_planRepository);
            string          id = Guid.NewGuid().ToString();
            var             result = controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) puts this object. </summary>
        [Test]
        public void Put()
        {
            string          startName = "A name";
            string          putName = "B name";
            PlansController controller = new PlansController(_planRepository);
            string          id = Guid.NewGuid().ToString();
            Plan            plan = new Plan { Id = id, Name = startName };

            controller.Post(plan);

            plan.Name = putName;
            var result = controller.Put(id, plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the plan and check the returned object has the new Name
            result = controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);
            plan = (Plan)objectResult.Value;
            Assert.AreEqual(plan.Id, id);
            Assert.AreEqual(plan.Name, putName);
        }

        /// <summary>   (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            PlansController controller = new PlansController(_planRepository);
            string          id = Guid.NewGuid().ToString();
            Plan            plan = new Plan();
            var             result = controller.Put(id, plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            PlansController controller = new PlansController(_planRepository);
            string          id = Guid.NewGuid().ToString();
            Plan            plan = new Plan { Id = id };

            controller.Post(plan);

            var result = controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            PlansController controller = new PlansController(_planRepository);
            string          id = Guid.NewGuid().ToString();
            var             result = controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) patches this object. </summary>
        [Test]
        public void Patch()
        {
            string          startName = "A name";
            string          patchName = "B name";
            string          startNote = "Some notes";
            PlansController controller = new PlansController(_planRepository);
            string          id = Guid.NewGuid().ToString();
            Plan            plan = new Plan { Id = id, Name = startName, Notes = startNote };

            // Create a new plan
            controller.Post(plan);

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

            plan = (Plan)objectResult.Value;
            Assert.AreEqual(plan.Id, id);
            Assert.AreEqual(plan.Name, patchName);
            Assert.AreEqual(plan.Notes, startNote);

            // Get the plan and check the returned object has the same Note and new Name
            result = controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Plan>(objectResult.Value);

            plan = (Plan)objectResult.Value;
            Assert.AreEqual(plan.Id, id);
            Assert.AreEqual(plan.Name, patchName);
            Assert.AreEqual(plan.Notes, startNote);
        }

        /// <summary>   (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            string          startName = "A name";
            string          patchName = "B name";
            string          startNote = "Some notes";
            PlansController controller = new PlansController(_planRepository);
            Plan            plan = new Plan { Id = Guid.NewGuid().ToString(), Name = startName, Notes = startNote };

            controller.Post(plan);

            JsonPatchDocument<Plan> patch = new JsonPatchDocument<Plan>();
            patch.Replace(e => e.Name, patchName);

            var result = controller.Patch(Guid.NewGuid().ToString(), patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundResult)result).StatusCode);
        }
    }
}
