// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.Materials.Models
{
    using Xunit;
    using PackItUI.Areas.Materials.Models;

    /// <summary> (Unit Test Fixture) Material edit view model. </summary>
    public class TestMaterialEditViewModel
    {
        /// <summary> The model under test. </summary>
        private readonly MaterialEditViewModel model;

        /// <summary> Setup for all unit tests here. </summary>
        public TestMaterialEditViewModel()
        {
            this.model = new();
            Assert.NotNull(this.model);
        }

        /// <summary> (Unit Test Method) set model sections. </summary>
        [Fact]
        public void SetCostings()
        {
            MaterialEditViewModel.Material data = new();
            Assert.NotNull(data);
            data.Costings.Add(new());
            this.model.Data = data;
            Assert.Single(this.model.Data.Costings);
        }

        /// <summary> (Unit Test Method) set model sections. </summary>
        [Fact]
        public void SetSections()
        {
            MaterialEditViewModel.Material data = new MaterialEditViewModel.Material();
            data.Sections.Add(new());
            this.model.Data = data;
            Assert.Single(this.model.Data.Sections);
        }
    }
}
