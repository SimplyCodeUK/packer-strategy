// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Models
{
    using System.Collections.Generic;
    using PackIt.Helpers.Enums;
    using PackIt.Material;
    using PackItUI.Helpers;

    /// <summary> Material home view model. </summary>
    public class MaterialViewModel
    {
        /// <summary> The data. </summary>
        private Material data;

        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialViewModel" /> class.
        /// </summary>
        public MaterialViewModel()
        {
            this.Data = new Material();
            this.SetSectionTypes();
        }

        /// <summary> Gets and sets the section types used in select box. </summary>
        public List<ListForFlag<SectionTypes>> SectionTypes { get; private set; }

        /// <summary> Gets or sets the material data. </summary>
        ///
        /// <value> The material data. </value>
        public Material Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
                this.SetSectionTypes();
            }
        }

        /// <summary> Sets the section types. </summary>
        private void SetSectionTypes()
        {
            this.SectionTypes = new List<ListForFlag<SectionTypes>>();
            foreach (Section section in this.Data.Sections)
            {
                this.SectionTypes.Add(new ListForFlag<SectionTypes>(section.SectionType));
            }
        }
    }
}
