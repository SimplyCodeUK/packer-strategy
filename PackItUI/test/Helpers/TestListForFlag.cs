// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Helpers
{
    using System;
    using Xunit;
    using PackItUI.Helpers;

    /// <summary> (Unit Test Fixture) helper for enums. </summary>
    public class TestListForFlag
    {
        private enum TestType { t1, t2 }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Fact]
        public void EnumValid()
        {
            ListForFlag<TestType> test;

            var exception = Record.Exception(() => { test = new(0); });
            Assert.Null(exception);
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Fact]
        public void EnumInvalid()
        {
            ListForFlag<int> test;
            Assert.Throws<ArgumentException>(delegate { test = new(0); });
        }
    }
}
