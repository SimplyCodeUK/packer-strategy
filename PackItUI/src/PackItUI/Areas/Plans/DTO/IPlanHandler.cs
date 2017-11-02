// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.DTO
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PackItUI.Services;

    /// <summary> Interface for plan I/O. </summary>
    public interface IPlanHandler
    {
        /// <summary> Reads asynchronously the service information. </summary>
        ///
        /// <returns> The service information. </returns>
        Task<ServiceInfo> InformationAsync();

        /// <summary> Creates asynchronously a plan. </summary>
        ///
        /// <param name="data"> The plan to save. </param>
        ///
        /// <returns> True if successful. </returns>
        Task<bool> CreateAsync(PackIt.Plan.Plan data);

        /// <summary> Read asynchronously all plans. </summary>
        ///
        /// <returns> A list of all the plans. </returns>
        Task<IList<PackIt.Plan.Plan>> ReadAsync();

        /// <summary> Reads asynchronously a plan. </summary>
        ///
        /// <param name="id"> The identifier of the plan. </param>
        ///
        /// <returns> The plan or null id the plan could not be found. </returns>
        Task<PackIt.Plan.Plan> ReadAsync(string id);

        /// <summary> Updates asynchronously a plan. </summary>
        ///
        /// <param name="id"> The id of the plan. </param>
        /// <param name="data"> The plan to update. </param>
        ///
        /// <returns> True if successful. </returns>
        Task<bool> UpdateAsync(string id, PackIt.Plan.Plan data);

        /// <summary> Deletes asynchronously a plan. </summary>
        ///
        /// <param name="id"> The id of the plan. </param>
        ///
        /// <returns> True if successful. </returns>
        Task<bool> DeleteAsync(string id);
    }
}
