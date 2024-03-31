// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using PackIt.Material;

    /// <summary> A material repository. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.Repository{TData, TDtoData, TMapper}"/>
    /// <seealso cref="T:PackIt.DTO.IMaterialRepository"/>
    /// <remarks>
    /// Initialises a new instance of the <see cref="MaterialRepository" /> class.
    /// </remarks>
    ///
    /// <param name="context"> The context. </param>
    public class MaterialRepository(MaterialContext context) : Repository<Material, DtoMaterial.DtoMaterial, MaterialMapper>(context), IMaterialRepository
    {
    }
}
