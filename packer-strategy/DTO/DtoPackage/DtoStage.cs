// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoPackage
{
    using System.Collections.Generic;

    /// <summary>   A dto stage. </summary>
    public class DtoStage : DtoPlan.DtoStage
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DtoStage" /> class.
        /// </summary>
        public DtoStage()
        {
            this.Results = new List<DtoResult>();
        }

        /// <summary>   Gets or sets the results. </summary>
        ///
        /// <value> The results. </value>
        public List<DtoResult> Results { get; set; }
    }
}
