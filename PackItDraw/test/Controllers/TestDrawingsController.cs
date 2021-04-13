// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using PackIt.Drawing;
    using PackIt.DTO;
    using PackIt.Pack;
    using PackItDraw.Controllers;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestDrawingsController
    {
        /// <summary> The controller under test. </summary>
        private DrawingsController controller;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            var builder = new DbContextOptionsBuilder<DrawingContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testdrawing");

            var context = new DrawingContext(builder.Options);
            var repository = new DrawingRepository(context);

            this.controller = new DrawingsController(
                Mock.Of<ILogger<DrawingsController>>(),
                repository);
            Assert.IsNotNull(this.controller);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            var item = new Pack { PackId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            CreatedAtRouteResult res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
        }

        /// <summary> (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            var result = this.controller.Post(null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            var item = new Pack { PackId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            CreatedAtRouteResult res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            var val1 = res.RouteValues["id"].ToString();

            result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            var val2 = res.RouteValues["id"].ToString();

            Assert.AreNotEqual(val1, val2);
        }

        /// <summary> (Unit Test Method) gets all. </summary>
        [Test]
        public void GetAll()
        {
            const int ItemsToAdd = 10;
            var ids = new List<string>();

            for (int item = 0; item < ItemsToAdd; ++item)
            {
                var id = Guid.NewGuid().ToString();

                var resultPost = this.controller.Post(new Pack { PackId = id });
                CreatedAtRouteResult res = resultPost as CreatedAtRouteResult;
                Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
                var val = res.RouteValues["id"].ToString();
                ids.Add(val);
            }

            var result = this.controller.Get();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IList<Drawing>>(objectResult.Value);

            foreach (var item in (IList<Drawing>)objectResult.Value)
            {
                if (ids.Contains(item.DrawingId))
                {
                    ids.Remove(item.DrawingId);
                }
            }

            Assert.IsEmpty(ids, "IDS not found " + string.Join(",", ids));
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            const string StartName = "A name";
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            CreatedAtRouteResult res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            var val1 = res.RouteValues["id"].ToString();

            result = this.controller.Get(val1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Drawing>(objectResult.Value);

            Drawing drawing = (Drawing)objectResult.Value;
            Assert.AreEqual(drawing.DrawingId, val1);
            Assert.AreEqual(drawing.Pack.Name, StartName);
        }

        /// <summary> (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            var id = Guid.NewGuid().ToString();

            var result = this.controller.Get(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id };

            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            CreatedAtRouteResult res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            var val = res.RouteValues["id"].ToString();

            result = this.controller.Delete(val);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            var id = Guid.NewGuid().ToString();

            var result = this.controller.Delete(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }
    }
}
