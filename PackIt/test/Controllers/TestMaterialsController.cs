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
    using PackItLib.Helpers.Enums;
    using PackItLib.Material;
    using PackIt.Controllers;
    using PackIt.DTO;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    public class TestMaterialsController
    {
        /// <summary> The controller under test. </summary>
        private readonly MaterialsController controller;

        /// <summary> Setup for all unit tests here. </summary>
        public TestMaterialsController()
        {
            var builder = new DbContextOptionsBuilder<MaterialContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testmaterial");

            var context = new MaterialContext(builder.Options);
            var repository = new MaterialRepository(context);

            this.controller = new(
                Mock.Of<ILogger<MaterialsController>>(),
                repository);
            Assert.NotNull(this.controller);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void Post()
        {
            var item = new Material { MaterialId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);
        }

        /// <summary> (Unit Test Method) post with not valid model. </summary>
        [Fact]
        public void PostModelNotValid()
        {
            var item = new Material { MaterialId = null };
            this.controller.ModelState.AddModelError("ID", "Invalid");
            var result = this.controller.Post(item);
            Assert.IsType<BadRequestResult>(result);
            var res = result as BadRequestResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, res.StatusCode);
        }

        /// <summary> (Unit Test Method) posts no data. </summary>
        [Fact]
        public void PostNoData()
        {
            var result = this.controller.Post(null);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
            var res = result as BadRequestResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, res.StatusCode);
        }

        /// <summary> (Unit Test Method) posts the already exists. </summary>
        [Fact]
        public void PostAlreadyExists()
        {
            var item = new Material { MaterialId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);

            result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result);
            var res2 = result as StatusCodeResult;
            Assert.Equal((int)HttpStatusCode.Conflict, res2.StatusCode);
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
                this.controller.Post(new Material { MaterialId = id });
            }

            var result = this.controller.Get();
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<List<Material>>(objectResult.Value);

            var items = objectResult.Value as IList<Material>;
            foreach (var item in items)
            {
                if (ids.Contains(item.MaterialId))
                {
                    ids.Remove(item.MaterialId);
                }
            }

            Assert.Empty(ids);
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Fact]
        public void Get()
        {
            const string StartName = "A name";
            const MaterialType Type = MaterialType.Can;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type, Name = StartName };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);

            result = this.controller.Get(id);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Material>(objectResult.Value);

            item = objectResult.Value as Material;
            Assert.Equal(item.MaterialId, id);
            Assert.Equal(Type, item.Type);
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
            Assert.Equal(notfound.Value, id);
        }

        /// <summary> (Unit Test Method) puts this object. </summary>
        [Fact]
        public void Put()
        {
            const string StartName = "A name";
            const string PutName = "B name";
            const MaterialType Type = MaterialType.Cap;
            var id = Guid.NewGuid().ToString();
            var item = new Material { Type = Type, MaterialId = id, Name = StartName };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);

            item.Name = PutName;
            result = this.controller.Put(id, item);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, (result as OkResult).StatusCode);

            // Get the material and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Material>(objectResult.Value);

            item = objectResult.Value as Material;
            Assert.Equal(Type, item.Type);
            Assert.Equal(item.MaterialId, id);
            Assert.Equal(PutName, item.Name);
        }

        /// <summary> (Unit Test Method) put with not valid model. </summary>
        [Fact]
        public void PutModelNotValid()
        {
            const string StartName = "A name";
            const string PutName = "B name";
            const MaterialType Type = MaterialType.Cap;
            var id = Guid.NewGuid().ToString();
            var item = new Material { Type = Type, MaterialId = id, Name = StartName };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);

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
            var item = new Material();

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
            const MaterialType Type = MaterialType.Collar;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);

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
            const MaterialType Type = MaterialType.Crate;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type, Name = StartName };

            // Create a new material
            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);

            // Patch the material with a new name
            var patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, PatchName);

            result = this.controller.Patch(id, patch);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Material>(objectResult.Value);

            item = objectResult.Value as Material;
            Assert.Equal(Type, item.Type);
            Assert.Equal(id, item.MaterialId);
            Assert.Equal(PatchName, item.Name);

            // Get the material and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.IsType<OkObjectResult>(result);

            objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Material>(objectResult.Value);

            item = objectResult.Value as Material;
            Assert.Equal(Type, item.Type);
            Assert.Equal(item.MaterialId, id);
            Assert.Equal(PatchName, item.Name);
        }

        /// <summary> (Unit Test Method) patches with not valid model. </summary>
        [Fact]
        public void PatchModelNotValid()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            const MaterialType Type = MaterialType.Crate;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type, Name = StartName };

            // Create a new material
            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);

            // Patch the material with a new name
            var patch = new JsonPatchDocument<Material>();
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
            const MaterialType Type = MaterialType.Crate;
            var item = new Material { MaterialId = Guid.NewGuid().ToString(), Type = Type, Name = StartName };

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);

            var patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, PatchName);

            var id = Guid.NewGuid().ToString();
            result = this.controller.Patch(id, patch);
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            var notfound = result as NotFoundObjectResult;
            Assert.Equal((int)HttpStatusCode.NotFound, notfound.StatusCode);
            Assert.Equal(notfound.Value, id);
        }

        /// <summary> (Unit Test Method) posts the complex material. </summary>
        [Fact]
        public void PostComplexMaterial()
        {
            const MaterialType Type = MaterialType.Crate;
            var id = Guid.NewGuid().ToString();

            // Create a material with a costing
            var costing = new Costing();
            var item = new Material { MaterialId = id, Type = Type };
            item.Costings.Add(costing);

            var result = this.controller.Post(item);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Material>(res.Value);

            // Get the material
            result = this.controller.Get(id);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Material>(objectResult.Value);

            // Test the material
            item = objectResult.Value as Material;
            Assert.Equal(Type, item.Type);
            Assert.Equal(item.MaterialId, id);
            Assert.Single(item.Costings);
        }
    }
}
