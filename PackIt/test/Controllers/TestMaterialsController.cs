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
    using PackIt.Helpers.Enums;
    using PackIt.Material;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestMaterialsController
    {
        /// <summary> The controller under test. </summary>
        private MaterialsController controller;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            var builder = new DbContextOptionsBuilder<MaterialContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testmaterial");

            var context = new MaterialContext(builder.Options);
            var repository = new MaterialRepository(context);

            this.controller = new(
                Mock.Of<ILogger<MaterialsController>>(),
                repository);
            Assert.That(this.controller, Is.Not.Null);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            var item = new Material { MaterialId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Material>());
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
            var item = new Material { MaterialId = Guid.NewGuid().ToString() };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Material>());

            result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
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
                this.controller.Post(new Material { MaterialId = id });
            }

            var result = this.controller.Get();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<List<Material>>());

            var items = objectResult.Value as IList<Material>;
            foreach (var item in items)
            {
                if (ids.Contains(item.MaterialId))
                {
                    ids.Remove(item.MaterialId);
                }
            }

            Assert.That(ids, Is.Empty, "IDS not found " + string.Join(",", ids));
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            const string StartName = "A name";
            const MaterialType Type = MaterialType.Can;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type, Name = StartName };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Material>());

            result = this.controller.Get(id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Material>());

            item = objectResult.Value as Material;
            Assert.That(item.MaterialId, Is.EqualTo(id));
            Assert.That(item.Type, Is.EqualTo(Type));
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
            const MaterialType Type = MaterialType.Cap;
            var id = Guid.NewGuid().ToString();
            var item = new Material { Type = Type, MaterialId = id, Name = StartName };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Material>());

            item.Name = PutName;
            result = this.controller.Put(id, item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That((result as OkResult).StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

            // Get the material and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Material>());

            item = objectResult.Value as Material;
            Assert.That(item.Type, Is.EqualTo(Type));
            Assert.That(item.MaterialId, Is.EqualTo(id));
            Assert.That(item.Name, Is.EqualTo(PutName));
        }

        /// <summary> (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            var id = Guid.NewGuid().ToString();
            var item = new Material();

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
            const MaterialType Type = MaterialType.Collar;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Material>());

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
            const MaterialType Type = MaterialType.Crate;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type, Name = StartName };

            // Create a new material
            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Material>());

            // Patch the material with a new name
            var patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, PatchName);

            result = this.controller.Patch(id, patch);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            // Check the returned object from the patch has the same Note but different Name
            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Material>());

            item = objectResult.Value as Material;
            Assert.That(item.Type, Is.EqualTo(Type));
            Assert.That(item.MaterialId, Is.EqualTo(id));
            Assert.That(item.Name, Is.EqualTo(PatchName));

            // Get the material and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Material>());

            item = objectResult.Value as Material;
            Assert.That(item.Type, Is.EqualTo(Type));
            Assert.That(item.MaterialId, Is.EqualTo(id));
            Assert.That(item.Name, Is.EqualTo(PatchName));
        }

        /// <summary> (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            const MaterialType Type = MaterialType.Crate;
            var item = new Material { MaterialId = Guid.NewGuid().ToString(), Type = Type, Name = StartName };

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Material>());

            var patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, PatchName);

            var id = Guid.NewGuid().ToString();
            result = this.controller.Patch(id, patch);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
            var notfound = result as NotFoundObjectResult;
            Assert.That(notfound.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
            Assert.That(notfound.Value, Is.EqualTo(id));
        }

        /// <summary> (Unit Test Method) posts the complex material. </summary>
        [Test]
        public void PostComplexMaterial()
        {
            const MaterialType Type = MaterialType.Crate;
            var id = Guid.NewGuid().ToString();

            // Create a material with a costing
            var costing = new Costing();
            var item = new Material { MaterialId = id, Type = Type };
            item.Costings.Add(costing);

            var result = this.controller.Post(item);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            var res = result as CreatedAtRouteResult;
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(res.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(res.Value, Is.TypeOf<Material>());

            // Get the material
            result = this.controller.Get(id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var objectResult = result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(objectResult.Value, Is.TypeOf<Material>());

            // Test the material
            item = objectResult.Value as Material;
            Assert.That(item.Type, Is.EqualTo(Type));
            Assert.That(item.MaterialId, Is.EqualTo(id));
            Assert.That(item.Costings.Count, Is.EqualTo(1));
        }
    }
}
