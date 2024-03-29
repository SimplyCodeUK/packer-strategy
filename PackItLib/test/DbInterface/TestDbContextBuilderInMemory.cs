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
    public class TestDbContextBuilderInMemory
    {
        /// <summary> (Unit Test Method) Successful database context lookup. </summary>
        [Test]
        public void CreateContextOptionsBuilder()
        {
            var context = new DbContextBuilderInMemory();
            Assert.That(context, Is.Not.Null);

            var action = context.CreateContextOptionsBuilder("material");
            Assert.That(action, Is.Not.Null);
        }
    }
}
