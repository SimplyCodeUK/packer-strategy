// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using PackIt.Helpers.Enums;

    /// <summary> Material home view model. </summary>
    public class MaterialUpdateViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialUpdateViewModel"/> class.
        /// </summary>
        public MaterialUpdateViewModel()
        {
            this.Data = new Material();
        }

        /// <summary> Gets or sets the material data. </summary>
        ///
        /// <value> The material data. </value>
        public Material Data { get; set; }

        /// <summary> Reads asynchronously the model for a material. </summary>
        ///
        /// <param name="endpoint"> The materials service endpoint. </param>
        /// <param name="id"> The identifier of the material. </param>
        ///
        /// <returns> The model. </returns>
        public static async Task<MaterialUpdateViewModel> ReadAsync(string endpoint, string id)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint + "Materials/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

                var ret = new MaterialUpdateViewModel
                {
                    Data = JsonConvert.DeserializeObject<Material>(body)
                };

                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Data for material view model
        /// </summary>
        public class Material
        {
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

            /// <summary> Gets or sets the colour. </summary>
            ///
            /// <value> The colour. </value>
            public string Colour { get; set; }

            /// <summary> Gets or sets the drawing. </summary>
            ///
            /// <value> The drawing. </value>
            public string Drawing { get; set; }

            /// <summary> Gets or sets the bar code. </summary>
            ///
            /// <value> The bar code. </value>
            public string BarCode { get; set; }

            /// <summary> Gets or sets the computer code. </summary>
            ///
            /// <value> The computer code. </value>
            public string ComputerCode { get; set; }

            /// <summary> Gets or sets the finish. </summary>
            ///
            /// <value> The finish. </value>
            public string Finish { get; set; }

            /// <summary> Gets or sets the print. </summary>
            ///
            /// <value> The print. </value>
            public string Print { get; set; }

            /// <summary> Gets or sets the type of the print. </summary>
            ///
            /// <value> The type of the print. </value>
            public long PrintType { get; set; }

            /// <summary> Gets or sets the form. </summary>
            ///
            /// <value> The form. </value>
            public PackForm Form { get; set; }

            /// <summary> Gets or sets the type of the closure. </summary>
            ///
            /// <value> The type of the closure. </value>
            public long ClosureType { get; set; }

            /// <summary> Gets or sets the closure weight. </summary>
            ///
            /// <value> The closure weight. </value>
            public double ClosureWeight { get; set; }

            /// <summary> Gets or sets the stack step. </summary>
            ///
            /// <value> The stack step. </value>
            public double StackStep { get; set; }

            /// <summary> Gets or sets the flap. </summary>
            ///
            /// <value> The flap. </value>
            public double Flap { get; set; }

            /// <summary> Gets or sets the length of the internal. </summary>
            ///
            /// <value> The length of the internal. </value>
            public double InternalLength { get; set; }

            /// <summary> Gets or sets the internal breadth. </summary>
            ///
            /// <value> The internal breadth. </value>
            public double InternalBreadth { get; set; }

            /// <summary> Gets or sets the height of the internal. </summary>
            ///
            /// <value> The height of the internal. </value>
            public double InternalHeight { get; set; }

            /// <summary> Gets or sets the internal volume. </summary>
            ///
            /// <value> The internal volume. </value>
            public double InternalVolume { get; set; }

            /// <summary> Gets or sets the nett weight. </summary>
            ///
            /// <value> The nett weight. </value>
            public double NettWeight { get; set; }

            /// <summary> Gets or sets the length of the external. </summary>
            ///
            /// <value> The length of the external. </value>
            public double ExternalLength { get; set; }

            /// <summary> Gets or sets the external breadth. </summary>
            ///
            /// <value> The external breadth. </value>
            public double ExternalBreadth { get; set; }

            /// <summary> Gets or sets the height of the external. </summary>
            ///
            /// <value> The height of the external. </value>
            public double ExternalHeight { get; set; }

            /// <summary> Gets or sets the external volume. </summary>
            ///
            /// <value> The external volume. </value>
            public double ExternalVolume { get; set; }

            /// <summary> Gets or sets the gross weight. </summary>
            ///
            /// <value> The gross weight. </value>
            public double GrossWeight { get; set; }

            /// <summary> Gets or sets the load capacity. </summary>
            ///
            /// <value> The load capacity. </value>
            public double LoadCapacity { get; set; }

            /// <summary> Gets or sets the centre of gravity x coordinate. </summary>
            ///
            /// <value> The centre of gravity x coordinate. </value>
            public double CentreOfGravityX { get; set; }

            /// <summary> Gets or sets the centre of gravity y coordinate. </summary>
            ///
            /// <value> The centre of gravity y coordinate. </value>
            public double CentreOfGravityY { get; set; }

            /// <summary> Gets or sets the centre of gravity z coordinate. </summary>
            ///
            /// <value> The centre of gravity z coordinate. </value>
            public double CentreOfGravityZ { get; set; }

            /// <summary> Gets or sets the caliper. </summary>
            ///
            /// <value> The caliper. </value>
            public double Caliper { get; set; }

            /// <summary> Gets or sets the length of the cell. </summary>
            ///
            /// <value> The length of the cell. </value>
            public double CellLength { get; set; }

            /// <summary> Gets or sets the cell breadth. </summary>
            ///
            /// <value> The cell breadth. </value>
            public double CellBreadth { get; set; }

            /// <summary> Gets or sets the height of the cell. </summary>
            ///
            /// <value> The height of the cell. </value>
            public double CellHeight { get; set; }

            /// <summary> Gets or sets the blank area. </summary>
            ///
            /// <value> The blank area. </value>
            public double BlankArea { get; set; }

            /// <summary> Gets or sets the body tolerance. </summary>
            ///
            /// <value> The body tolerance. </value>
            public double BodyTolerance { get; set; }

            /// <summary> Gets or sets the height tolerance. </summary>
            ///
            /// <value> The height tolerance. </value>
            public double HeightTolerance { get; set; }

            /// <summary> Gets or sets the density. </summary>
            ///
            /// <value> The density. </value>
            public double Density { get; set; }

            /// <summary> Gets or sets the shoulder to top. </summary>
            ///
            /// <value> The shoulder to top. </value>
            public double ShoulderToTop { get; set; }

            /// <summary> Gets or sets the finish x coordinate. </summary>
            ///
            /// <value> The finish x coordinate. </value>
            public double FinishX { get; set; }

            /// <summary> Gets or sets the finish y coordinate. </summary>
            ///
            /// <value> The finish y coordinate. </value>
            public double FinishY { get; set; }

            /// <summary> Gets or sets the board strength. </summary>
            ///
            /// <value> The board strength. </value>
            public double BoardStrength { get; set; }

            /// <summary> Gets or sets target compression. </summary>
            ///
            /// <value> The target compression. </value>
            public double TargetCompression { get; set; }
        }
    }
}
