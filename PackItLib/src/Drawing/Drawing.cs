// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary> A drawing. </summary>
    [ExcludeFromCodeCoverage]
    public class Drawing
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Drawing" /> class.
        /// </summary>
        public Drawing()
        {
            this.DrawingId = Guid.NewGuid().ToString();
            this.Packs = new List<Pack.Pack>();
            this.Packs.Add(new Pack.Pack());
            this.Computed = false;
            this.Shapes = new List<Shape3D>();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Drawing" /> class.
        /// </summary>
        ///
        /// <param name="pack"> The pack. </param>
        public Drawing(Pack.Pack pack) : this()
        {
            this.Packs[0] = pack;
        }

        /// <summary> Gets or sets the drawing id. </summary>
        ///
        /// <value> The drawing id. </value>
        public string DrawingId { get; private set; }

        /// <summary> Flag indicating if the drawing has been computed. </summary>
        ///
        /// <value> The computed state. </value>
        public bool Computed { get; set; }

        /// <summary> The pack to compute the drawing for. </summary>
        ///
        /// <value> The Pack. </value>
        ///
        /// <see cref="Pack.Pack"/>
        public IList<Pack.Pack> Packs { get; set; }

        /// <summary> Gets or sets the collection of 3D shapes. </summary>
        ///
        /// <value> Collection of 3D shapes. </value>
        public IList<Shape3D> Shapes { get; set; }
    }
}
