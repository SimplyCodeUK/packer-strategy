//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO.PackageDTO
{
    using System.Collections.Generic;

    /// <summary>   A stage. </summary>
    public class StageDTO : PlanDTO.StageDTO
    {
        /// <summary>   Default constructor. </summary>
        public StageDTO()
        {
            Results = new List<ResultDTO>();
        }

        /// <summary>   Gets or sets the results. </summary>
        ///
        /// <value> The results. </value>
        public List<ResultDTO> Results { get; set; }
    }
}
