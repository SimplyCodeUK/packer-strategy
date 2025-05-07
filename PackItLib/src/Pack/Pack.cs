// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.Pack
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using PackItLib.Helpers.Enums;

    /// <summary> A pack. </summary>
    [ExcludeFromCodeCoverage]
    public class Pack
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Pack" /> class.
        /// </summary>
        public Pack()
        {
            this.Costings = new List<Costing>();
            this.Stages = new List<Stage>();
        }

        /// <summary> Gets or sets the identifier of the Pack. </summary>
        ///
        /// <value> The identifier of the Pack. </value>
        [Required]
        [Display(Name = "ID", Prompt = "Enter Pack Id")]
        public string PackId { get; set; }

        /// <summary> Gets or sets the name. </summary>
        ///
        /// <value> The pack name. </value>
        [Display(Prompt = "Enter Pack Name")]
        public string Name { get; set; }

        /// <summary> Gets or sets the plan code. </summary>
        ///
        /// <value> The plan code. </value>
        [Display(Prompt = "Enter Plan Code")]
        public string PlanCode { get; set; }

        /// <summary> Gets or sets the name of the plan. </summary>
        ///
        /// <value> The name of the plan. </value>
        [Display(Prompt = "Enter Plan Name")]
        public string PlanName { get; set; }

        /// <summary> Gets or sets the type of the material. </summary>
        ///
        /// <value> The type of the material. </value>
        public MaterialType MaterialType { get; set; }

        /// <summary> Gets or sets the material code. </summary>
        ///
        /// <value> The material code. </value>
        [Display(Prompt = "Enter Material Code")]
        public string MaterialCode { get; set; }

        /// <summary> Gets or sets the name of the material. </summary>
        ///
        /// <value> The name of the material. </value>
        [Display(Prompt = "Enter Material Name")]
        public string MaterialName { get; set; }

        /// <summary> Gets or sets the collection of costings. </summary>
        ///
        /// <value> Collection of costings. </value>
        public IList<Costing> Costings { get; set; }

        /// <summary> Gets or sets the collection of stages. </summary>
        ///
        /// <value> Collection of stages. </value>
        public IList<Stage> Stages { get; set; }
    }
}
