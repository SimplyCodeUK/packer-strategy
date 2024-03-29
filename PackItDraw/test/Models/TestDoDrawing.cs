﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Test.Models
{
    using System.IO;
    using System.Text.Json;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using PackIt.DTO;
    using PackIt.Drawing;
    using PackIt.Models;

    /// <summary> (Unit Test Method) Convert a Pack to it's DTO. </summary>
    [TestFixture]
    public class TestDoDrawing
    {
        private IDrawingRepository repository;
        private PackIt.Pack.Pack pack;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            var builder = new DbContextOptionsBuilder<DrawingContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testdrawing");

            var context = new DrawingContext(builder.Options);
            this.repository = new DrawingRepository(context);

            // Pack to draw in tests
            var text = File.ReadAllText("Models/TestData/pack.json");
            this.pack = JsonSerializer.Deserialize<PackIt.Pack.Pack>(text);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void TestStart()
        {
            Drawing value = new(this.pack);
            string drawingId = value.DrawingId;
            this.repository.Add(value);

            DoDrawing.Start(drawingId, this.repository);
            var drawing = this.repository.Find(drawingId);
            Assert.That(drawing.Computed, Is.True);
            Assert.That(drawing.Shapes.Count, Is.EqualTo(2));
        }
    }
}
