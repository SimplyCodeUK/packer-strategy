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

            this.controller = new PacksController(repository);
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
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
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
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

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
                string id = Guid.NewGuid().ToString();

                ids.Add(id);
                this.controller.Post(new Pack { PackId = id });
            }

            IActionResult result = this.controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IList<Pack>>(objectResult.Value);

            foreach (Pack item in (IList<Pack>)objectResult.Value)
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
            string id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            this.controller.Post(item);

            var result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);

            item = (Pack)objectResult.Value;
            Assert.AreEqual(item.PackId, id);
            Assert.AreEqual(item.Name, StartName);
        }

        /// <summary> (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) puts this object. </summary>
        [Test]
        public void Put()
        {
            const string StartName = "A name";
            const string PutName = "B name";
            string id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            this.controller.Post(item);

            item.Name = PutName;
            var result = this.controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the pack and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);
            item = (Pack)objectResult.Value;
            Assert.AreEqual(item.PackId, id);
            Assert.AreEqual(item.Name, PutName);
        }

        /// <summary> (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            string id = Guid.NewGuid().ToString();
            var item = new Pack();
            var result = this.controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            string id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id };

            this.controller.Post(item);

            var result = this.controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) patches this object. </summary>
        [Test]
        public void Patch()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            string id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            // Create a new pack
            this.controller.Post(item);

            // Patch the pack with a new name
            var patch = new JsonPatchDocument<Pack>();
            patch.Replace(e => e.Name, PatchName);

            var result = this.controller.Patch(id, patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);

            item = (Pack)objectResult.Value;
            Assert.AreEqual(item.PackId, id);
            Assert.AreEqual(item.Name, PatchName);

            // Get the pack and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);

            item = (Pack)objectResult.Value;
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

            this.controller.Post(item);

            var patch = new JsonPatchDocument<Pack>();
            patch.Replace(e => e.Name, PatchName);

            var result = this.controller.Patch(Guid.NewGuid().ToString(), patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) posts a complex pan. </summary>
        [Test]
        public void PostComplexPan()
        {
            string id = Guid.NewGuid().ToString();
            const long Quantity = 1000;
            const double Weight = 2000.0;

            // Create a pack with a stage that has a limit
            var costing = new Costing { RequiredQuantity = Quantity, RequiredWeight = Weight };

            var item = new Pack { PackId = id };
            item.Costings.Add(costing);

            var result = this.controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            // Get the pack
            result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Pack>(objectResult.Value);

            // Test the plan
            item = (Pack)objectResult.Value;
            Assert.AreEqual(item.PackId, id);

            // Test for one stage
            Assert.AreEqual(item.Costings.Count, 1);
            Assert.AreEqual(item.Costings[0].RequiredQuantity, Quantity);
            Assert.AreEqual(item.Costings[0].RequiredWeight, Weight);
        }
    }
}
