// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text.Json;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;
    using PackIt.Drawing;
    using PackIt.DTO;
    using PackIt.Pack;
    using PackItDraw.Controllers;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    public class TestDrawingsController
    {
        /// <summary> The controller under test. </summary>
        private DrawingsController controller;

        /// <summary> The pack to draw. </summary>
        private Pack drawingPack;

        /// <summary> Setup for all unit tests here. </summary>
        public TestDrawingsController()
        {
            var builder = new DbContextOptionsBuilder<DrawingContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testdrawing");

            var context = new DrawingContext(builder.Options);
            var repository = new DrawingRepository(context);

            // Pack to draw in tests
            var text = File.ReadAllText("Controllers/TestData/pack.json");
            this.drawingPack = JsonSerializer.Deserialize<Pack>(text);

            this.controller = new(
                Mock.Of<ILogger<DrawingsController>>(),
                repository);
            Assert.NotNull(this.controller);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void Post()
        {
            this.drawingPack.PackId = Guid.NewGuid().ToString();

            var result = this.controller.Post(this.drawingPack);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            Assert.True(res.RouteValues.ContainsKey("id"));
            Assert.IsType<Drawing>(res.Value);
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
            this.drawingPack.PackId = Guid.NewGuid().ToString();

            var result = this.controller.Post(this.drawingPack);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            var val1 = res.RouteValues["id"].ToString();
            Assert.IsType<Drawing>(res.Value);

            result = this.controller.Post(this.drawingPack);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            var val2 = res.RouteValues["id"].ToString();
            Assert.IsType<Drawing>(res.Value);

            Assert.NotEqual(val1, val2);
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
                this.drawingPack.PackId = id;

                var resultPost = this.controller.Post(this.drawingPack);
                var res = resultPost as CreatedAtRouteResult;
                Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
                var val = res.RouteValues["id"].ToString();
                ids.Add(val);
            }

            var result = this.controller.Get();
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<List<Drawing>>(objectResult.Value);

            var items = objectResult.Value as IList<Drawing>;
            foreach (var item in items)
            {
                if (ids.Contains(item.DrawingId))
                {
                    ids.Remove(item.DrawingId);
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
            this.drawingPack.PackId = id;
            this.drawingPack.Name = StartName;

            var result = this.controller.Post(this.drawingPack);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            var val1 = res.RouteValues["id"].ToString();

            result = this.controller.Get(val1);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.IsType<Drawing>(objectResult.Value);

            var drawing = objectResult.Value as Drawing;
            Assert.Equal(val1, drawing.DrawingId);
            Assert.Equal(StartName, drawing.Packs[0].Name);
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

        /// <summary> (Unit Test Method) deletes this object. </summary>
        [Fact]
        public void Delete()
        {
            var id = Guid.NewGuid().ToString();
            this.drawingPack.PackId = id;

            var result = this.controller.Post(this.drawingPack);
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            var res = result as CreatedAtRouteResult;
            Assert.Equal((int)HttpStatusCode.Created, res.StatusCode);
            var val = res.RouteValues["id"].ToString();
            Assert.IsType<Drawing>(res.Value);

            result = this.controller.Delete(val);
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
            Assert.Equal(id, notfound.Value);
        }
    }
}
