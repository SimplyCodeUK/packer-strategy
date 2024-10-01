// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.Test.Helpers
{
    using Xunit;
    using PackIt.Helpers;
    using PackIt.Helpers.Enums;

    /// <summary> (Unit Test Method) Enum attributes helpers. </summary>
    public class TestAttributes
    {
        /// <summary> (Unit Test Method) get an enum short name. </summary>
        [Fact]
        public void ShortName()
        {
            var name = Attributes.ShortName(MaterialType.UBoard);

            Assert.Equal("UBoard", name);
        }

        /// <summary> (Unit Test Method) get an enum name. </summary>
        [Fact]
        public void Name()
        {
            var name = Attributes.Name(MaterialType.UBoard);

            Assert.Equal("U Board", name);
        }

        /// <summary> (Unit Test Method) get an enum url name. </summary>
        [Fact]
        public void UrlName()
        {
            var name = Attributes.UrlName(MaterialType.UBoard);

            Assert.Equal("uboard", name);
        }

        /// <summary> (Unit Test Method) get an enum short name with no attribute. </summary>
        [Fact]
        public void ShortNameNotSpecified()
        {
            var name = Attributes.ShortName(CostType.Area);

            Assert.Equal("Area", name);
        }

        /// <summary> (Unit Test Method) get an enum name with no attribute. </summary>
        [Fact]
        public void NameNotSpecified()
        {
            var name = Attributes.Name(CostType.Area);

            Assert.Equal("Area", name);
        }

        /// <summary> (Unit Test Method) get an enum url name with no attribute. </summary>
        [Fact]
        public void UrlNameNotSpecified()
        {
            var name = Attributes.UrlName(CostType.Area);

            Assert.Equal("area", name);
        }
    }
}
