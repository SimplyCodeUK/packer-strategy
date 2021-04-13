// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Drawing
{
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
            this.Pack = new Pack.Pack();
        }

        /// <summary> Gets or sets the drawing id. </summary>
        ///
        /// <value> The drawing id. </value>
        public string DrawingId { get; set; }

        /// <summary> Flag indicating if the drawing has been computed. </summary>
        ///
        /// <value> The computed state. </value>
        public bool Computed { get; set; }

        /// <summary> The pack to compute the drawing for. </summary>
        ///
        /// <value> The Pack. </value>
        ///
        /// <see cref="Pack.Pack"/>
        public Pack.Pack Pack { get; set; }
    }
}
