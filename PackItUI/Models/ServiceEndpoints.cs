// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Models
{
    /// <summary> Service endpoint settings. </summary>
    public class ServiceEndpoints
    {
        /// <summary> Gets or sets the Materials endpoint.</summary>
        ///
        /// <value> The Materials endpoint.</value>
        public string Materials { get; set; }

        /// <summary> Gets or sets the Packs endpoint.</summary>
        ///
        /// <value> The Packs endpoint.</value>
        public string Packs { get; set; }

        /// <summary> Gets or sets the Plans endpoint.</summary>
        ///
        /// <value> The Plans endpoint.</value>
        public string Plans { get; set; }
    }
}
