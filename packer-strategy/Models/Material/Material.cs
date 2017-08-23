//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Models.Material
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>   Values that represent material types. </summary>
    public enum MaterialType
    {
        /// <summary>   An enum constant representing the Minimum option. </summary>
        [Display(ShortName = "Product")]
        Min,
        /// <summary>   An enum constant representing the product option. </summary>
        [Display(ShortName = "Product")]
        Product = Min,
        /// <summary>   An enum constant representing the carton option. </summary>
        [Display(ShortName = "Carton")]
        Carton,
        /// <summary>   An enum constant representing the board option. </summary>
        [Display(ShortName = "UBoard")]
        UBoard,
        /// <summary>   An enum constant representing the can option. </summary>
        [Display(ShortName = "Can")]
        Can,
        /// <summary>   An enum constant representing the bottle option. </summary>
        [Display(ShortName = "Bottle")]
        Bottle,
        /// <summary>   An enum constant representing the tub option. </summary>
        [Display(ShortName = "Tub")]
        Tub,
        /// <summary>   An enum constant representing the jar option. </summary>
        [Display(ShortName = "Jar")]
        Jar,
        /// <summary>   An enum constant representing the sleeve option. </summary>
        [Display(ShortName = "Sleeve")]
        Sleeve,
        /// <summary>   An enum constant representing the wrap around option. </summary>
        [Display(ShortName = "Wraparound")]
        WrapAround,
        /// <summary>   An enum constant representing the shrinkwrap option. </summary>
        [Display(ShortName = "Shrinkwrap")]
        Shrinkwrap,
        /// <summary>   An enum constant representing the crate option. </summary>
        [Display(ShortName = "Crate")]
        Crate,
        /// <summary>   An enum constant representing the container option. </summary>
        [Display(ShortName = "Container")]
        Container,
        /// <summary>   An enum constant representing the tray option. </summary>
        [Display(ShortName = "Tray")]
        Tray,
        /// <summary>   An enum constant representing the pad option. </summary>
        [Display(ShortName = "Pad")]
        Pad,
        /// <summary>   An enum constant representing the slipsheet option. </summary>
        [Display(ShortName = "Slipsheet")]
        Slipsheet,
        /// <summary>   An enum constant representing the pallet option. </summary>
        [Display(ShortName = "Pallet")]
        Pallet,
        /// <summary>   An enum constant representing the divider option. </summary>
        [Display(ShortName = "Divider")]
        Divider,
        /// <summary>   An enum constant representing the liner option. </summary>
        [Display(ShortName = "Liner")]
        Liner,
        /// <summary>   An enum constant representing the strapping option. </summary>
        [Display(ShortName = "Strapping")]
        Strapping,
        /// <summary>   An enum constant representing the edgeboard option. </summary>
        [Display(ShortName = "Edgeboard")]
        Edgeboard,
        /// <summary>   An enum constant representing the collar option. </summary>
        [Display(ShortName = "Collar")]
        Collar,
        /// <summary>   An enum constant representing the Capability option. </summary>
        [Display(ShortName = "Cap")]
        Cap,
        /// <summary>   An enum constant representing the lid option. </summary>
        [Display(ShortName = "Lid")]
        Lid,
        /// <summary>   An enum constant representing the seal option. </summary>
        [Display(ShortName = "Seal")]
        Seal,
        /// <summary>   An enum constant representing the Maximum option. </summary>
        Max
    }

    /// <summary>   A material. </summary>
    public class Material
    {
        /// <summary>   Default constructor. </summary>
        public Material()
        {
            this.Costings = new List<Costing>();
            this.IdType = MaterialType.Bottle;
        }

        /// <summary>   Defines the. </summary>
        private MaterialType _idType;

        /// <summary>   Gets or sets the type of the identifier. </summary>
        ///
        /// <value> The type of the identifier. </value>
        public MaterialType IdType
        {
            get { return _idType; }
            set
            {
                _idType = value;
                foreach (Costing costing in Costings)
                {
                    costing.MaterialIdType = value;
                }
            }
        }

        /// <summary>   The identifier. </summary>
        private string _id;

        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                foreach (Costing costing in Costings)
                {
                    costing.MaterialId = value;
                }
            }
        }

        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        public string Name
        {
            get; set;
        }

        /// <summary>   Gets or sets the notes. </summary>
        ///
        /// <value> The notes. </value>
        public string Notes
        {
            get; set;
        }

        /// <summary>   Gets or sets the colour. </summary>
        ///
        /// <value> The colour. </value>
        public string Colour
        {
            get; set;
        }

        /// <summary>   Gets or sets the drawing. </summary>
        ///
        /// <value> The drawing. </value>
        public string Drawing
        {
            get; set;
        }

        /// <summary>   Gets or sets the bar code. </summary>
        ///
        /// <value> The bar code. </value>
        public string BarCode
        {
            get; set;
        }

        /// <summary>   Gets or sets the computer code. </summary>
        ///
        /// <value> The computer code. </value>
        public string ComputerCode
        {
            get; set;
        }

        /// <summary>   Gets or sets the finish. </summary>
        ///
        /// <value> The finish. </value>
        public string Finish
        {
            get; set;
        }

        /// <summary>   Gets or sets the print. </summary>
        ///
        /// <value> The print. </value>
        public string Print
        {
            get; set;
        }

        /// <summary>   Gets or sets the type of the print. </summary>
        ///
        /// <value> The type of the print. </value>
        public long PrintType
        {
            get; set;
        }

        /// <summary>   Gets or sets the form. </summary>
        ///
        /// <value> The form. </value>
        public Plan.PackForm Form
        {
            get; set;
        }

        /// <summary>   Gets or sets the type of the closure. </summary>
        ///
        /// <value> The type of the closure. </value>
        public long ClosureType
        {
            get; set;
        }

        /// <summary>   Gets or sets the closure weight. </summary>
        ///
        /// <value> The closure weight. </value>
        public double ClosureWeight
        {
            get; set;
        }

        /// <summary>   Gets or sets the stack step. </summary>
        ///
        /// <value> The stack step. </value>
        public double StackStep
        {
            get; set;
        }

        /// <summary>   Gets or sets the flap. </summary>
        ///
        /// <value> The flap. </value>
        public double Flap
        {
            get; set;
        }

        /// <summary>   Gets or sets the length of the internal. </summary>
        ///
        /// <value> The length of the internal. </value>
        public double InternalLength
        {
            get; set;
        }

        /// <summary>   Gets or sets the internal breadth. </summary>
        ///
        /// <value> The internal breadth. </value>
        public double InternalBreadth
        {
            get; set;
        }

        /// <summary>   Gets or sets the height of the internal. </summary>
        ///
        /// <value> The height of the internal. </value>
        public double InternalHeight
        {
            get; set;
        }

        /// <summary>   Gets or sets the internal volume. </summary>
        ///
        /// <value> The internal volume. </value>
        public double InternalVolume
        {
            get; set;
        }

        /// <summary>   Gets or sets the nett weight. </summary>
        ///
        /// <value> The nett weight. </value>
        public double NettWeight
        {
            get; set;
        }

        /// <summary>   Gets or sets the length of the external. </summary>
        ///
        /// <value> The length of the external. </value>
        public double ExternalLength
        {
            get; set;
        }

        /// <summary>   Gets or sets the external breadth. </summary>
        ///
        /// <value> The external breadth. </value>
        public double ExternalBreadth
        {
            get; set;
        }

        /// <summary>   Gets or sets the height of the external. </summary>
        ///
        /// <value> The height of the external. </value>
        public double ExternalHeight
        {
            get; set;
        }

        /// <summary>   Gets or sets the external volume. </summary>
        ///
        /// <value> The external volume. </value>
        public double ExternalVolume
        {
            get; set;
        }

        /// <summary>   Gets or sets the gross weight. </summary>
        ///
        /// <value> The gross weight. </value>
        public double GrossWeight
        {
            get; set;
        }

        /// <summary>   Gets or sets the load capacity. </summary>
        ///
        /// <value> The load capacity. </value>
        public double LoadCapacity
        {
            get; set;
        }

        /// <summary>   Gets or sets the centre of gravity x coordinate. </summary>
        ///
        /// <value> The centre of gravity x coordinate. </value>
        public double CentreOfGravityX
        {
            get; set;
        }

        /// <summary>   Gets or sets the centre of gravity y coordinate. </summary>
        ///
        /// <value> The centre of gravity y coordinate. </value>
        public double CentreOfGravityY
        {
            get; set;
        }

        /// <summary>   Gets or sets the centre of gravity z coordinate. </summary>
        ///
        /// <value> The centre of gravity z coordinate. </value>
        public double CentreOfGravityZ
        {
            get; set;
        }

        /// <summary>   Gets or sets the caliper. </summary>
        ///
        /// <value> The caliper. </value>
        public double Caliper
        {
            get; set;
        }

        /// <summary>   Gets or sets the length of the cell. </summary>
        ///
        /// <value> The length of the cell. </value>
        public double CellLength
        {
            get; set;
        }

        /// <summary>   Gets or sets the cell breadth. </summary>
        ///
        /// <value> The cell breadth. </value>
        public double CellBreadth
        {
            get; set;
        }

        /// <summary>   Gets or sets the height of the cell. </summary>
        ///
        /// <value> The height of the cell. </value>
        public double CellHeight
        {
            get; set;
        }

        /// <summary>   Gets or sets the blank area. </summary>
        ///
        /// <value> The blank area. </value>
        public double BlankArea
        {
            get; set;
        }

        /// <summary>   Gets or sets the body tolerance. </summary>
        ///
        /// <value> The body tolerance. </value>
        public double BodyTolerance
        {
            get; set;
        }

        /// <summary>   Gets or sets the height tolerance. </summary>
        ///
        /// <value> The height tolerance. </value>
        public double HeightTolerance
        {
            get; set;
        }

        /// <summary>   Gets or sets the density. </summary>
        ///
        /// <value> The density. </value>
        public double Density
        {
            get; set;
        }

        /// <summary>   Gets or sets the shoulder to top. </summary>
        ///
        /// <value> The shoulder to top. </value>
        public double ShoulderToTop
        {
            get; set;
        }

        /// <summary>   Gets or sets the finish x coordinate. </summary>
        ///
        /// <value> The finish x coordinate. </value>
        public double FinishX
        {
            get; set;
        }

        /// <summary>   Gets or sets the finish y coordinate. </summary>
        ///
        /// <value> The finish y coordinate. </value>
        public double FinishY
        {
            get; set;
        }

        /// <summary>   Gets or sets the board strength. </summary>
        ///
        /// <value> The board strength. </value>
        public double BoardStrength
        {
            get; set;
        }

        /// <summary>   Gets or sets target compression. </summary>
        ///
        /// <value> The target compression. </value>
        public double TargetCompression
        {
            get; set;
        }

        /// <summary>   Gets or sets the costings. </summary>
        ///
        /// <value> The costings. </value>
        public List<Costing> Costings
        {
            get; set;
        }
    }
}
