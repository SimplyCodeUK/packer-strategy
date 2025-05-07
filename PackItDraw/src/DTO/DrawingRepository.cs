// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.DTO
{
    using PackItLib.Drawing;

    /// <summary> A pack repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.Repository{TData, TDtoData, TMapper}"/>
    /// <seealso cref="T:PackIt.DTO.IDrawingRepository"/>
    /// <remarks>
    /// Initialises a new instance of the <see cref="DrawingRepository" /> class.
    /// </remarks>
    ///
    /// <param name="context"> The context. </param>
    public class DrawingRepository(DrawingContext context) : PackItLib.DTO.Repository<Drawing, PackItLib.DTO.DtoDrawing.DtoDrawing, DrawingMapper>(context), IDrawingRepository
    {
    }
}
