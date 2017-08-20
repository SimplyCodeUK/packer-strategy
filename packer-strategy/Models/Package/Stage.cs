//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Package
{
    using System.Collections.Generic;

    /// <summary>   A stage. </summary>
    public class Stage : Models.Plan.Stage
    {
        /// <summary>   Default constructor. </summary>
        public Stage()
        {
            Results = new List<Result>();
        }

        /// <summary>   Sets owner identifier. </summary>
        ///
        /// <param name="value">    The value. </param>
        protected override void SetOwnerId(string value)
        {
            base.SetOwnerId(value);
            foreach (Result result in Results)
            {
                result.OwnerId = value;
            }
        }

        /// <summary>   Gets or sets the results. </summary>
        ///
        /// <value> The results. </value>
        List<Result> Results { get; set; }
    }
}
