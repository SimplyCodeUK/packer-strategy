//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Package
{
    using System.Collections.Generic;

    /// <summary>   A package. </summary>
    public class Package
    {
        /// <summary>   Default constructor. </summary>
        public Package()
        {
            this.Costings = new List<Costing>();
            this.Stages = new List<Stage>();
        }

        /// <summary>   The identifier. </summary>
        private string _id;

        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                foreach (Costing costing in Costings)
                {
                    costing.OwnerId = value;
                }
                foreach (Stage stage in Stages)
                {
                    stage.OwnerId = value;
                }
            }
        }

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
        public Material.Type MaterialType { get; set; }

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
        public List<Costing> Costings { get; set; }

        /// <summary>   Gets or sets the stages. </summary>
        ///
        /// <value> The stages. </value>
        public List<Stage> Stages
        {
            get; set;
        }
    }
}
