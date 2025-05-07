// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using PackItLib.Pack;

    /// <summary> A pack repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.Repository{TData, TDtoData, TMapper}"/>
    /// <seealso cref="T:PackIt.DTO.IPackRepository"/>
    /// <remarks>
    /// Initialises a new instance of the <see cref="PackRepository" /> class.
    /// </remarks>
    ///
    /// <param name="context"> The context. </param>
    public class PackRepository(PackContext context) : PackItLib.DTO.Repository<Pack, PackItLib.DTO.DtoPack.DtoPack, PackMapper>(context), IPackRepository
    {
    }
}
