//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System;
using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

using packer_strategy.Controllers;
using packer_strategy.Models;
using packer_strategy.Models.Plan;

namespace packer_strategy_test
{
    /*!
     * \class   TestPlansController
     *
     * \brief   (Unit Test Fixture) a controller for handling test plans.
     */
    [TestFixture]
    public class TestPlansController
    {
        /*! \brief   Options for controlling the operation */
        private DbContextOptions<PlanContext> options;
        /*! \brief   The builder */
        private DbContextOptionsBuilder<PlanContext> builder;
        private PlanContext planContext;
        private PlanRepository planRepository;
 
        /*!
         * \fn  public void BeforeTest()
         *
         * \brief   Initialises this object.
         */
        [SetUp]
        public void BeforeTest()
        {
            builder = new DbContextOptionsBuilder<PlanContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;
            planContext = new PlanContext(options);
            planRepository = new PlanRepository(planContext);
        }

        /*!
         * \fn  public void Create()
         *
         * \brief   (Unit Test Method) creates this object.
         */
        [Test]
        public void Create()
        {
            var controller = new PlansController(planRepository);
            Assert.IsNotNull(controller);
        }

        /*!
         * \fn  public void Post()
         *
         * \brief   (Unit Test Method) post this message.
         */
        [Test]
        public void Post()
        {
            var controller = new PlansController(planRepository);
            var plan = new Plan { Id = Guid.NewGuid().ToString() };
            var result = controller.Post(plan);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
        }

        /*!
         * \fn  public void PostBad()
         *
         * \brief   (Unit Test Method) posts the bad.
         */
        [Test]
        public void PostBad()
        {
            var controller = new PlansController(planRepository);
            var result = controller.Post(null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /*!
         * \fn  public void PostAlreadyExists()
         *
         * \brief   (Unit Test Method) posts the already exists.
         */
        [Test]
        public void PostAlreadyExists()
        {
            var controller = new PlansController(planRepository);
            var plan = new Plan { Id = Guid.NewGuid().ToString() };
            var result = controller.Post(plan);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            result = controller.Post(plan);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ForbidResult>(result);
        }
    }
}
