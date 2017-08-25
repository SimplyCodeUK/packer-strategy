//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO.DtoPackage
{
    using System.Collections.Generic;

    /// <summary>   A dto stage. </summary>
    public class DtoStage : DtoPlan.DtoStage
    {
        /// <summary>   Default constructor. </summary>
        public DtoStage()
        {
            Results = new List<DtoResult>();
        }

        /// <summary>   Gets or sets the results. </summary>
        ///
        /// <value> The results. </value>
        public List<DtoResult> Results { get; set; }
    }
}
