// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DbInterface
{
    using NUnit.Framework;
    using PackIt.DbInterface;

    /// <summary> (Unit Test Method) Database connection manager. </summary>
    [TestFixture]
    public class TestDbConnectionManager
    {
        /// <summary> (Unit Test Method) Successful database context lookup. </summary>
        [Test]
        public void DatabaseLookupOk()
        {
            var manager = new DbConnectionManager();
            var inmemory = new DbContextBuilderInMemory();
            var postgres = new DbContextBuilderPostgres();
            Assert.That(manager, Is.Not.Null);
            Assert.That(inmemory, Is.Not.Null);
            Assert.That(postgres, Is.Not.Null);

            manager.RegisterContextBuilder("inmemory", inmemory);
            manager.RegisterContextBuilder("postgres", postgres);

            Assert.That(manager.ContextBuilder("inmemory"), Is.EqualTo(inmemory));
            Assert.That(manager.ContextBuilder("postgres"), Is.EqualTo(postgres));
        }

        /// <summary> (Unit Test Method) Failed database context lookup. </summary>
        [Test]
        public void DatabaseLookupFail()
        {
            var manager = new DbConnectionManager();
            var inmemory = new DbContextBuilderInMemory();
            var postgres = new DbContextBuilderPostgres();
            Assert.That(manager, Is.Not.Null);
            Assert.That(inmemory, Is.Not.Null);
            Assert.That(postgres, Is.Not.Null);

            manager.RegisterContextBuilder("inmemory", inmemory);
            manager.RegisterContextBuilder("postgres", postgres);

            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => manager.ContextBuilder("unknown"));
        }
    }
}
