// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Models
{
    using System;

    /// <summary> Exception model. </summary>
    public class ErrorViewModel
    {
        /// <summary>   Gets or sets the requested Id. </summary>
        ///
        /// <value> The position x coordinate. </value>
        public string RequestId { get; set; }

        /// <summary> Gets a flag indicating if the request id can be shown</summary>
        /// 
        /// <returns> A flag indicating if the request id can be shown.</returns>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}