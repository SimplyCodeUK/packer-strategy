// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.DbInterface
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary> Interface for a database connection. </summary>
    public interface IDbContextBuilder
    {
        /// <summary> Creates the context options builder. </summary>
        ///
        /// <param name="connection"> The arguments to create the connection. </param>
        ///
        /// <returns> The context options builder. </returns>
        Action<DbContextOptionsBuilder> CreateContextOptionsBuilder(string connection);
    }
}
