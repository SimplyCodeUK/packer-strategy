// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoDrawing
{
    using PackIt.DTO.DtoPack;

    /// <summary> A dto drawing. </summary>
    public class DtoDrawing
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DtoDrawing" /> class.
        /// </summary>
        public DtoDrawing()
        {
            this.Pack = new DtoPack();
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
        public DtoPack Pack { get; set; }
    }
}
