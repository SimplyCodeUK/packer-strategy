// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using PackIt.Helpers.Enums;
    using PackIt.Pack;

    /// <summary> Pack edit view model. </summary>
    public class PackEditViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PackEditViewModel"/> class.
        /// </summary>
        public PackEditViewModel()
        {
            this.Data = new Pack();
        }

        /// <summary> Gets or sets the pack data. </summary>
        ///
        /// <value> The pack data. </value>
        public Pack Data { get; set; }

        /// <summary>
        /// Data for pack view model
        /// </summary>
        public class Pack
        {
            /// <summary>
            /// Initialises a new instance of the <see cref="Pack" /> class.
            /// </summary>
            public Pack()
            {
                this.Costings = new List<Costing>();
            }

            /// <summary> Gets or sets the Pack identifier. </summary>
            ///
            /// <value> The Pack identifier. </value>
            [Required]
            [Display(Name = "ID", Prompt = "Enter Pack Id")]
            public string PackId { get; set; }

            /// <summary> Gets or sets the name. </summary>
            ///
            /// <value> The name. </value>
            [Display(Prompt = "Enter Material Name")]
            public string Name { get; set; }

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
            public IList<Costing> Costings { get; set; }
        }
    }
}
