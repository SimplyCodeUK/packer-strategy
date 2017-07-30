//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;
using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

using packer_strategy.Controllers;
using packer_strategy.Models;
using packer_strategy.Models.Plan;

namespace packer_strategy_test
{
    /*! (Unit Test Fixture) a controller for handling test plans. */
    [TestFixture]
    public class TestPlansController
    {
        /*! The plan repository */
        private PlanRepository planRepository;

        /*! Tests before. */
        [SetUp]
        public void BeforeTest()
        {
            DbContextOptionsBuilder<PlanContext> builder = new DbContextOptionsBuilder<PlanContext>();
            builder.UseInMemoryDatabase();

            PlanContext planContext = new PlanContext(builder.Options);

            planRepository = new PlanRepository(planContext);
        }

        /*! (Unit Test Method) creates this object. */
        [Test]
        public void Create()
        {
            PlansController controller = new PlansController(planRepository);

            Assert.IsNotNull(controller);
        }

        /*! (Unit Test Method) post this message. */
        [Test]
        public void Post()
        {
            PlansController controller = new PlansController(planRepository);
            Plan            plan = new Plan { Id = Guid.NewGuid().ToString() };
            var             result = controller.Post(plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
        }

        /*! (Unit Test Method) posts the bad. */
        [Test]
        public void PostBad()
        {
            PlansController controller = new PlansController(planRepository);
            var             result = controller.Post(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /*! (Unit Test Method) posts the already exists. */
        [Test]
        public void PostAlreadyExists()
        {
            PlansController controller = new PlansController(planRepository);
            Plan            plan = new Plan { Id = Guid.NewGuid().ToString() };
            var             result = controller.Post(plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            result = controller.Post(plan);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ForbidResult>(result);
        }

        /*! (Unit Test Method) gets all. */
        [Test]
        public void GetAll()
        {
            int             plansToAdd = 10;
            PlansController controller = new PlansController(planRepository);
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

        /*! (Unit Test Method) gets this object. */
        [Test]
        public void Get()
        {
            PlansController controller = new PlansController(planRepository);
            string          id = Guid.NewGuid().ToString();

            controller.Post(new Plan { Id = id });

            var result = controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /*! (Unit Test Method) gets not found. */
        [Test]
        public void GetNotFound()
        {
            PlansController controller = new PlansController(planRepository);
            string          id = Guid.NewGuid().ToString();

            var result = controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundResult)result).StatusCode);
        }

        /*! (Unit Test Method) puts this object. */
        [Test]
        public void Put()
        {
            PlansController controller = new PlansController(planRepository);
            string          id = Guid.NewGuid().ToString();
            Plan            plan = new Plan { Id = id, Name = "A name" };

            controller.Post(plan);

            plan.Name = "B name";
            var result = controller.Put(id, plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /*! (Unit Test Method) puts the bad. */
        [Test]
        public void PutBad()
        {
            PlansController controller = new PlansController(planRepository);
            Plan            plan = new Plan();

            var result = controller.Put(null, plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /*! (Unit Test Method) puts not found. */
        [Test]
        public void PutNotFound()
        {
            PlansController controller = new PlansController(planRepository);
            string id = Guid.NewGuid().ToString();
            Plan plan = new Plan();

            var result = controller.Put(id, plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundResult)result).StatusCode);
        }
    }
}
