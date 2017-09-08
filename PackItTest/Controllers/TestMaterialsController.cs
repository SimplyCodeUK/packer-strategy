// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItTest
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
    using PackIt.Helpers;
    using PackIt.Helpers.Enums;
    using PackIt.Models.Material;

    /// <summary>   (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestMaterialsController
    {
        /// <summary>   The controller under test. </summary>
        private MaterialsController controller;

        /// <summary>   Tests before. </summary>
        [SetUp]
        public void BeforeTest()
        {
            DbContextOptionsBuilder<MaterialContext> builder = new DbContextOptionsBuilder<MaterialContext>();
            builder.UseInMemoryDatabase("testmaterial");

            MaterialContext context = new MaterialContext(builder.Options);
            MaterialRepository repository = new MaterialRepository(context);

            this.controller = new MaterialsController(repository);
            Assert.IsNotNull(this.controller);
        }

        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            Material item = new Material { Id = Guid.NewGuid().ToString() };
            var result = this.controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            var result = this.controller.Post(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            Material item = new Material { Id = Guid.NewGuid().ToString() };
            var result = this.controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            result = this.controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Conflict, ((StatusCodeResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) gets all. </summary>
        [Test]
        public void GetAll()
        {
            int itemsToAdd = 10;
            List<string> ids = new List<string>();

            for (int item = 0; item < itemsToAdd; ++item)
            {
                string id = Guid.NewGuid().ToString();

                ids.Add(id);
                this.controller.Post(new Material { Id = id });
            }

            IActionResult result = this.controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IEnumerable<Material>>(objectResult.Value);

            IEnumerable<Material> items = (IEnumerable<Material>)objectResult.Value;
            foreach (Material item in items)
            {
                if (ids.Contains(item.Id))
                {
                    ids.Remove(item.Id);
                }
            }

            Assert.IsEmpty(ids, "IDS not found " + string.Join(",", ids));
        }

        /// <summary>   (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            string startName = "A name";
            string startNote = "Some notes";
            MaterialType type = MaterialType.Can;
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Type = type, Name = startName, Notes = startNote };

            this.controller.Post(item);

            var result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            item = (Material)objectResult.Value;
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Type, type);
            Assert.AreEqual(item.Name, startName);
            Assert.AreEqual(item.Notes, startNote);
        }

        /// <summary>   (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) puts this object. </summary>
        [Test]
        public void Put()
        {
            string startName = "A name";
            string putName = "B name";
            MaterialType type = MaterialType.Cap;
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Type = type,  Id = id, Name = startName };

            this.controller.Post(item);

            item.Name = putName;
            var result = this.controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the material and check the returned object has the new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);
            item = (Material)objectResult.Value;
            Assert.AreEqual(item.Type, type);
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, putName);
        }

        /// <summary>   (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            string id = Guid.NewGuid().ToString();
            Material item = new Material();
            var result = this.controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            MaterialType type = MaterialType.Collar;
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Type = type };

            this.controller.Post(item);

            var result = this.controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) patches this object. </summary>
        [Test]
        public void Patch()
        {
            string startName = "A name";
            string patchName = "B name";
            string startNote = "Some notes";
            MaterialType type = MaterialType.Crate;
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Type = type, Name = startName, Notes = startNote };

            // Create a new material
            this.controller.Post(item);

            // Patch the material with a new name
            JsonPatchDocument<Material> patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, patchName);

            var result = this.controller.Patch(id, patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            item = (Material)objectResult.Value;
            Assert.AreEqual(item.Type, type);
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, patchName);
            Assert.AreEqual(item.Notes, startNote);

            // Get the material and check the returned object has the same Note and new Name
            result = this.controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            item = (Material)objectResult.Value;
            Assert.AreEqual(item.Type, type);
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, patchName);
            Assert.AreEqual(item.Notes, startNote);
        }

        /// <summary>   (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            string startName = "A name";
            string patchName = "B name";
            string startNote = "Some notes";
            MaterialType type = MaterialType.Crate;
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Type = type, Name = startName, Notes = startNote };

            this.controller.Post(item);

            JsonPatchDocument<Material> patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, patchName);

            var result = this.controller.Patch(Guid.NewGuid().ToString(), patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the complex pan. </summary>
        [Test]
        public void PostComplexPan()
        {
            MaterialType type = MaterialType.Crate;
            string id = Guid.NewGuid().ToString();

            // Create a material with a costing
            Costing costing = new Costing();
            Material item = new Material { Id = id, Type = type };
            item.Costings.Add(costing);

            var result = this.controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            // Get the material
            result = this.controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            // Test the material
            item = (Material)objectResult.Value;
            Assert.AreEqual(item.Type, type);
            Assert.AreEqual(item.Id, id);
        }
    }
}
