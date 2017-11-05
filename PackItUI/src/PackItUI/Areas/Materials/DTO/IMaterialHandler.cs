// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.DTO
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PackItUI.Services;

    /// <summary> Interface for material I/O. </summary>
    public interface IMaterialHandler : IServiceHandler
    {
        /// <summary> Creates asynchronously a material. </summary>
        ///
        /// <param name="data"> The material to save. </param>
        ///
        /// <returns> True if successful. </returns>
        Task<bool> CreateAsync(PackIt.Material.Material data);

        /// <summary> Read asynchronously all materials. </summary>
        ///
        /// <returns> A list of all the materials. </returns>
        Task<IList<PackIt.Material.Material>> ReadAsync();

        /// <summary> Reads asynchronously a material. </summary>
        ///
        /// <param name="id"> The identifier of the material. </param>
        ///
        /// <returns> The material or null id the material could not be found. </returns>
        Task<PackIt.Material.Material> ReadAsync(string id);

        /// <summary> Updates asynchronously a material. </summary>
        ///
        /// <param name="id"> The id of the material. </param>
        /// <param name="data"> The material to update. </param>
        ///
        /// <returns> True if successful. </returns>
        Task<bool> UpdateAsync(string id, PackIt.Material.Material data);

        /// <summary> Deletes asynchronously a material. </summary>
        ///
        /// <param name="id"> The id of the material. </param>
        ///
        /// <returns> True if successful. </returns>
        Task<bool> DeleteAsync(string id);
    }
}
