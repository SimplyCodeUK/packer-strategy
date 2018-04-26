// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.DTO
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PackItUI.Services;

    /// <summary> Interface for pack I/O. </summary>
    public interface IPackHandler : IServiceHandler
    {
        /// <summary> Creates asynchronously a pack. </summary>
        ///
        /// <param name="data"> The pack to save. </param>
        ///
        /// <returns> True if successful. </returns>
        Task<bool> CreateAsync(PackIt.Pack.Pack data);

        /// <summary> Read asynchronously all packs. </summary>
        ///
        /// <returns> A list of all the packs. </returns>
        Task<IList<PackIt.Pack.Pack>> ReadAsync();

        /// <summary> Reads asynchronously a pack. </summary>
        ///
        /// <param name="id"> The identifier of the pack. </param>
        ///
        /// <returns> The pack or null id the pack could not be found. </returns>
        Task<PackIt.Pack.Pack> ReadAsync(string id);

        /// <summary> Updates asynchronously a pack. </summary>
        ///
        /// <param name="id"> The id of the pack. </param>
        /// <param name="data"> The pack to update. </param>
        ///
        /// <returns> True if successful. </returns>
        Task<bool> UpdateAsync(string id, PackIt.Pack.Pack data);

        /// <summary> Deletes asynchronously a pack. </summary>
        ///
        /// <param name="id"> The id of the pack. </param>
        ///
        /// <returns> True if successful. </returns>
        Task<bool> DeleteAsync(string id);
    }
}
