﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Models
{
    using PackItLib.Pack;
    using PackItLib.Helpers.Enums;
    using PackItLib.Drawing;
    using PackItDraw.DTO;
    using static PackItDraw.Models.DoDrawingHelpers;

    /// <summary> Evaluate the drawing. </summary>
    public class DoDrawing
    {
        /// <summary> The repository. </summary>
        private readonly IDrawingRepository repository;

        /// <summary> The drawing to evaluate. </summary>
        private readonly Drawing drawing;

        /// <summary> Stage to draw. </summary>
        private readonly StageLevel stageLevel;

        /// <summary> Index of result to draw in stage. </summary>
        private readonly int result;

        private Drawing Go()
        {
            var pack = this.drawing.Packs[0];
            var stage = pack.Stages[(int)this.stageLevel];
            var res = stage.Results[this.result];

            // Populate the scene
            if ((res.Generator != PatternGenerator.Variable) &&
                (stage.StageLevel != StageLevel.New))
            {
                this.DrawResult(pack, res);
            }
            else
            {
                this.DrawProduct(res);
            }
            this.drawing.Computed = true;
            this.repository.Update(this.drawing);
            return this.drawing;
        }

        /// <summary> Draw the result as a product. </summary>
        ///
        /// <param name="result">The result to draw as a product.</param>
        private void DrawProduct(Result result)
        {
            var shape = new Shape3D(FormType.Box)
            {
                Length = result.ExternalLength,
                Breadth = result.ExternalBreadth,
                Height = result.ExternalHeight
            };
            this.drawing.Shapes.Add(shape);
        }

        /// <summary> Get the parent of the result. </summary>
        ///
        /// <param name="pack">The pack that the result is drawn from.</param>
        /// <param name="result">The result to draw.</param>
        ///
        /// <returns> The parent of the result or null is one does not exist. </returns>
        private static Result GetParent(Pack pack, Result result)
        {
            foreach (var stage in pack.Stages)
            {
                if (stage.StageLevel == result.ParentLevel)
                {
                    return stage.Results[(int)result.ParentIndex];
                }
            }

            return null;
        }

        private void DrawCollation(Dims parentDimensions, Collation collation, double height)
        {
            var x = collation.PosX;
            for (var kdx = 0; kdx < collation.CountX; ++kdx)
            {
                var y = collation.PosY;
                for (var ldx = 0; ldx < collation.CountY; ++ldx)
                {
                    var shape = new Shape3D(FormType.Box)
                    {
                        Length = parentDimensions.Width,
                        Breadth = parentDimensions.Depth,
                        Height = parentDimensions.Height,
                        X = x,
                        Y = y,
                        Z = height
                    };
                    this.drawing.Shapes.Add(shape);
                }
            }
        }

        /// <summary> Draw the result. </summary>
        ///
        /// <param name="pack">The pack that the result is drawn from.</param>
        /// <param name="result">The result to draw.</param>
        private void DrawResult(Pack pack, Result result)
        {
            var parent = GetParent(pack, result);

            if (parent is not null)
            {
                var layer = result.Layers[0];
                var parentDimensions = RotateResult(parent, layer.Rotation);

                double height = 0;
                for (var idx = 0; idx < layer.Layers; ++idx)
                {
                    foreach (var collation in layer.Collations)
                    {
                        this.DrawCollation(parentDimensions, collation, height);
                    }
                    height += parentDimensions.Depth;
                }
            }
        }

        /// <summary> Start process. </summary>
        ///
        /// <param name="drawingId"> The drawing id to evaluate. </param>
        /// <param name="repository"> The repository the drawing is persistent in. </param>
        public static Drawing Start(string drawingId, IDrawingRepository repository)
        {
            var obj = new DoDrawing(drawingId, repository);
            return obj.Go();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DoDrawing" /> class.
        /// </summary>
        ///
        /// <param name="drawingId"> The drawing id to evaluate. </param>
        /// <param name="repository"> The repository the drawing is persistent in. </param>
        private DoDrawing(string drawingId, IDrawingRepository repository)
        {
            this.repository = repository;
            this.drawing = this.repository.Find(drawingId);
            this.stageLevel = StageLevel.New;

            var level = StageLevel.New;
            foreach (Stage stage in this.drawing.Packs[0].Stages)
            {
                if (stage.Results.Count != 0)
                {
                    this.stageLevel = level;
                }
                ++level;
            }
            this.result = 0;
        }
    }
}
