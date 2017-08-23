//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO.PlanDTO
{
    using System.Collections.Generic;

    /// <summary>   A plan. </summary>
    public class PlanDTO
    {
        /// <summary>   Default constructor. </summary>
        public PlanDTO()
        {
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

        /// <summary>   Gets or sets the notes. </summary>
        ///
        /// <value> The notes. </value>
        public string Notes { get; set; }

        /// <summary>   Gets or sets the stages. </summary>
        ///
        /// <value> The stages. </value>
        public List<StageDTO> Stages { get; set; }
    }
}
