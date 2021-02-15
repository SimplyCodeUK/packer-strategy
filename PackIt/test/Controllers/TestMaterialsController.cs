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
        /// <summary> The controller logger. </summary>
        private ILogger<MaterialsController> logger;

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

            this.logger = Mock.Of<ILogger<MaterialsController>>();
            this.controller = new MaterialsController(this.logger, repository);
            Assert.IsNotNull(this.controller);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            var item = new Material { MaterialId = Guid.NewGuid().ToString() };
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
            var item = new Material { MaterialId = Guid.NewGuid().ToString() };
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
                var id = Guid.NewGuid().ToString();

                ids.Add(id);
                this.controller.Post(new Material { MaterialId = id });
            }

            IActionResult result = this.controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IList<Material>>(objectResult.Value);

            foreach (var item in (IList<Material>)objectResult.Value)
            {
                if (ids.Contains(item.MaterialId))
                {
                    ids.Remove(item.MaterialId);
                }
            }

            Assert.IsEmpty(ids, "IDS not found " + string.Join(",", ids));
        }

        /// <summary> (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            const string StartName = "A name";
            const MaterialType Type = MaterialType.Can;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type, Name = StartName };

            this.controller.Post(item);

            var result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            item = (Material)objectResult.Value;
            Assert.AreEqual(item.MaterialId, id);
            Assert.AreEqual(item.Type, Type);
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
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
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

            this.controller.Post(item);

            item.Name = PutName;
            var result = this.controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the material and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);
            item = (Material)objectResult.Value;
            Assert.AreEqual(item.Type, Type);
            Assert.AreEqual(item.MaterialId, id);
            Assert.AreEqual(item.Name, PutName);
        }

        /// <summary> (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            var id = Guid.NewGuid().ToString();
            var item = new Material();
            var result = this.controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            const MaterialType Type = MaterialType.Collar;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type };

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
            var id = Guid.NewGuid().ToString();
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
            const MaterialType Type = MaterialType.Crate;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type, Name = StartName };

            // Create a new material
            this.controller.Post(item);

            // Patch the material with a new name
            var patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, PatchName);

            var result = this.controller.Patch(id, patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            item = (Material)objectResult.Value;
            Assert.AreEqual(item.Type, Type);
            Assert.AreEqual(item.MaterialId, id);
            Assert.AreEqual(item.Name, PatchName);

            // Get the material and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            item = (Material)objectResult.Value;
            Assert.AreEqual(item.Type, Type);
            Assert.AreEqual(item.MaterialId, id);
            Assert.AreEqual(item.Name, PatchName);
        }

        /// <summary> (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            const string StartName = "A name";
            const string PatchName = "B name";
            const MaterialType Type = MaterialType.Crate;
            var id = Guid.NewGuid().ToString();
            var item = new Material { MaterialId = id, Type = Type, Name = StartName };

            this.controller.Post(item);

            var patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, PatchName);

            var result = this.controller.Patch(Guid.NewGuid().ToString(), patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary> (Unit Test Method) posts the complex pan. </summary>
        [Test]
        public void PostComplexPan()
        {
            const MaterialType Type = MaterialType.Crate;
            var id = Guid.NewGuid().ToString();

            // Create a material with a costing
            var costing = new Costing();
            var item = new Material { MaterialId = id, Type = Type };
            item.Costings.Add(costing);

            var result = this.controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            // Get the material
            result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            // Test the material
            item = (Material)objectResult.Value;
            Assert.AreEqual(item.Type, Type);
            Assert.AreEqual(item.MaterialId, id);
            Assert.AreEqual(item.Costings.Count, 1);
        }
    }
}
