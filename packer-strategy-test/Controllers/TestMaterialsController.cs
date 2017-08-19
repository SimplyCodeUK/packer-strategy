//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy_test
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using packer_strategy.Controllers;
    using packer_strategy.Helpers;
    using packer_strategy.Models;
    using packer_strategy.Models.Material;

    /// <summary>   (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestMaterialsController
    {
        /// <summary>   The repository. </summary>
        private MaterialRepository _repository;

        /// <summary>   Type of the bad. </summary>
        private static string _badType = "BadType";

        /// <summary>   Tests before. </summary>
        [SetUp]
        public void BeforeTest()
        {
            DbContextOptionsBuilder<MaterialContext> builder = new DbContextOptionsBuilder<MaterialContext>();
            builder.UseInMemoryDatabase("testmaterial");

            MaterialContext context = new MaterialContext(builder.Options);

            _repository = new MaterialRepository(context);
        }

        /// <summary>   (Unit Test Method) creates this object. </summary>
        [Test]
        public void Create()
        {
            MaterialsController controller = new MaterialsController(_repository);

            Assert.IsNotNull(controller);
        }

        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            MaterialsController controller = new MaterialsController(_repository);
            Material item = new Material { Id = Guid.NewGuid().ToString() };
            var result = controller.Post(Attributes.UrlName(item.IdType), item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            MaterialsController controller = new MaterialsController(_repository);
            var result = controller.Post(Attributes.UrlName(Material.Type.Bottle), null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the bad type. </summary>
        [Test]
        public void PostBadType()
        {
            MaterialsController controller = new MaterialsController(_repository);
            Material item = new Material { Id = Guid.NewGuid().ToString() };
            var result = controller.Post(_badType, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            MaterialsController controller = new MaterialsController(_repository);
            Material item = new Material { Id = Guid.NewGuid().ToString() };
            string nameType = Attributes.UrlName(item.IdType);
            var result = controller.Post(nameType, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            result = controller.Post(nameType, item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Conflict, ((StatusCodeResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) gets all. </summary>
        [Test]
        public void GetAll()
        {
            int itemsToAdd = 10;
            MaterialsController controller = new MaterialsController(_repository);
            List<string> ids = new List<string>();
            Material.Type type = Material.Type.Bottle;
            string nameType = Attributes.UrlName(type);

            for (int item = 0; item < itemsToAdd; ++item)
            {
                string id = Guid.NewGuid().ToString();

                ids.Add(id);
                controller.Post(nameType, new Material { Id = id });
            }

            IActionResult result = controller.Get(nameType);

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
            Assert.IsEmpty(ids, "IDS not found " + String.Join(",", ids));
        }

        /// <summary>   (Unit Test Method) gets all bad type. </summary>
        [Test]
        public void GetAllBadType()
        {
            MaterialsController controller = new MaterialsController(_repository);
            IActionResult result = controller.Get(_badType);

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
            MaterialsController controller = new MaterialsController(_repository);
            Material.Type type = Material.Type.Can;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Name = startName, Notes = startNote };

            controller.Post(nameType, item);

            var result = controller.Get(nameType, id);

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
            MaterialsController controller = new MaterialsController(_repository);
            string id = Guid.NewGuid().ToString();
            var result = controller.Get(_badType, id);

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
            MaterialsController controller = new MaterialsController(_repository);
            var result = controller.Get();

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
            MaterialsController controller = new MaterialsController(_repository);
            Material.Type type = Material.Type.Cap;
            string id = Guid.NewGuid().ToString();
            var result = controller.Get(Attributes.UrlName(type), id);

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
            MaterialsController controller = new MaterialsController(_repository);
            Material.Type type = Material.Type.Cap;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Name = startName };

            controller.Post(nameType, item);

            item.Name = putName;
            var result = controller.Put(nameType, id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the material and check the returned object has the new Name
            result = controller.Get(nameType, id);
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
            MaterialsController controller = new MaterialsController(_repository);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id };
            var result = controller.Put(_badType, id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            MaterialsController controller = new MaterialsController(_repository);
            Material.Type type = Material.Type.Carton;
            string id = Guid.NewGuid().ToString();
            Material item = new Material();
            var result = controller.Put(Attributes.UrlName(type), id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            MaterialsController controller = new MaterialsController(_repository);
            Material.Type type = Material.Type.Collar;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id };

            controller.Post(nameType, item);

            var result = controller.Delete(nameType, id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes the bad type. </summary>
        [Test]
        public void DeleteBadType()
        {
            MaterialsController controller = new MaterialsController(_repository);
            string id = Guid.NewGuid().ToString();
            var result = controller.Delete(_badType, id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            MaterialsController controller = new MaterialsController(_repository);
            Material.Type type = Material.Type.Collar;
            string id = Guid.NewGuid().ToString();
            var result = controller.Delete(Attributes.UrlName(type), id);

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
            MaterialsController controller = new MaterialsController(_repository);
            Material.Type type = Material.Type.Crate;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Name = startName, Notes = startNote };

            // Create a new material
            controller.Post(nameType, item);

            // Patch the material with a new name
            JsonPatchDocument<Material> patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, patchName);

            var result = controller.Patch(nameType, id, patch);

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
            result = controller.Get(nameType, id);
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
            MaterialsController controller = new MaterialsController(_repository);
            string id = Guid.NewGuid().ToString();

            // Patch the material with a new name
            JsonPatchDocument<Material> patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, patchName);

            var result = controller.Patch(_badType, id, patch);

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
            MaterialsController controller = new MaterialsController(_repository);
            Material.Type type = Material.Type.Crate;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();
            Material item = new Material { Id = id, Name = startName, Notes = startNote };

            controller.Post(nameType, item);

            JsonPatchDocument<Material> patch = new JsonPatchDocument<Material>();
            patch.Replace(e => e.Name, patchName);

            var result = controller.Patch(nameType, Guid.NewGuid().ToString(), patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the complex pan. </summary>
        [Test]
        public void PostComplexPan()
        {
            MaterialsController controller = new MaterialsController(_repository);
            Material.Type type = Material.Type.Crate;
            string nameType = Attributes.UrlName(type);
            string id = Guid.NewGuid().ToString();

            // Create a material with a costing
            Costing costing = new Costing();
            Material item = new Material();
            item.Costings.Add(costing);

            item.Id = id;

            var result = controller.Post(nameType, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            // Get the material
            result = controller.Get(nameType, id);

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
