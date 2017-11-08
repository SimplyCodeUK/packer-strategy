// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoPack
{
    using System.Collections.Generic;
    using PackIt.Helpers.Enums;

    /// <summary> A dto result. </summary>
    public class DtoResult
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DtoResult" /> class.
        /// </summary>
        public DtoResult()
        {
            this.Layers = new List<DtoLayer>();
            this.Materials = new List<DtoMaterial>();
            this.Sections = new List<DtoSection>();
        }

        /// <summary> Gets or sets the identifier of the Pack owns this item. </summary>
        ///
        /// <value> The identifier of the Pack. </value>
        public string PackId { get; set; }

        /// <summary> Gets or sets the level. </summary>
        ///
        /// <value> The level. </value>
        public StageLevel StageLevel { get; set; }

        /// <summary> Gets or sets the zero-based index of this Result. </summary>
        ///
        /// <value> The Result index. </value>
        public long ResultIndex { get; set; }

        /// <summary> Gets or sets a value indicating whether the used. </summary>
        ///
        /// <value> True if used, false if not. </value>
        public bool Used { get; set; }

        /// <summary> Gets or sets the generator. </summary>
        ///
        /// <value> The generator. </value>
        public PatternGenerator Generator { get; set; }

        /// <summary> Gets or sets the sub generator. </summary>
        ///
        /// <value> The sub generator. </value>
        public long SubGenerator { get; set; }

        /// <summary> Gets or sets the parent level. </summary>
        ///
        /// <value> The parent level. </value>
        public StageLevel ParentLevel { get; set; }

        /// <summary> Gets or sets the parent index. </summary>
        ///
        /// <value> The parent index. </value>
        public long ParentIndex { get; set; }

        /// <summary> Gets or sets the lift. </summary>
        ///
        /// <value> The lift. </value>
        public long Lift { get; set; }

        /// <summary> Gets or sets the form. </summary>
        ///
        /// <value> The form. </value>
        public FormType Form { get; set; }

        /// <summary> Gets or sets the closure. </summary>
        ///
        /// <value> The closure. </value>
        public long Closure { get; set; }

        /// <summary> Gets or sets the height of the closure. </summary>
        ///
        /// <value> The height of the closure. </value>
        public double ClosureHeight { get; set; }

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

        /// <summary> Gets or sets the density. </summary>
        ///
        /// <value> The density. </value>
        public double Density { get; set; }

        /// <summary> Gets or sets the board strength. </summary>
        ///
        /// <value> The board strength. </value>
        public double BoardStrength { get; set; }

        /// <summary> Gets or sets target compression. </summary>
        ///
        /// <value> The target compression. </value>
        public double TargetCompression { get; set; }

        /// <summary> Gets or sets the safety factor. </summary>
        ///
        /// <value> The safety factor. </value>
        public double SafetyFactor { get; set; }

        /// <summary> Gets or sets the area utilisation. </summary>
        ///
        /// <value> The area utilisation. </value>
        public double AreaUtilisation { get; set; }

        /// <summary> Gets or sets the volume utilisation. </summary>
        ///
        /// <value> The volume utilisation. </value>
        public double VolumeUtilisation { get; set; }

        /// <summary> Gets or sets the collection of layers. </summary>
        ///
        /// <value> Collection of layers. </value>
        public IList<DtoLayer> Layers { get; set; }

        /// <summary> Gets or sets the collection of materials. </summary>
        ///
        /// <value> Collection of materials. </value>
        public IList<DtoMaterial> Materials { get; set; }

        /// <summary> Gets or sets the collection of sections. </summary>
        ///
        /// <value> Collection of sections. </value>
        public IList<DtoSection> Sections { get; set; }
    }
}
