// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.Models
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using PackIt.DTO;
    using PackIt.Drawing;
    using PackIt.Models;
    using System.Text.Json;

    /// <summary> (Unit Test Method) Convert a Pack to it's DTO. </summary>
    [TestFixture]
    public class TestDoDrawing
    {
        private IDrawingRepository repository;
        private string drawingId;

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
            var pack = JsonSerializer.Deserialize<Pack.Pack>(text);

            Drawing value = new(pack);
            this.drawingId = value.DrawingId;
            this.repository.Add(value);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Test]
        public void TestStart()
        {
            DoDrawing.Start(this.drawingId, this.repository);
            var drawing = this.repository.Find(this.drawingId);
            Assert.IsTrue(drawing.Computed);
            Assert.AreEqual(2, drawing.Shapes.Count);
        }
    }
}
