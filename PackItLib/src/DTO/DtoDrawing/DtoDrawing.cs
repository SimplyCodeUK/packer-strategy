// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoDrawing
{
    using System.Collections.Generic;
    using PackIt.DTO.DtoPack;

    /// <summary> A dto drawing. </summary>
    public class DtoDrawing
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DtoDrawing" /> class.
        /// </summary>
        public DtoDrawing()
        {
            this.Packs = new List<DtoPack>();
            this.Packs.Add(new DtoPack());
            this.Shapes = new List<DtoShape3D>();
        }

        /// <summary> Gets or sets the drawing id. </summary>
        ///
        /// <value> The drawing id. </value>
        public string DrawingId { get; set; }

        /// <summary> Flag indicating if the drawing has been computed. </summary>
        ///
        /// <value> The computed state. </value>
        public bool Computed { get; set; }

        /// <summary> Flag indicating if the drawing has been computed. </summary>
        ///
        /// <value> The computed state. </value>
        public IList<DtoPack> Packs { get; set; }

        /// <summary> Gets or sets the collection of 3D shapes. </summary>
        ///
        /// <value> Collection of 3D shapes. </value>
        public IList<DtoShape3D> Shapes { get; set; }
    }
}
