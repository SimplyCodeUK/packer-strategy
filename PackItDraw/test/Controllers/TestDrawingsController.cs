// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text.Json;
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

        /// <summary> The pack to draw. </summary>
        private Pack drawingPack;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            var builder = new DbContextOptionsBuilder<DrawingContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testdrawing");

            var context = new DrawingContext(builder.Options);
            var repository = new DrawingRepository(context);

            // Pack to draw in tests
            var text = File.ReadAllText("Controllers/TestData/pack.json");
            this.drawingPack = JsonSerializer.Deserialize<Pack>(text);

            this.controller = new(
                Mock.Of<ILogger<DrawingsController>>(),
                repository);
            Assert.IsNotNull(this.controller);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            this.drawingPack.PackId = Guid.NewGuid().ToString();

            var result = this.controller.Post(this.drawingPack);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOf<Drawing>(res.Value);
        }

        /// <summary> (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            var result = this.controller.Post(null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (result as BadRequestResult).StatusCode);
        }

        /// <summary> (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            this.drawingPack.PackId = Guid.NewGuid().ToString();

            var result = this.controller.Post(this.drawingPack);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            var val1 = res.RouteValues["id"].ToString();
            Assert.IsInstanceOf<Drawing>(res.Value);

            result = this.controller.Post(this.drawingPack);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            var val2 = res.RouteValues["id"].ToString();
            Assert.IsInstanceOf<Drawing>(res.Value);

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
                this.drawingPack.PackId = id;

                var resultPost = this.controller.Post(this.drawingPack);
                var res = resultPost as CreatedAtRouteResult;
                Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
                var val = res.RouteValues["id"].ToString();
                ids.Add(val);
            }

            var result = this.controller.Get();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IList<Drawing>>(objectResult.Value);

            var items = objectResult.Value as IList<Drawing>;
            foreach (var item in items)
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
            this.drawingPack.PackId = id;
            this.drawingPack.Name = StartName;

            var result = this.controller.Post(this.drawingPack);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            var val1 = res.RouteValues["id"].ToString();

            result = this.controller.Get(val1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Drawing>(objectResult.Value);

            var drawing = objectResult.Value as Drawing;
            Assert.AreEqual(drawing.DrawingId, val1);
            Assert.AreEqual(drawing.Packs[0].Name, StartName);
        }

        /// <summary> (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            var id = Guid.NewGuid().ToString();

            var result = this.controller.Get(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notfound = result as NotFoundObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, notfound.StatusCode);
            Assert.AreEqual(id, notfound.Value);
        }

        /// <summary> (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            var id = Guid.NewGuid().ToString();
            this.drawingPack.PackId = id;

            var result = this.controller.Post(this.drawingPack);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            var val = res.RouteValues["id"].ToString();
            Assert.IsInstanceOf<Drawing>(res.Value);

            result = this.controller.Delete(val);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, (result as OkResult).StatusCode);
        }

        /// <summary> (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            var id = Guid.NewGuid().ToString();

            var result = this.controller.Delete(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notfound = result as NotFoundObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, notfound.StatusCode);
            Assert.AreEqual(id, notfound.Value);
        }
    }
}
