// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.Areas.Materials.Models
{
    using NUnit.Framework;
    using PackItUI.Areas.Materials.Models;

    /// <summary> (Unit Test Fixture) Material edit view model. </summary>
    [TestFixture]
    public class TestMaterialEditViewModel
    {
        /// <summary> The model under test. </summary>
        private MaterialEditViewModel model;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.model = new();
            Assert.That(this.model, Is.Not.Null);
        }

        /// <summary> (Unit Test Method) set model sections. </summary>
        [Test]
        public void SetCostings()
        {
            MaterialEditViewModel.Material data = new();
            data.Costings.Add(new());
            this.model.Data = data;
            Assert.That(this.model.Data.Costings.Count, Is.EqualTo(1));
        }

        /// <summary> (Unit Test Method) set model sections. </summary>
        [Test]
        public void SetSections()
        {
            MaterialEditViewModel.Material data = new MaterialEditViewModel.Material();
            data.Sections.Add(new());
            this.model.Data = data;
            Assert.That(this.model.Data.Sections.Count, Is.EqualTo(1));
        }
    }
}
