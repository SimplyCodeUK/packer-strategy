// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.Test.DbInterface
{
    using Xunit;
    using PackItLib.DbInterface;

    /// <summary> (Unit Test Method) Database connection manager. </summary>
    public class TestDbConnectionManager
    {
        /// <summary> (Unit Test Method) Successful database context lookup. </summary>
        [Fact]
        public void DatabaseLookupOk()
        {
            var manager = new DbConnectionManager();
            var inmemory = new DbContextBuilderInMemory();
            var postgres = new DbContextBuilderPostgres();
            Assert.NotNull(manager);
            Assert.NotNull(inmemory);
            Assert.NotNull(postgres);

            manager.RegisterContextBuilder("inmemory", inmemory);
            manager.RegisterContextBuilder("postgres", postgres);

            Assert.Equal(inmemory, manager.ContextBuilder("inmemory"));
            Assert.Equal(postgres, manager.ContextBuilder("postgres"));
        }

        /// <summary> (Unit Test Method) Failed database context lookup. </summary>
        [Fact]
        public void DatabaseLookupFail()
        {
            var manager = new DbConnectionManager();
            var inmemory = new DbContextBuilderInMemory();
            var postgres = new DbContextBuilderPostgres();
            Assert.NotNull(manager);
            Assert.NotNull(inmemory);
            Assert.NotNull(postgres);

            manager.RegisterContextBuilder("inmemory", inmemory);
            manager.RegisterContextBuilder("postgres", postgres);

            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => manager.ContextBuilder("unknown"));
        }
    }
}
