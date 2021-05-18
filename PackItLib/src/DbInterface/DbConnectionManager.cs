// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DbInterface
{
    using System.Collections.Generic;

    /// <summary> The database connection manager. </summary>
    public class DbConnectionManager
    {
        /// <summary> The registered connections. </summary>
        private readonly Dictionary<string, IDbContextBuilder> connections;

        /// <summary> Initialises a new instance of the <see cref="DbConnectionManager"/> class. </summary>
        public DbConnectionManager()
        {
            this.connections = new();
        }

        /// <summary> Registers a context builder. </summary>
        ///
        /// <param name="key"> The key. </param>
        /// <param name="connection"> The connection. </param>
        public void RegisterContextBuilder(string key, IDbContextBuilder connection)
        {
            this.connections[key] = connection;
        }

        /// <summary> Get a connection. </summary>
        ///
        /// <param name="key"> The key to the connection. </param>
        ///
        /// <returns> The connection. </returns>
        ///
        /// <exception cref="KeyNotFoundException"> Thrown when key not registered. </exception>
        public IDbContextBuilder ContextBuilder(string key)
        {
            return this.connections[key];
        }
    }
}
