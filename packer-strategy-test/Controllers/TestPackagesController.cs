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
    using packer_strategy.DTO;
    using packer_strategy.Models.Package;

    /// <summary>   (Unit Test Fixture) a controller for handling test packages. </summary>
    [TestFixture]
    public class TestPackagesController
    {
        /// <summary>   The package repository. </summary>
        private PackageRepository _repository;

        /// <summary>   Tests before. </summary>
        [SetUp]
        public void BeforeTest()
        {
            DbContextOptionsBuilder<PackageContext> builder = new DbContextOptionsBuilder<PackageContext>();
            builder.UseInMemoryDatabase("testpackage");

            PackageContext context = new PackageContext(builder.Options);

            _repository = new PackageRepository(context);
        }

        /// <summary>   (Unit Test Method) creates this object. </summary>
        [Test]
        public void Create()
        {
            PackagesController controller = new PackagesController(_repository);

            Assert.IsNotNull(controller);
        }

        /// <summary>   (Unit Test Method) post this message. </summary>
        [Test]
        public void Post()
        {
            PackagesController controller = new PackagesController(_repository);
            Package item = new Package { Id = Guid.NewGuid().ToString() };
            var result = controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the no data. </summary>
        [Test]
        public void PostNoData()
        {
            PackagesController controller = new PackagesController(_repository);
            var result = controller.Post(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((BadRequestResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts the already exists. </summary>
        [Test]
        public void PostAlreadyExists()
        {
            PackagesController controller = new PackagesController(_repository);
            Package item = new Package { Id = Guid.NewGuid().ToString() };
            var result = controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            result = controller.Post(item);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Conflict, ((StatusCodeResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) gets all. </summary>
        [Test]
        public void GetAll()
        {
            int itemsToAdd = 10;
            PackagesController controller = new PackagesController(_repository);
            List<string> ids = new List<string>();

            for (int item = 0; item < itemsToAdd; ++item)
            {
                string id = Guid.NewGuid().ToString();

                ids.Add(id);
                controller.Post(new Package { Id = id });
            }

            IActionResult result = controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<IEnumerable<Package>>(objectResult.Value);

            IEnumerable<Package> items = (IEnumerable<Package>)objectResult.Value;
            foreach (Package item in items)
            {
                if (ids.Contains(item.Id))
                {
                    ids.Remove(item.Id);
                }
            }
            Assert.IsEmpty(ids, "IDS not found " + String.Join(",", ids));
        }

        /// <summary>   (Unit Test Method) gets this object. </summary>
        [Test]
        public void Get()
        {
            string startName = "A name";
            PackagesController controller = new PackagesController(_repository);
            string id = Guid.NewGuid().ToString();
            Package item = new Package { Id = id, Name = startName };

            controller.Post(item);

            var result = controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Package>(objectResult.Value);

            item = (Package)objectResult.Value;
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, startName);
        }

        /// <summary>   (Unit Test Method) gets not found. </summary>
        [Test]
        public void GetNotFound()
        {
            PackagesController controller = new PackagesController(_repository);
            string id = Guid.NewGuid().ToString();
            var result = controller.Get(id);

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
            PackagesController controller = new PackagesController(_repository);
            string id = Guid.NewGuid().ToString();
            Package item = new Package { Id = id, Name = startName };

            controller.Post(item);

            item.Name = putName;
            var result = controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

            // Get the package and check the returned object has the new Name
            result = controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Package>(objectResult.Value);
            item = (Package)objectResult.Value;
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, putName);
        }

        /// <summary>   (Unit Test Method) puts not found. </summary>
        [Test]
        public void PutNotFound()
        {
            PackagesController controller = new PackagesController(_repository);
            string id = Guid.NewGuid().ToString();
            Package item = new Package();
            var result = controller.Put(id, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes this object. </summary>
        [Test]
        public void Delete()
        {
            PackagesController controller = new PackagesController(_repository);
            string id = Guid.NewGuid().ToString();
            Package item = new Package { Id = id };

            controller.Post(item);

            var result = controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) deletes the not found. </summary>
        [Test]
        public void DeleteNotFound()
        {
            PackagesController controller = new PackagesController(_repository);
            string id = Guid.NewGuid().ToString();
            var result = controller.Delete(id);

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
            PackagesController controller = new PackagesController(_repository);
            string id = Guid.NewGuid().ToString();
            Package item = new Package { Id = id, Name = startName };

            // Create a new package
            controller.Post(item);

            // Patch the package with a new name
            JsonPatchDocument<Package> patch = new JsonPatchDocument<Package>();
            patch.Replace(e => e.Name, patchName);

            var result = controller.Patch(id, patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Check the returned object from the patch has the same Note but different Name
            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Package>(objectResult.Value);

            item = (Package)objectResult.Value;
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, patchName);

            // Get the package and check the returned object has the same Note and new Name
            result = controller.Get(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Package>(objectResult.Value);

            item = (Package)objectResult.Value;
            Assert.AreEqual(item.Id, id);
            Assert.AreEqual(item.Name, patchName);
        }

        /// <summary>   (Unit Test Method) patch not found. </summary>
        [Test]
        public void PatchNotFound()
        {
            string startName = "A name";
            string patchName = "B name";
            PackagesController controller = new PackagesController(_repository);
            Package item = new Package { Id = Guid.NewGuid().ToString(), Name = startName };

            controller.Post(item);

            JsonPatchDocument<Package> patch = new JsonPatchDocument<Package>();
            patch.Replace(e => e.Name, patchName);

            var result = controller.Patch(Guid.NewGuid().ToString(), patch);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        /// <summary>   (Unit Test Method) posts a complex pan. </summary>
        [Test]
        public void PostComplexPan()
        {
            PackagesController controller = new PackagesController(_repository);
            string id = Guid.NewGuid().ToString();
            long quantity = 1000;
            double weight = 2000.0;

            // Create a package with a stage that has a limit
            Costing costing = new Costing { RequiredQuantity= quantity, RequiredWeight= weight };

            Package item = new Package();
            item.Costings.Add(costing);

            item.Id = id;

            var result = controller.Post(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            Assert.AreEqual((int)HttpStatusCode.Created, ((CreatedAtRouteResult)result).StatusCode);

            // Get the package
            result = controller.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Package>(objectResult.Value);

            // Test the plan
            item = (Package)objectResult.Value;
            Assert.AreEqual(item.Id, id);

            // Test for one stage
            Assert.AreEqual(item.Costings.Count, 1);
            Assert.AreEqual(item.Costings[0].OwnerId, id);
            Assert.AreEqual(item.Costings[0].Index, 0);
            Assert.AreEqual(item.Costings[0].RequiredQuantity, quantity);
            Assert.AreEqual(item.Costings[0].RequiredWeight, weight);
        }
    }
}
