// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoPlan
{
    using System.Collections.Generic;

    /// <summary> A dto plan. </summary>
    public class DtoPlan
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DtoPlan" /> class.
        /// </summary>
        public DtoPlan()
        {
            this.Stages = new List<DtoStage>();
        }

        /// <summary> Gets or sets the Plan identifier. </summary>
        ///
        /// <value> The Plan identifier. </value>
        public string PlanId { get; set; }

        /// <summary> Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary> Gets or sets the collection of stages. </summary>
        ///
        /// <value> Collection of stages. </value>
        public IList<DtoStage> Stages { get; set; }
    }
}
