// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.DbInterface
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary> In memory database connection. </summary>
    ///
    /// <seealso cref="PackItLib.DbInterface.IDbContextBuilder" />
    public class DbContextBuilderInMemory : IDbContextBuilder
    {
        /// <summary> Creates the context options builder for an in memory database. </summary>
        ///
        /// <param name="connection"> The arguments to create the connection. </param>
        ///
        /// <returns> The context options builder for an in memory database. </returns>
        public Action<DbContextOptionsBuilder> CreateContextOptionsBuilder(string connection)
        {
            return option => option.UseInMemoryDatabase(connection);
        }
    }
}
