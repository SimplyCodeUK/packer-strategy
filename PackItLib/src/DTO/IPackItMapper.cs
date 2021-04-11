// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    /// <summary> Maps TData from and to TDtoData </summary>
    ///
    /// <typeparam name="TData"> The type of the data. </typeparam>
    /// <typeparam name="TDtoData"> The type of the data transfer object. </typeparam>
    public interface IPackItMapper<TData, TDtoData>
    {
        /// <summary> Converts a Data to its DTO. </summary>
        /// 
        /// <param name="data"> Data to convert. </param>
        /// 
        /// <returns> The converted DTO. </returns>
        public TDtoData ConvertToDto(TData data);

        /// <summary> Converts a DTO to its Data. </summary>
        ///
        /// <param name="dtoData"> DTO to convert. </param>
        ///
        /// <returns> The converted Data. </returns>
        public TData ConvertToData(TDtoData dtoData);

        /// <summary> Get Data key. </summary>
        ///
        /// <param name="data"> Data to get the key for. </param>
        ///
        /// <returns> The key. </returns>
        public string KeyForData(TData data);
    }
}
