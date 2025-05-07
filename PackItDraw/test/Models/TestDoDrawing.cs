// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Test.Models
{
    using System.IO;
    using System.Text.Json;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    using PackItLib.Drawing;
    using PackItDraw.DTO;
    using PackItDraw.Models;

    /// <summary> (Unit Test Method) Convert a Pack to it's DTO. </summary>
    public class TestDoDrawing
    {
        private readonly DrawingRepository repository;
        private readonly PackItLib.Pack.Pack pack;

        /// <summary> Setup for all unit tests here. </summary>
        public TestDoDrawing()
        {
            var builder = new DbContextOptionsBuilder<DrawingContext>();
            builder.EnableSensitiveDataLogging();
            builder.UseInMemoryDatabase("testdrawing");

            var context = new DrawingContext(builder.Options);
            this.repository = new DrawingRepository(context);

            // Pack to draw in tests
            var text = File.ReadAllText("Models/TestData/pack.json");
            this.pack = JsonSerializer.Deserialize<PackItLib.Pack.Pack>(text);
        }

        /// <summary> (Unit Test Method) post this message. </summary>
        [Fact]
        public void TestStart()
        {
            Drawing value = new(this.pack);
            string drawingId = value.DrawingId;
            this.repository.Add(value);

            DoDrawing.Start(drawingId, this.repository);
            var drawing = this.repository.Find(drawingId);
            Assert.True(drawing.Computed);
            Assert.Equal(2, drawing.Shapes.Count);
        }
    }
}
