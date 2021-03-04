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
    public class MaterialRepository : Repository<Material, DtoMaterial.DtoMaterial, MaterialMapper>, IMaterialRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialRepository" /> class.
        /// </summary>
        ///
        /// <param name="context"> The context. </param>
        public MaterialRepository(MaterialContext context) : base(context)
        {
        }
    }
}
