// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO.DtoPack
{
    using PackIt.Helpers.Enums;

    /// <summary> A dto limit. </summary>
    public class DtoLimit
    {
        /// <summary> Gets or sets the identifier of the Pack that owns this item. </summary>
        ///
        /// <value> The identifier of the Pack. </value>
        public string PackId { get; set; }

        /// <summary> Gets or sets the stage level. </summary>
        ///
        /// <value> The stage level. </value>
        public StageLevel StageLevel { get; set; }

        /// <summary> Gets or sets the zero-based index of the Limit. </summary>
        ///
        /// <value> The Limit index. </value>
        public long LimitIndex { get; set; }

        /// <summary> Gets or sets a value indicating whether the design. </summary>
        ///
        /// <value> True if design, false if not. </value>
        public bool Design { get; set; }

        /// <summary> Gets or sets the type of the material. </summary>
        ///
        /// <value> The type of the material. </value>
        public MaterialType MaterialType { get; set; }

        /// <summary> Gets or sets the material code. </summary>
        ///
        /// <value> The material code. </value>
        public string MaterialCode { get; set; }

        /// <summary> Gets or sets the material code minimum. </summary>
        ///
        /// <value> The material code minimum. </value>
        public string MaterialCodeMin { get; set; }

        /// <summary> Gets or sets the type of the design. </summary>
        ///
        /// <value> The type of the design. </value>
        public DesignType DesignType { get; set; }

        /// <summary> Gets or sets the design code. </summary>
        ///
        /// <value> The design code. </value>
        public string DesignCode { get; set; }

        /// <summary> Gets or sets the design code minimum. </summary>
        ///
        /// <value> The design code minimum. </value>
        public string DesignCodeMin { get; set; }

        /// <summary> Gets or sets the type of the quality. </summary>
        ///
        /// <value> The type of the quality. </value>
        public QualityType QualityType { get; set; }

        /// <summary> Gets or sets the quality code. </summary>
        ///
        /// <value> The quality code. </value>
        public string QualityCode { get; set; }

        /// <summary> Gets or sets the quality code minimum. </summary>
        ///
        /// <value> The quality code minimum. </value>
        public string QualityCodeMin { get; set; }

        /// <summary> Gets or sets the usage. </summary>
        ///
        /// <value> The usage. </value>
        public UsageType Usage { get; set; }

        /// <summary> Gets or sets a value indicating whether the inverted. </summary>
        ///
        /// <value> True if inverted, false if not. </value>
        public bool Inverted { get; set; }

        /// <summary> Gets or sets the number of layers. </summary>
        ///
        /// <value> The number of layers. </value>
        public long LayerCount { get; set; }

        /// <summary> Gets or sets the layer start. </summary>
        ///
        /// <value> The layer start. </value>
        public long LayerStart { get; set; }

        /// <summary> Gets or sets the layer step. </summary>
        ///
        /// <value> The layer step. </value>
        public long LayerStep { get; set; }

        /// <summary> Gets or sets the quality caliper. </summary>
        ///
        /// <value> The quality caliper. </value>
        public double QualityCaliper { get; set; }

        /// <summary> Gets or sets the quality density. </summary>
        ///
        /// <value> The quality density. </value>
        public double QualityDensity { get; set; }

        /// <summary> Gets or sets the length minimum. </summary>
        ///
        /// <value> The length minimum. </value>
        public double LengthMin { get; set; }

        /// <summary> Gets or sets the length maximum. </summary>
        ///
        /// <value> The length maximum. </value>
        public double LengthMax { get; set; }

        /// <summary> Gets or sets the breadth minimum. </summary>
        ///
        /// <value> The breadth minimum. </value>
        public double BreadthMin { get; set; }

        /// <summary> Gets or sets the breadth maximum. </summary>
        ///
        /// <value> The breadth maximum. </value>
        public double BreadthMax { get; set; }

        /// <summary> Gets or sets the height minimum. </summary>
        ///
        /// <value> The height minimum. </value>
        public double HeightMin { get; set; }

        /// <summary> Gets or sets the height maximum. </summary>
        ///
        /// <value> The height maximum. </value>
        public double HeightMax { get; set; }

        /// <summary> Gets or sets the caliper minimum. </summary>
        ///
        /// <value> The caliper minimum. </value>
        public double CaliperMin { get; set; }

        /// <summary> Gets or sets the caliper maximum. </summary>
        ///
        /// <value> The caliper maximum. </value>
        public double CaliperMax { get; set; }

        /// <summary> Gets or sets the packing gap x coordinate. </summary>
        ///
        /// <value> The packing gap x coordinate. </value>
        public double PackingGapX { get; set; }

        /// <summary> Gets or sets the packing gap y coordinate. </summary>
        ///
        /// <value> The packing gap y coordinate. </value>
        public double PackingGapY { get; set; }

        /// <summary> Gets or sets the packing gap z coordinate. </summary>
        ///
        /// <value> The packing gap z coordinate. </value>
        public double PackingGapZ { get; set; }

        /// <summary> Gets or sets the safety factor minimum. </summary>
        ///
        /// <value> The safety factor minimum. </value>
        public double SafetyFactorMin { get; set; }

        /// <summary> Gets or sets the safety factor maximum. </summary>
        ///
        /// <value> The safety factor maximum. </value>
        public double SafetyFactorMax { get; set; }

        /// <summary> Gets or sets the front placement. </summary>
        ///
        /// <value> The front placement. </value>
        public long FrontPlacement { get; set; }

        /// <summary> Gets or sets the back placement. </summary>
        ///
        /// <value> The back placement. </value>
        public long BackPlacement { get; set; }

        /// <summary> Gets or sets the left placement. </summary>
        ///
        /// <value> The left placement. </value>
        public long LeftPlacement { get; set; }

        /// <summary> Gets or sets the right placement. </summary>
        ///
        /// <value> The right placement. </value>
        public long RightPlacement { get; set; }

        /// <summary> Gets or sets the top placement. </summary>
        ///
        /// <value> The top placement. </value>
        public long TopPlacement { get; set; }

        /// <summary> Gets or sets the bottom placement. </summary>
        ///
        /// <value> The bottom placement. </value>
        public long BottomPlacement { get; set; }

        /// <summary> Gets or sets the length thicknesses. </summary>
        ///
        /// <value> The length thicknesses. </value>
        public long LengthThicknesses { get; set; }

        /// <summary> Gets or sets the length sink change. </summary>
        ///
        /// <value> The length sink change. </value>
        public long LengthSinkChange { get; set; }

        /// <summary> Gets or sets the breadth thicknesses. </summary>
        ///
        /// <value> The breadth thicknesses. </value>
        public long BreadthThicknesses { get; set; }

        /// <summary> Gets or sets the breadth sink change. </summary>
        ///
        /// <value> The breadth sink change. </value>
        public long BreadthSinkChange { get; set; }

        /// <summary> Gets or sets the height thicknesses. </summary>
        ///
        /// <value> The height thicknesses. </value>
        public long HeightThicknesses { get; set; }

        /// <summary> Gets or sets the height sink change. </summary>
        ///
        /// <value> The height sink change. </value>
        public long HeightSinkChange { get; set; }

        /// <summary> Gets or sets the type of the costing. </summary>
        ///
        /// <value> The type of the costing. </value>
        public CostType CostingType { get; set; }
    }
}
