// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.App.Controllers
{
    using NUnit.Framework;
    using PackItUI.Areas.App.Models;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    [TestFixture]
    public class TestErrorViewModel
    {

        /// <summary> (Unit Test Method) index action. </summary>
        [Test]
        public void CanShowRequestId()
        {
            ErrorViewModel model = new();

            model.RequestId = "";

            Assert.AreEqual("", model.RequestId);
            Assert.False(model.ShowRequestId);
        }

        /// <summary> (Unit Test Method) about action when the services are not running. </summary>
        [Test]
        public void CannotShowRequestId()
        {
            ErrorViewModel model = new();

            model.RequestId = "id";

            Assert.AreEqual("id", model.RequestId);
            Assert.True(model.ShowRequestId);
        }
    }
}
