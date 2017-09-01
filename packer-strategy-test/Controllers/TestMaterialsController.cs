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
        /// <summary>   Type of the bad. </summary>
        private static string badType = "BadType";

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
            var result = this.controller.Post(Attributes.UrlName(item.IdType), item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            var result = this.controller.Post(Attributes.UrlName(MaterialType.Bottle), null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the bad type. </summary>
        [Test]
        public void PostBadType()
        {
            Material item = new Material { Id = Guid.NewGuid().ToString() };
            var result = this.controller.Post(badType, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            Material item = new Material { Id = Guid.NewGuid().ToString() };
            string nameType = Attributes.UrlName(item.IdType);
            var result = this.controller.Post(nameType, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            result = this.controller.Post(nameType, item);
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
            MaterialType type = MaterialType.Bottle;
            string nameType = Attributes.UrlName(type);

            for (int item = 0; item < itemsToAdd; ++item)
            {
                string id = Guid.NewGuid().ToString();

                ids.Add(id);
                this.controller.Post(nameType, new Material { Id = id });
            }

            IActionResult result = this.controller.Get(nameType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IEnumerable<Material>>(objectResult.Value);

            IEnumerable<Material> items = (IEnumerable<Material>)objectResult.Value;
            foreach (Material item in items)
            {
                if (ids.Contains(item.Id) && item.IdType == type)
                {
                    ids.Remove(item.Id);
                }
            }

            Assert.IsEmpty(ids, "IDS not found " + string.Join(",", ids));
        }

        /// <summary>   (Unit Test Method) gets all bad type. </summary>
        [Test]
        public void GetAllBadType()
        {
            IActionResult result = this.controller.Get(badType);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            string startName = "A name";
            string startNote = "Some notes";
            MaterialType type = MaterialType.Can;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Name = startName, Notes = startNote };

            this.controller.Post(nameType, item);

            var result = this.controller.Get(nameType, id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            item = (Material)objectResult.Value;
            Assert.AreEqual(item.IdType, type);
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, startName);
            Assert.AreEqual(item.Notes, startNote);
        }

        /// <summary>   (Unit Test Method) gets bad type. </summary>
        [Test]
        public void GetBadType()
        {
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Get(badType, id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>
        /// The GetTypes
        /// </summary>
        [Test]
        public void GetTypes()
        {
            var result = this.controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IEnumerable<string>>(objectResult.Value);

            IEnumerable<string> item = (IEnumerable<string>)objectResult.Value;
            Assert.IsNotEmpty(item);
        }

        /// <summary>   (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            MaterialType type = MaterialType.Cap;
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Get(Attributes.UrlName(type), id);

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
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { IdType = type,  Id = id, Name = startName };

            this.controller.Post(nameType, item);

            item.Name = putName;
            var result = this.controller.Put(nameType, id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the material and check the returned object has the new Name
            result = this.controller.Get(nameType, id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);
            item = (Material)objectResult.Value;
            Assert.AreEqual(item.IdType, type);
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, putName);
        }

        /// <summary>   (Unit Test Method) puts bad type. </summary>
        [Test]
        public void PutBadType()
        {
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id };
            var result = this.controller.Put(badType, id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            MaterialType type = MaterialType.Carton;
            string id = Guid.NewGuid().ToString();
            Material item = new Material();
            var result = this.controller.Put(Attributes.UrlName(type), id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            MaterialType type = MaterialType.Collar;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id };

            this.controller.Post(nameType, item);

            var result = this.controller.Delete(nameType, id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes the bad type. </summary>
        [Test]
        public void DeleteBadType()
        {
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Delete(badType, id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            MaterialType type = MaterialType.Collar;
            string id = Guid.NewGuid().ToString();
            var result = this.controller.Delete(Attributes.UrlName(type), id);

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
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { IdType = type, Id = id, Name = startName, Notes = startNote };

            // Create a new material
            this.controller.Post(nameType, item);

            // Patch the material with a new name
            JsonPatchDocument<Material> patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, patchName);

            var result = this.controller.Patch(nameType, id, patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            item = (Material)objectResult.Value;
            Assert.AreEqual(item.IdType, type);
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, patchName);
            Assert.AreEqual(item.Notes, startNote);

            // Get the material and check the returned object has the same Note and new Name
            result = this.controller.Get(nameType, id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            item = (Material)objectResult.Value;
            Assert.AreEqual(item.IdType, type);
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, patchName);
            Assert.AreEqual(item.Notes, startNote);
        }

        /// <summary>   (Unit Test Method) patch bad type. </summary>
        [Test]
        public void PatchBadType()
        {
            string patchName = "B name";
            string id = Guid.NewGuid().ToString();

            // Patch the material with a new name
            JsonPatchDocument<Material> patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, patchName);

            var result = this.controller.Patch(badType, id, patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            string startName = "A name";
            string patchName = "B name";
            string startNote = "Some notes";
            MaterialType type = MaterialType.Crate;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Name = startName, Notes = startNote };

            this.controller.Post(nameType, item);

            JsonPatchDocument<Material> patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, patchName);

            var result = this.controller.Patch(nameType, Guid.NewGuid().ToString(), patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the complex pan. </summary>
        [Test]
        public void PostComplexPan()
        {
            MaterialType type = MaterialType.Crate;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();

            // Create a material with a costing
            Costing costing = new Costing();
            Material item = new Material() { Id = id };
            item.Costings.Add(costing);

            var result = this.controller.Post(nameType, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            // Get the material
            result = this.controller.Get(nameType, id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Material>(objectResult.Value);

            // Test the material
            item = (Material)objectResult.Value;
            Assert.AreEqual(item.IdType, type);
            Assert.AreEqual(item.Id, id);
        }
    }
}
