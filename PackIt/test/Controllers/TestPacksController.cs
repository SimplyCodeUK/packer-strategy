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
            Assert.That(this.controller, Is.Not.Null);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            var item = new Pack { PackId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Pack>());
        }

        /// <summary> (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            var result = this.controller.Post(null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
            Assert.That((result as BadRequestResult).StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        /// <summary> (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            var item = new Pack { PackId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Pack>());

            result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<StatusCodeResult>());
            Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo((int)HttpStatusCode.Conflict));
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<List<Pack>>());

            var items = objectResult.Value as IList<Pack>;
            foreach (var item in items)
            {
                if (ids.Contains(item.PackId))
                {
                    ids.Remove(item.PackId);
                }
            }

            Assert.That(ids, Is.Empty, "IDS not found " + string.Join(",", ids));
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            const string StartName = "A name";
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Pack>());

            result = this.controller.Get(id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Pack>());

            item = objectResult.Value as Pack;
            Assert.That(item.PackId, Is.EqualTo(id));
            Assert.That(item.Name, Is.EqualTo(StartName));
        }

        /// <summary> (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            var id = Guid.NewGuid().ToString();

            var result = this.controller.Get(id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
            var notfound = result as NotFoundObjectResult;
            Assert.That(notfound.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
            Assert.That(notfound.Value, Is.EqualTo(id));
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Pack>());

            item.Name = PutName;
            result = this.controller.Put(id, item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That((result as OkResult).StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

            // Get the pack and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Pack>());

            item = objectResult.Value as Pack;
            Assert.That(item.PackId, Is.EqualTo(id));
            Assert.That(item.Name, Is.EqualTo(PutName));
        }

        /// <summary> (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            var id = Guid.NewGuid().ToString();
            var item = new Pack();

            var result = this.controller.Put(id, item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
            var notfound = result as NotFoundObjectResult;
            Assert.That(notfound.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
            Assert.That(notfound.Value, Is.EqualTo(id));
        }

        /// <summary> (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Pack>());

            result = this.controller.Delete(id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That((result as OkResult).StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        /// <summary> (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            var id = Guid.NewGuid().ToString();

            var result = this.controller.Delete(id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
            var notfound = result as NotFoundObjectResult;
            Assert.That(notfound.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
            Assert.That(notfound.Value, Is.EqualTo(id));
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Pack>());

            // Patch the pack with a new name
            var patch = new JsonPatchDocument<Pack>();
            patch.Replace(e => e.Name, PatchName);

            result = this.controller.Patch(id, patch);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            // Check the returned object from the patch has the same Note but different Name
            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Pack>());

            item = objectResult.Value as Pack;
            Assert.That(item.PackId, Is.EqualTo(id));
            Assert.That(item.Name, Is.EqualTo(PatchName));

            // Get the pack and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Pack>());

            item = objectResult.Value as Pack;
            Assert.That(item.PackId, Is.EqualTo(id));
            Assert.That(item.Name, Is.EqualTo(PatchName));
        }

        /// <summary> (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            var item = new Pack { PackId = Guid.NewGuid().ToString(), Name = StartName };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Pack>());

            var patch = new JsonPatchDocument<Pack>();
            patch.Replace(e => e.Name, PatchName);

            var id = Guid.NewGuid().ToString();
            result = this.controller.Patch(id, patch);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
            var notfound = result as NotFoundObjectResult;
            Assert.That(notfound.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
            Assert.That(notfound.Value, Is.EqualTo(id));
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Pack>());

            // Get the pack
            result = this.controller.Get(id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Pack>());

            // Test the pack
            item = objectResult.Value as Pack;
            Assert.That(item.PackId, Is.EqualTo(id));
            // Test for one stage
            Assert.That(item.Costings.Count, Is.EqualTo(1));
            Assert.That(item.Costings[0].RequiredQuantity, Is.EqualTo(Quantity) );
            Assert.That(item.Costings[0].RequiredWeight, Is.EqualTo(Weight));
        }
    }
}
