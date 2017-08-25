//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO.PackageDTO
{
    using System.Collections.Generic;

    /// <summary>   A package. </summary>
    public class PackageDTO
    {
        /// <summary>   Default constructor. </summary>
        public PackageDTO()
        {
            this.Costings = new List<CostingDTO>();
            this.Stages = new List<StageDTO>();
        }

        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        public string Id { get; set; }

        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>   Gets or sets the plan code. </summary>
        ///
        /// <value> The plan code. </value>
        public string PlanCode { get; set; }

        /// <summary>   Gets or sets the name of the plan. </summary>
        ///
        /// <value> The name of the plan. </value>
        public string PlanName { get; set; }

        /// <summary>   Gets or sets the type of the material. </summary>
        ///
        /// <value> The type of the material. </value>
        public Models.Material.MaterialType MaterialType { get; set; }

        /// <summary>   Gets or sets the material code. </summary>
        ///
        /// <value> The material code. </value>
        public string MaterialCode { get; set; }

        /// <summary>   Gets or sets the name of the material. </summary>
        ///
        /// <value> The name of the material. </value>
        public string MaterialName { get; set; }

        /// <summary>   Gets or sets the costing premium. </summary>
        ///
        /// <value> The costing premium. </value>
        public double CostingPremium { get; set; }

        /// <summary>   Gets or sets the costings. </summary>
        ///
        /// <value> The costings. </value>
        public List<CostingDTO> Costings { get; set; }

        /// <summary>   Gets or sets the stages. </summary>
        ///
        /// <value> The stages. </value>
        public List<StageDTO> Stages { get; set; }
    }
}
