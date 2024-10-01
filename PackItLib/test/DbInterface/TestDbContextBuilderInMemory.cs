// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DbInterface
{
    using Xunit;
    using PackIt.DbInterface;

    /// <summary> (Unit Test Method) Database connection manager. </summary>
    public class TestDbContextBuilderInMemory
    {
        /// <summary> (Unit Test Method) Successful database context lookup. </summary>
        [Fact]
        public void CreateContextOptionsBuilder()
        {
            var context = new DbContextBuilderInMemory();
            Assert.NotNull(context);

            var action = context.CreateContextOptionsBuilder("material");
            Assert.NotNull(action);
        }
    }
}
