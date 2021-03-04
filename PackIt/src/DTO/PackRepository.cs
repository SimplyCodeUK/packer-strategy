// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using PackIt.Pack;

    /// <summary> A pack repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.Repository{TData, TDtoData, TMapper}"/>
    /// <seealso cref="T:PackIt.DTO.IPackRepository"/>
    public class PackRepository : Repository<Pack, DtoPack.DtoPack, PackMapper>, IPackRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PackRepository" /> class.
        /// </summary>
        ///
        /// <param name="context"> The context. </param>
        public PackRepository(PackContext context) : base(context)
        {
        }
    }
}
