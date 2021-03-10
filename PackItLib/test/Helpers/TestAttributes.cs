// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.Test.Helpers
{
    using NUnit.Framework;
    using PackIt.Helpers;
    using PackIt.Helpers.Enums;

    /// <summary> (Unit Test Method) Enum attributes helpers. </summary>
    [TestFixture]
    public class TestAttributes
    {
        /// <summary> (Unit Test Method) get an enum short name. </summary>
        [Test]
        public void ShortName()
        {
            var name = Attributes.ShortName(MaterialType.UBoard);

            Assert.AreEqual("UBoard", name);
        }

        /// <summary> (Unit Test Method) get an enum name. </summary>
        [Test]
        public void Name()
        {
            var name = Attributes.Name(MaterialType.UBoard);

            Assert.AreEqual("U Board", name);
        }

        /// <summary> (Unit Test Method) get an enum url name. </summary>
        [Test]
        public void UrlName()
        {
            var name = Attributes.UrlName(MaterialType.UBoard);

            Assert.AreEqual("uboard", name);
        }

        /// <summary> (Unit Test Method) get an enum short name with no attribute. </summary>
        [Test]
        public void ShortNameNotSpecified()
        {
            var name = Attributes.ShortName(CostType.Area);

            Assert.AreEqual("Area", name);
        }

        /// <summary> (Unit Test Method) get an enum name with no attribute. </summary>
        [Test]
        public void NameNotSpecified()
        {
            var name = Attributes.Name(CostType.Area);

            Assert.AreEqual("Area", name);
        }

        /// <summary> (Unit Test Method) get an enum url name with no attribute. </summary>
        [Test]
        public void UrlNameNotSpecified()
        {
            var name = Attributes.UrlName(CostType.Area);

            Assert.AreEqual("area", name);
        }
    }
}
