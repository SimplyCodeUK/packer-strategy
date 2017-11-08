// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using PackIt.Helpers.Enums;
    using PackIt.Material;
    using PackItUI.Helpers;

    /// <summary> Material home view model. </summary>
    public class MaterialUpdateViewModel
    {
        /// <summary> The data. </summary>
        private Material data;

        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialUpdateViewModel"/> class.
        /// </summary>
        public MaterialUpdateViewModel()
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

        /// <summary> Data for material view model. </summary>
        public class Material
        {
            /// <summary>
            /// Initialises a new instance of the <see cref="Material" /> class.
            /// </summary>
            public Material()
            {
                this.Type = MaterialType.Bottle;
                this.Form = FormType.Bottle;
                this.Costings = new List<Costing>();
                this.Sections = new List<Section>();
            }

            /// <summary> Gets or sets the Material identifier. </summary>
            ///
            /// <value> The Material identifier. </value>
            [Required]
            [Display(Name = "ID", Prompt = "Enter Material Id")]
            public string MaterialId { get; set; }

            /// <summary> Gets or sets the type of the Material. </summary>
            ///
            /// <value> The type of the Material. </value>
            [Display(Prompt = "Enter Material Type")]
            public MaterialType Type { get; set; }

            /// <summary> Gets or sets the name. </summary>
            ///
            /// <value> The name. </value>
            [Display(Prompt = "Enter Material Name")]
            public string Name { get; set; }

            /// <summary> Gets or sets the notes. </summary>
            ///
            /// <value> The notes. </value>
            [Display(Prompt = "Enter Material Notes")]
            public string Notes { get; set; }

            /// <summary> Gets or sets the form. </summary>
            ///
            /// <value> The form. </value>
            public FormType Form { get; set; }

            /// <summary> Gets or sets the type of the closure. </summary>
            ///
            /// <value> The type of the closure. </value>
            public long ClosureType { get; set; }

            /// <summary> Gets or sets the closure weight. </summary>
            ///
            /// <value> The closure weight. </value>
            [Display(Name = "Closure Weight", Prompt = "Enter Closure Weight")]
            public double ClosureWeight { get; set; }

            /// <summary> Gets or sets the stack step. </summary>
            ///
            /// <value> The stack step. </value>
            [Display(Name = "Stack Step", Prompt = "Enter Stack Step")]
            public double StackStep { get; set; }

            /// <summary> Gets or sets the flap. </summary>
            ///
            /// <value> The flap. </value>
            [Display(Prompt = "Enter Flap Size")]
            public double Flap { get; set; }

            /// <summary> Gets or sets the length of the internal. </summary>
            ///
            /// <value> The length of the internal. </value>
            [Display(Name = "Internal Length", Prompt = "Enter Internal Length")]
            public double InternalLength { get; set; }

            /// <summary> Gets or sets the internal breadth. </summary>
            ///
            /// <value> The internal breadth. </value>
            [Display(Name = "Internal Breadth", Prompt = "Enter Internal Breadth")]
            public double InternalBreadth { get; set; }

            /// <summary> Gets or sets the height of the internal. </summary>
            ///
            /// <value> The height of the internal. </value>
            [Display(Name = "Internal Height", Prompt = "Enter Internal Height")]
            public double InternalHeight { get; set; }

            /// <summary> Gets or sets the internal volume. </summary>
            ///
            /// <value> The internal volume. </value>
            [Display(Name = "Internal Colume", Prompt = "Enter Internal Volume")]
            public double InternalVolume { get; set; }

            /// <summary> Gets or sets the nett weight. </summary>
            ///
            /// <value> The nett weight. </value>
            [Display(Name = "Nett Weight", Prompt = "Enter Nett Weight")]
            public double NettWeight { get; set; }

            /// <summary> Gets or sets the length of the external. </summary>
            ///
            /// <value> The length of the external. </value>
            [Display(Name = "External Length", Prompt = "Enter External Length")]
            public double ExternalLength { get; set; }

            /// <summary> Gets or sets the external breadth. </summary>
            ///
            /// <value> The external breadth. </value>
            [Display(Name = "External Breadth", Prompt = "Enter External Breadth")]
            public double ExternalBreadth { get; set; }

            /// <summary> Gets or sets the height of the external. </summary>
            ///
            /// <value> The height of the external. </value>
            [Display(Name = "External Height", Prompt = "Enter External Height")]
            public double ExternalHeight { get; set; }

            /// <summary> Gets or sets the external volume. </summary>
            ///
            /// <value> The external volume. </value>
            [Display(Name = "External Volume", Prompt = "Enter External Volume")]
            public double ExternalVolume { get; set; }

            /// <summary> Gets or sets the gross weight. </summary>
            ///
            /// <value> The gross weight. </value>
            [Display(Name = "Gross Weight", Prompt = "Enter Gross Weight")]
            public double GrossWeight { get; set; }

            /// <summary> Gets or sets the load capacity. </summary>
            ///
            /// <value> The load capacity. </value>
            [Display(Name = "Load Capacity", Prompt = "Enter Load Capacity")]
            public double LoadCapacity { get; set; }

            /// <summary> Gets or sets the centre of gravity x coordinate. </summary>
            ///
            /// <value> The centre of gravity x coordinate. </value>
            [Display(Name = "Length Center Of Gravity", Prompt = "Enter Center Of Gravity On The Length")]
            public double CentreOfGravityX { get; set; }

            /// <summary> Gets or sets the centre of gravity y coordinate. </summary>
            ///
            /// <value> The centre of gravity y coordinate. </value>
            [Display(Name = "Breadth Center Of Gravity", Prompt = "Enter Center Of Gravity On The Breadth")]
            public double CentreOfGravityY { get; set; }

            /// <summary> Gets or sets the centre of gravity z coordinate. </summary>
            ///
            /// <value> The centre of gravity z coordinate. </value>
            [Display(Name = "Height Center Of Gravity", Prompt = "Enter Center Of Gravity On The Height")]
            public double CentreOfGravityZ { get; set; }

            /// <summary> Gets or sets the caliper. </summary>
            ///
            /// <value> The caliper. </value>
            [Display(Prompt = "Enter Material Caliper")]
            public double Caliper { get; set; }

            /// <summary> Gets or sets the length of the cell. </summary>
            ///
            /// <value> The length of the cell. </value>
            [Display(Name = "Cell Length", Prompt = "Enter Cell Length")]
            public double CellLength { get; set; }

            /// <summary> Gets or sets the cell breadth. </summary>
            ///
            /// <value> The cell breadth. </value>
            [Display(Name = "Cell Breadth", Prompt = "Enter Cell Breadth")]
            public double CellBreadth { get; set; }

            /// <summary> Gets or sets the height of the cell. </summary>
            ///
            /// <value> The height of the cell. </value>
            [Display(Name = "Cell Height", Prompt = "Enter Cell Height")]
            public double CellHeight { get; set; }

            /// <summary> Gets or sets the blank area. </summary>
            ///
            /// <value> The blank area. </value>
            [Display(Name = "Blank Area", Prompt = "Enter Blank Area")]
            public double BlankArea { get; set; }

            /// <summary> Gets or sets the density. </summary>
            ///
            /// <value> The density. </value>
            [Display(Prompt = "Enter Material Density")]
            public double Density { get; set; }

            /// <summary> Gets or sets the board strength. </summary>
            ///
            /// <value> The board strength. </value>
            [Display(Name = "Board Strength", Prompt = "Enter Board Strength")]
            public double BoardStrength { get; set; }

            /// <summary> Gets or sets target compression. </summary>
            ///
            /// <value> The target compression. </value>
            [Display(Name = "Target Compression", Prompt = "Enter Target Compression")]
            public double TargetCompression { get; set; }

            /// <summary> Gets or sets the collection of costings. </summary>
            ///
            /// <value> Collection of costings. </value>
            public IList<Costing> Costings { get; set; }

            /// <summary> Gets or sets the collection of sections. </summary>
            ///
            /// <value> Collection of sections. </value>
            public IList<Section> Sections { get; set; }
        }
    }
}
