// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using PackIt.Controllers;
    using PackIt.DTO;
    using PackIt.Pack;

    /// <summary> (Unit Test Fixture) a controller for handling test packs. </summary>
    [TestFixture]
    public class TestPacksController
    {
        /// <summary> The controller under test. </summary>
        private PacksController controller;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            var builder = new DbContextOptionsBuilder<PackContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testpack");

            var context = new PackContext(builder.Options);
            var repository = new PackRepository(context);

            this.controller = new(
                Mock.Of<ILogger<PacksController>>(),
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
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOf<Pack>(res.Value);
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
            var item = new Pack { PackId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOf<Pack>(res.Value);

            result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Conflict, ((StatusCodeResult)result).StatusCode);
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

                ids.Add(id);
                this.controller.Post(new Pack { PackId = id });
            }

            var result = this.controller.Get();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IList<Pack>>(objectResult.Value);

            var items = objectResult.Value as IList<Pack>;
            foreach (var item in items)
            {
                if (ids.Contains(item.PackId))
                {
                    ids.Remove(item.PackId);
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
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOf<Pack>(res.Value);

            result = this.controller.Get(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);

            item = objectResult.Value as Pack;
            Assert.AreEqual(item.PackId, id);
            Assert.AreEqual(item.Name, StartName);
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

        /// <summary> (Unit Test Method) puts this object. </summary>
        [Test]
        public void Put()
        {
            const string StartName = "A name";
            const string PutName = "B name";
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOf<Pack>(res.Value);

            item.Name = PutName;
            result = this.controller.Put(id, item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, (result as OkResult).StatusCode);

            // Get the pack and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);

            item = objectResult.Value as Pack;
            Assert.AreEqual(item.PackId, id);
            Assert.AreEqual(item.Name, PutName);
        }

        /// <summary> (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            var id = Guid.NewGuid().ToString();
            var item = new Pack();

            var result = this.controller.Put(id, item);
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
            var item = new Pack { PackId = id };

            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOf<Pack>(res.Value);

            result = this.controller.Delete(id);
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

        /// <summary> (Unit Test Method) patches this object. </summary>
        [Test]
        public void Patch()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            // Create a new pack
            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOf<Pack>(res.Value);

            // Patch the pack with a new name
            var patch = new JsonPatchDocument<Pack>();
            patch.Replace(e => e.Name, PatchName);

            result = this.controller.Patch(id, patch);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            var objectResult = result as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);

            item = objectResult.Value as Pack;
            Assert.AreEqual(item.PackId, id);
            Assert.AreEqual(item.Name, PatchName);

            // Get the pack and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            objectResult = result as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);

            item = objectResult.Value as Pack;
            Assert.AreEqual(item.PackId, id);
            Assert.AreEqual(item.Name, PatchName);
        }

        /// <summary> (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            var item = new Pack { PackId = Guid.NewGuid().ToString(), Name = StartName };

            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOf<Pack>(res.Value);

            var patch = new JsonPatchDocument<Pack>();
            patch.Replace(e => e.Name, PatchName);

            var id = Guid.NewGuid().ToString();
            result = this.controller.Patch(id, patch);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notfound = result as NotFoundObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, notfound.StatusCode);
            Assert.AreEqual(id, notfound.Value);
        }

        /// <summary> (Unit Test Method) posts a complex pack. </summary>
        [Test]
        public void PostComplexPack()
        {
            var id = Guid.NewGuid().ToString();
            const long Quantity = 1000;
            const double Weight = 2000.0;

            // Create a pack with a stage that has a limit
            var costing = new Costing { RequiredQuantity = Quantity, RequiredWeight = Weight };

            var item = new Pack { PackId = id };
            item.Costings.Add(costing);

            var result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode);
            Assert.IsTrue(res.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOf<Pack>(res.Value);

            // Get the pack
            result = this.controller.Get(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);

            // Test the pack
            item = objectResult.Value as Pack;
            Assert.AreEqual(item.PackId, id);
            // Test for one stage
            Assert.AreEqual(item.Costings.Count, 1);
            Assert.AreEqual(item.Costings[0].RequiredQuantity, Quantity);
            Assert.AreEqual(item.Costings[0].RequiredWeight, Weight);
        }
    }
}
