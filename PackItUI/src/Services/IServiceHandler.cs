// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Services
{
    using System.Threading.Tasks;

    /// <summary> Interface for a service. </summary>
    public interface IServiceHandler
    {
        /// <summary> Reads asynchronously the service information. </summary>
        ///
        /// <returns> The service information. </returns>
        Task<ServiceInfo> InformationAsync();
    }
}
