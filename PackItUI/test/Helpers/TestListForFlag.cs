// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Helpers
{
    using NUnit.Framework;
    using PackItUI.Helpers;
    using System;

    /// <summary> (Unit Test Fixture) helper for enums. </summary>
    [TestFixture]
    public class TestListForFlag
    {
        private enum TestEnum { t1, t2 }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Test]
        public void EnumValid()
        {
            ListForFlag<TestEnum> test;
            Assert.DoesNotThrow(delegate { test = new ListForFlag<TestEnum>(0); });
        }

        /// <summary> (Unit Test Method) index action when the service is down. </summary>
        [Test]
        public void EnumInvalid()
        {
            ListForFlag<int> test;
            Assert.Throws<ArgumentException>(delegate { test = new ListForFlag<int>(0); });
        }
    }
}
