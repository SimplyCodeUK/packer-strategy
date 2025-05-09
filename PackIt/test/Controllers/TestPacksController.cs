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
    using Xunit;
    using PackItLib.Pack;
    using PackIt.Controllers;
    using PackIt.DTO;

    /// <summary> (Unit Test Fixture) a controller for handling test packs. </summary>
    public class TestPacksController
    {
        /// <summary> The controller under test. </summary>
        private readonly PacksController controller;

        /// <summary> Setup for all unit tests here. </summary>
        public TestPacksController()
        {
            var builder = new DbContextOptionsBuilder<PackContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testpack");

            var context = new PackContext(builder.Options);
            var repository = new PackRepository(context);

            this.controller = new(
                Mock.Of<ILogger<PacksController>>(),
                repository);
            Assert.NotNull(this.controller);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void Post()
        {
            var item = new Pack { PackId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);
        }

        /// <summary> (Unit Test Method) post with not valid model. </summary>
        [Fact]
        public void PostModelNotValid()
        {
            var item = new Pack { PackId = null };
            this.controller.ModelState.AddModelError("ID", "Invalid");
            var result = this.controller.Post(item);
            Assert.IsType<BadRequestResult>(result);
            var res = result as BadRequestResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, res.StatusCode);
        }

        /// <summary> (Unit Test Method) posts the no data. </summary>
        [Fact]
        public void PostNoData()
        {
            var result = this.controller.Post(null);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, (result as BadRequestResult).StatusCode);
        }

        /// <summary> (Unit Test Method) posts the already exists. </summary>
        [Fact]
        public void PostAlreadyExists()
        {
            var item = new Pack { PackId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);

            result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.Conflict, (result as StatusCodeResult).StatusCode);
        }

        /// <summary> (Unit Test Method) gets all. </summary>
        [Fact]
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
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<List<Pack>>(objectResult.Value);

            var items = objectResult.Value as IList<Pack>;
            foreach (var item in items)
            {
                if (ids.Contains(item.PackId))
                {
                    ids.Remove(item.PackId);
                }
            }

            Assert.Empty(ids);
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Fact]
        public void Get()
        {
            const string StartName = "A name";
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);

            result = this.controller.Get(id);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Pack>(objectResult.Value);

            item = objectResult.Value as Pack;
            Assert.Equal(id, item.PackId);
            Assert.Equal(StartName, item.Name);
        }

        /// <summary> (Unit Test Method) gets not found. </summary>
        [Fact]
        public void GetNotFound()
        {
            var id = Guid.NewGuid().ToString();

            var result = this.controller.Get(id);
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            var notfound = result as NotFoundObjectResult;
            Assert.Equal((int)HttpStatusCode.NotFound, notfound.StatusCode);
            Assert.Equal(id, notfound.Value);
        }

        /// <summary> (Unit Test Method) puts this object. </summary>
        [Fact]
        public void Put()
        {
            const string StartName = "A name";
            const string PutName = "B name";
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);

            item.Name = PutName;
            result = this.controller.Put(id, item);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, (result as OkResult).StatusCode);

            // Get the pack and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Pack>(objectResult.Value);

            item = objectResult.Value as Pack;
            Assert.Equal(id, item.PackId);
            Assert.Equal(PutName, item.Name);
        }

        /// <summary> (Unit Test Method) put with not valid model. </summary>
        [Fact]
        public void PutModelNotValid()
        {
            const string StartName = "A name";
            const string PutName = "B name";
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);

            item.Name = PutName;
            this.controller.ModelState.AddModelError("ID", "Invalid");
            result = this.controller.Put(id, item);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
            var res2 = result as BadRequestResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, res2.StatusCode);
        }

        /// <summary> (Unit Test Method) puts not found. </summary>
        [Fact]
        public void PutNotFound()
        {
            var id = Guid.NewGuid().ToString();
            var item = new Pack();

            var result = this.controller.Put(id, item);
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            var notfound = result as NotFoundObjectResult;
            Assert.Equal((int)HttpStatusCode.NotFound, notfound.StatusCode);
            Assert.Equal(notfound.Value, id);
        }

        /// <summary> (Unit Test Method) deletes this object. </summary>
        [Fact]
        public void Delete()
        {
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);

            result = this.controller.Delete(id);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, (result as OkResult).StatusCode);
        }

        /// <summary> (Unit Test Method) deletes the not found. </summary>
        [Fact]
        public void DeleteNotFound()
        {
            var id = Guid.NewGuid().ToString();

            var result = this.controller.Delete(id);
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            var notfound = result as NotFoundObjectResult;
            Assert.Equal((int)HttpStatusCode.NotFound, notfound.StatusCode);
            Assert.Equal(notfound.Value, id);
        }

        /// <summary> (Unit Test Method) patches this object. </summary>
        [Fact]
        public void Patch()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            // Create a new pack
            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);

            // Patch the pack with a new name
            var patch = new JsonPatchDocument<Pack>();
            patch.Replace(e => e.Name, PatchName);

            result = this.controller.Patch(id, patch);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Pack>(objectResult.Value);

            item = objectResult.Value as Pack;
            Assert.Equal(id, item.PackId);
            Assert.Equal(PatchName, item.Name);

            // Get the pack and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.IsType<OkObjectResult>(result);

            objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Pack>(objectResult.Value);

            item = objectResult.Value as Pack;
            Assert.Equal(id, item.PackId);
            Assert.Equal(PatchName, item.Name);
        }

        /// <summary> (Unit Test Method) patches with not valid model. </summary>
        [Fact]
        public void PatchModelNotValid()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            var id = Guid.NewGuid().ToString();
            var item = new Pack { PackId = id, Name = StartName };

            // Create a new pack
            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);

            // Patch the pack with a new name
            var patch = new JsonPatchDocument<Pack>();
            patch.Replace(e => e.Name, PatchName);

            this.controller.ModelState.AddModelError("ID", "Invalid");
            result = this.controller.Patch(id, patch);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
            var res2 = result as BadRequestResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, res2.StatusCode);
        }

        /// <summary> (Unit Test Method) patch not found. </summary>
        [Fact]
        public void PatchNotFound()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            var item = new Pack { PackId = Guid.NewGuid().ToString(), Name = StartName };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);

            var patch = new JsonPatchDocument<Pack>();
            patch.Replace(e => e.Name, PatchName);

            var id = Guid.NewGuid().ToString();
            result = this.controller.Patch(id, patch);
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            var notfound = result as NotFoundObjectResult;
            Assert.Equal((int)HttpStatusCode.NotFound, notfound.StatusCode);
            Assert.Equal(notfound.Value, id);
        }

        /// <summary> (Unit Test Method) posts a complex pack. </summary>
        [Fact]
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
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Pack>(res.Value);

            // Get the pack
            result = this.controller.Get(id);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Pack>(objectResult.Value);

            // Test the pack
            item = objectResult.Value as Pack;
            Assert.Equal(item.PackId, id);
            // Test for one stage
            Assert.Single(item.Costings);
            Assert.Equal(Quantity, item.Costings[0].RequiredQuantity);
            Assert.Equal(Weight, item.Costings[0].RequiredWeight);
        }
    }
}
