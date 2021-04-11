// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using PackIt.Pack;

    /// <summary> A pack repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.Repository{TData, TDtoData, TMapper}"/>
    /// <seealso cref="T:PackIt.DTO.IDrawingRepository"/>
    public class DrawingRepository : Repository<Pack, DtoPack.DtoPack, DrawingMapper>, IDrawingRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DrawingRepository" /> class.
        /// </summary>
        ///
        /// <param name="context"> The context. </param>
        public DrawingRepository(DrawingContext context) : base(context)
        {
        }
    }
}
