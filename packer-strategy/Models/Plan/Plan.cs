//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Plan
{
    using System.Collections.Generic;

    /// <summary>   A plan. </summary>
    public class Plan
    {
        /// <summary>   Default constructor. </summary>
        public Plan()
        {
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
                foreach (Stage stage in Stages)
                {
                    stage.PlanId = value;
                }
            }
        }

        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        public string Name
        {
            get; set;
        }

        /// <summary>   Gets or sets the notes. </summary>
        ///
        /// <value> The notes. </value>
        public string Notes
        {
            get; set;
        }

        /// <summary>   Gets or sets the stages. </summary>
        ///
        /// <value> The stages. </value>
        public List<Stage> Stages
        {
            get; set;
        }
    }
}
