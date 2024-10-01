// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.App.Controllers
{
    using Xunit;
    using PackItUI.Areas.App.Models;

    /// <summary> (Unit Test Fixture) a controller for handling test materials. </summary>
    public class TestErrorViewModel
    {

        /// <summary> (Unit Test Method) index action. </summary>
        [Fact]
        public void CanShowRequestId()
        {
            ErrorViewModel model = new();

            model.RequestId = "";

            Assert.Equal("", model.RequestId);
            Assert.False (model.ShowRequestId);
        }

        /// <summary> (Unit Test Method) about action when the services are not running. </summary>
        [Fact]
        public void CannotShowRequestId()
        {
            ErrorViewModel model = new();

            model.RequestId = "id";

            Assert.Equal("id", model.RequestId);
            Assert.True(model.ShowRequestId);
        }
    }
}
