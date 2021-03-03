// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    /// <summary>
    /// Maps from and to DtoPack
    /// </summary>
    public abstract class PackItMapper<TData, TDtoData>
    {
        /// <summary> Converts a Pack to its DTO. </summary>
        /// 
        /// <param name="pack"> Pack to convert. </param>
        /// 
        /// <returns> The converted dto. </returns>
        public abstract TDtoData ConvertToDto(TData pack);

        /// <summary> Converts a DTO to its Plan. </summary>
        ///
        /// <param name="pack"> Dto to convert. </param>
        ///
        /// <returns> The converted plan. </returns>
        public abstract TData ConvertToData(TDtoData pack);
    }
}
