// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoPack
{
    using System.Collections.Generic;
    using PackIt.Helpers.Enums;

    /// <summary> A dto pack. </summary>
    public class DtoPack
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DtoPack" /> class.
        /// </summary>
        public DtoPack()
        {
            this.Costings = new List<DtoCosting>();
            this.Stages = new List<DtoStage>();
        }

        /// <summary> Gets or sets the Pack identifier. </summary>
        ///
        /// <value> The Pack identifier. </value>
        public string PackId { get; set; }

        /// <summary> Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary> Gets or sets the notes. </summary>
        ///
        /// <value> The notes. </value>
        public string Notes { get; set; }

        /// <summary> Gets or sets the plan code. </summary>
        ///
        /// <value> The plan code. </value>
        public string PlanCode { get; set; }

        /// <summary> Gets or sets the name of the plan. </summary>
        ///
        /// <value> The name of the plan. </value>
        public string PlanName { get; set; }

        /// <summary> Gets or sets the type of the material. </summary>
        ///
        /// <value> The type of the material. </value>
        public MaterialType MaterialType { get; set; }

        /// <summary> Gets or sets the material code. </summary>
        ///
        /// <value> The material code. </value>
        public string MaterialCode { get; set; }

        /// <summary> Gets or sets the name of the material. </summary>
        ///
        /// <value> The name of the material. </value>
        public string MaterialName { get; set; }

        /// <summary> Gets or sets the collection of costings. </summary>
        ///
        /// <value> Collection of costings. </value>
        public IList<DtoCosting> Costings { get; set; }

        /// <summary> Gets or sets the collection of stages. </summary>
        ///
        /// <value> Collection of stages. </value>
        public IList<DtoStage> Stages { get; set; }
    }
}
