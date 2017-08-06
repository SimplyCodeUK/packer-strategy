//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;

namespace packer_strategy.Models.Plan
{
    /// <summary>   A stage. </summary>
    public class Stage
    {
        /// <summary>   Values that represent levels. </summary>
        public enum Levels
        {
            /// <summary>   An enum constant representing the new option. </summary>
            New,
            /// <summary>   An enum constant representing the variable product option. </summary>
            VariableProduct,
            /// <summary>   An enum constant representing the product pack option. </summary>
            ProductPack,
            /// <summary>   An enum constant representing the multi pack option. </summary>
            MultiPack,
            /// <summary>   An enum constant representing the shelf unit option. </summary>
            ShelfUnit,
            /// <summary>   An enum constant representing the transit pack option. </summary>
            TransitPack,
            /// <summary>   An enum constant representing the transport load option. </summary>
            TransportLoad,
            /// <summary>   An enum constant representing the multi stack option. </summary>
            MultiStack,
            /// <summary>   An enum constant representing the container option. </summary>
            Container
        }

        /// <summary>   Values that represent forms. </summary>
        public enum Forms
        {
            /// <summary>   An enum constant representing the Minimum option. </summary>
            Min,

            /// <summary>   An enum constant representing the none option. </summary>
            None = Min,
            /// <summary>   An enum constant representing the box option. </summary>
            Box,
            /// <summary>   An enum constant representing the cylinder option. </summary>
            Cylinder,
            /// <summary>   An enum constant representing the tub option. </summary>
            Tub,
            /// <summary>   An enum constant representing the cone option. </summary>
            Cone,
            /// <summary>   An enum constant representing the bottle option. </summary>
            Bottle,

            /// <summary>   An enum constant representing the Maximum option. </summary>
            Max
        };

        /// <summary>   Values that represent shapes. </summary>
        public enum Shapes
        {
            /// <summary>   An enum constant representing the Minimum option. </summary>
            Min,

            /// <summary>   An enum constant representing the rectangle option. </summary>
            Rectangle = Min,
            /// <summary>   An enum constant representing the circle option. </summary>
            Circle,
            /// <summary>   An enum constant representing the oval option. </summary>
            Oval,
            /// <summary>   An enum constant representing the round rectangle option. </summary>
            RoundRectangle,

            /// <summary>   An enum constant representing the Maximum option. </summary>
            Max
        };

        /// <summary>   Default constructor. </summary>
        public Stage()
        {
            this.Limits = new List<Limit>();
        }

        /// <summary>   Identifier for the plan. </summary>
        private string _planId;

        /// <summary>   Gets or sets the identifier of the plan. </summary>
        ///
        /// <value> The identifier of the plan. </value>
        public string PlanId {
            get {
                return _planId;
            }
            set {
                _planId = value;
                foreach (Limit limit in Limits)
                {
                    limit.PlanId = value;
                }
            }
        }

        /// <summary>   The level. </summary>
        private Levels level;

        /// <summary>   Gets or sets the level. </summary>
        ///
        /// <value> The level. </value>
        public Levels Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                foreach (Limit limit in Limits)
                {
                    limit.Level = value;
                }
            }
        }

        /// <summary>   Gets or sets the collation. </summary>
        ///
        /// <value> The collation. </value>
        public long Collation { get; set; }

        /// <summary>   Gets or sets the draw offset. </summary>
        ///
        /// <value> The draw offset. </value>
        public long DrawOffset { get; set; }

        /// <summary>   Gets or sets the shape. </summary>
        ///
        /// <value> The shape. </value>
        public Shapes Shape { get; set; }

        /// <summary>   Gets or sets the form. </summary>
        ///
        /// <value> The form. </value>
        public Forms Form { get; set; }

        /// <summary>   Gets or sets the shape parameter 0. </summary>
        ///
        /// <value> The shape parameter 0. </value>
        public double ShapeParameter0 { get; set; }

        /// <summary>   Gets or sets the shape parameter 1. </summary>
        ///
        /// <value> The shape parameter 1. </value>
        public double ShapeParameter1 { get; set; }

        /// <summary>   Gets or sets the shape parameter 2. </summary>
        ///
        /// <value> The shape parameter 2. </value>
        public double ShapeParameter2 { get; set; }

        /// <summary>   Gets or sets the shape parameter 3. </summary>
        ///
        /// <value> The shape parameter 3. </value>
        public double ShapeParameter3 { get; set; }

        /// <summary>   Gets or sets the shape parameter 4. </summary>
        ///
        /// <value> The shape parameter 4. </value>
        public double ShapeParameter4 { get; set; }

        /// <summary>   Gets or sets the shape parameter 5. </summary>
        ///
        /// <value> The shape parameter 5. </value>
        public double ShapeParameter5 { get; set; }

        /// <summary>   Gets or sets the shape parameter 6. </summary>
        ///
        /// <value> The shape parameter 6. </value>
        public double ShapeParameter6 { get; set; }

        /// <summary>   Gets or sets the shape parameter 7. </summary>
        ///
        /// <value> The shape parameter 7. </value>
        public double ShapeParameter7 { get; set; }

        /// <summary>   Gets or sets the shape parameter 8. </summary>
        ///
        /// <value> The shape parameter 8. </value>
        public double ShapeParameter8 { get; set; }

        /// <summary>   Gets or sets the shape parameter 9. </summary>
        ///
        /// <value> The shape parameter 9. </value>
        public double ShapeParameter9 { get; set; }

        /// <summary>   Gets or sets the form parameter 0. </summary>
        ///
        /// <value> The form parameter 0. </value>
        public double FormParameter0 { get; set; }

        /// <summary>   Gets or sets the form parameter 1. </summary>
        ///
        /// <value> The form parameter 1. </value>
        public double FormParameter1 { get; set; }

        /// <summary>   Gets or sets the form parameter 2. </summary>
        ///
        /// <value> The form parameter 2. </value>
        public double FormParameter2 { get; set; }

        /// <summary>   Gets or sets the form parameter 3. </summary>
        ///
        /// <value> The form parameter 3. </value>
        public double FormParameter3 { get; set; }

        /// <summary>   Gets or sets the form parameter 4. </summary>
        ///
        /// <value> The form parameter 4. </value>
        public double FormParameter4 { get; set; }

        /// <summary>   Gets or sets the form parameter 5. </summary>
        ///
        /// <value> The form parameter 5. </value>
        public double FormParameter5 { get; set; }

        /// <summary>   Gets or sets the form parameter 6. </summary>
        ///
        /// <value> The form parameter 6. </value>
        public double FormParameter6 { get; set; }

        /// <summary>   Gets or sets the form parameter 7. </summary>
        ///
        /// <value> The form parameter 7. </value>
        public double FormParameter7 { get; set; }

        /// <summary>   Gets or sets the form parameter 8. </summary>
        ///
        /// <value> The form parameter 8. </value>
        public double FormParameter8 { get; set; }

        /// <summary>   Gets or sets the form parameter 9. </summary>
        ///
        /// <value> The form parameter 9. </value>
        public double FormParameter9 { get; set; }

        /// <summary>   Gets or sets the density. </summary>
        ///
        /// <value> The density. </value>
        public double Density { get; set; }

        /// <summary>   Gets or sets the generator. </summary>
        ///
        /// <value> The generator. </value>
        public long Generator { get; set; }

        /// <summary>   Gets or sets the rotation. </summary>
        ///
        /// <value> The rotation. </value>
        public long Rotation { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the materials should be computed.
        /// </summary>
        ///
        /// <value> True if compute materials, false if not. </value>
        public bool ComputeMaterials { get; set; }

        /// <summary>   Gets or sets the sort. </summary>
        ///
        /// <value> The sort. </value>
        public long Sort { get; set; }

        /// <summary>   Gets or sets the breathing space x coordinate. </summary>
        ///
        /// <value> The breathing space x coordinate. </value>
        public double BreathingSpaceX { get; set; }

        /// <summary>   Gets or sets the breathing space y coordinate. </summary>
        ///
        /// <value> The breathing space y coordinate. </value>
        public double BreathingSpaceY { get; set; }

        /// <summary>   Gets or sets the breathing space z coordinate. </summary>
        ///
        /// <value> The breathing space z coordinate. </value>
        public double BreathingSpaceZ { get; set; }

        /// <summary>   Gets or sets the bulging x coordinate. </summary>
        ///
        /// <value> The bulging x coordinate. </value>
        public double BulgingX { get; set; }

        /// <summary>   Gets or sets the bulging y coordinate. </summary>
        ///
        /// <value> The bulging y coordinate. </value>
        public double BulgingY { get; set; }

        /// <summary>   Gets or sets the bulging z coordinate. </summary>
        ///
        /// <value> The bulging z coordinate. </value>
        public double BulgingZ { get; set; }

        /// <summary>   Gets or sets the percent minimum. </summary>
        ///
        /// <value> The percent minimum. </value>
        public double PercentMin { get; set; }

        /// <summary>   Gets or sets the percent maximum. </summary>
        ///
        /// <value> The percent maximum. </value>
        public double PercentMax { get; set; }

        /// <summary>   Gets or sets the load minimum. </summary>
        ///
        /// <value> The load minimum. </value>
        public long LoadMin { get; set; }

        /// <summary>   Gets or sets the load maximum. </summary>
        ///
        /// <value> The load maximum. </value>
        public long LoadMax { get; set; }

        /// <summary>   Gets or sets the load step. </summary>
        ///
        /// <value> The load step. </value>
        public long LoadStep { get; set; }

        /// <summary>   Gets or sets the tiers minimum. </summary>
        ///
        /// <value> The tiers minimum. </value>
        public long TiersMin { get; set; }

        /// <summary>   Gets or sets the tiers maximum. </summary>
        ///
        /// <value> The tiers maximum. </value>
        public long TiersMax { get; set; }

        /// <summary>   Gets or sets the tiers step. </summary>
        ///
        /// <value> The tiers step. </value>
        public long TiersStep { get; set; }

        /// <summary>   Gets or sets the per tier minimum. </summary>
        ///
        /// <value> The per tier minimum. </value>
        public long PerTierMin { get; set; }

        /// <summary>   Gets or sets the per tier maximum. </summary>
        ///
        /// <value> The per tier maximum. </value>
        public long PerTierMax { get; set; }

        /// <summary>   Gets or sets the per tier step. </summary>
        ///
        /// <value> The per tier step. </value>
        public long PerTierStep { get; set; }

        /// <summary>   Gets or sets the along side minimum. </summary>
        ///
        /// <value> The along side minimum. </value>
        public long AlongSideMin { get; set; }

        /// <summary>   Gets or sets the along side maximum. </summary>
        ///
        /// <value> The along side maximum. </value>
        public long AlongSideMax { get; set; }

        /// <summary>   Gets or sets the along side step. </summary>
        ///
        /// <value> The along side step. </value>
        public long AlongSideStep { get; set; }

        /// <summary>   Gets or sets the product minimum. </summary>
        ///
        /// <value> The product minimum. </value>
        public long ProductMin { get; set; }

        /// <summary>   Gets or sets the product maximum. </summary>
        ///
        /// <value> The product maximum. </value>
        public long ProductMax { get; set; }

        /// <summary>   Gets or sets the product step. </summary>
        ///
        /// <value> The product step. </value>
        public long ProductStep { get; set; }

        /// <summary>   Gets or sets the length of the external. </summary>
        ///
        /// <value> The length of the external. </value>
        public double ExternalLength { get; set; }

        /// <summary>   Gets or sets the external length minimum. </summary>
        ///
        /// <value> The external length minimum. </value>
        public double ExternalLengthMin { get; set; }

        /// <summary>   Gets or sets the external length maximum. </summary>
        ///
        /// <value> The external length maximum. </value>
        public double ExternalLengthMax { get; set; }

        /// <summary>   Gets or sets the external length step. </summary>
        ///
        /// <value> The external length step. </value>
        public double ExternalLengthStep { get; set; }

        /// <summary>   Gets or sets the external breadth. </summary>
        ///
        /// <value> The external breadth. </value>
        public double ExternalBreadth { get; set; }

        /// <summary>   Gets or sets the external breadth minimum. </summary>
        ///
        /// <value> The external breadth minimum. </value>
        public double ExternalBreadthMin { get; set; }

        /// <summary>   Gets or sets the external breadth maximum. </summary>
        ///
        /// <value> The external breadth maximum. </value>
        public double ExternalBreadthMax { get; set; }

        /// <summary>   Gets or sets the external breadth step. </summary>
        ///
        /// <value> The external breadth step. </value>
        public double ExternalBreadthStep { get; set; }

        /// <summary>   Gets or sets the height of the external. </summary>
        ///
        /// <value> The height of the external. </value>
        public double ExternalHeight { get; set; }

        /// <summary>   Gets or sets the external height minimum. </summary>
        ///
        /// <value> The external height minimum. </value>
        public double ExternalHeightMin { get; set; }

        /// <summary>   Gets or sets the external height maximum. </summary>
        ///
        /// <value> The external height maximum. </value>
        public double ExternalHeightMax { get; set; }

        /// <summary>   Gets or sets the external height step. </summary>
        ///
        /// <value> The external height step. </value>
        public double ExternalHeightStep { get; set; }

        /// <summary>   Gets or sets the external volume. </summary>
        ///
        /// <value> The external volume. </value>
        public double ExternalVolume { get; set; }

        /// <summary>   Gets or sets the external volume minimum. </summary>
        ///
        /// <value> The external volume minimum. </value>
        public double ExternalVolumeMin { get; set; }

        /// <summary>   Gets or sets the external volume maximum. </summary>
        ///
        /// <value> The external volume maximum. </value>
        public double ExternalVolumeMax { get; set; }

        /// <summary>   Gets or sets the external volume step. </summary>
        ///
        /// <value> The external volume step. </value>
        public double ExternalVolumeStep { get; set; }

        /// <summary>   Gets or sets the external length to breadth ratio. </summary>
        ///
        /// <value> The external length to breadth ratio. </value>
        public double ExternalLengthToBreadthRatio { get; set; }

        /// <summary>   Gets or sets the external length to breadth ratio minimum. </summary>
        ///
        /// <value> The external length to breadth ratio minimum. </value>
        public double ExternalLengthToBreadthRatioMin { get; set; }

        /// <summary>   Gets or sets the external length to breadth ratio maximum. </summary>
        ///
        /// <value> The external length to breadth ratio maximum. </value>
        public double ExternalLengthToBreadthRatioMax { get; set; }

        /// <summary>   Gets or sets the external length to breadth ratio step. </summary>
        ///
        /// <value> The external length to breadth ratio step. </value>
        public double ExternalLengthToBreadthRatioStep { get; set; }

        /// <summary>   Gets or sets the external length to height ratio. </summary>
        ///
        /// <value> The external length to height ratio. </value>
        public double ExternalLengthToHeightRatio { get; set; }

        /// <summary>   Gets or sets the external length to height ratio minimum. </summary>
        ///
        /// <value> The external length to height ratio minimum. </value>
        public double ExternalLengthToHeightRatioMin { get; set; }

        /// <summary>   Gets or sets the external length to height ratio maximum. </summary>
        ///
        /// <value> The external length to height ratio maximum. </value>
        public double ExternalLengthToHeightRatioMax { get; set; }

        /// <summary>   Gets or sets the external length to height ratio step. </summary>
        ///
        /// <value> The external length to height ratio step. </value>
        public double ExternalLengthToHeightRatioStep { get; set; }

        /// <summary>   Gets or sets the external angle. </summary>
        ///
        /// <value> The external angle. </value>
        public double ExternalAngle { get; set; }

        /// <summary>   Gets or sets the external angle minimum. </summary>
        ///
        /// <value> The external angle minimum. </value>
        public double ExternalAngleMin { get; set; }

        /// <summary>   Gets or sets the external angle maximum. </summary>
        ///
        /// <value> The external angle maximum. </value>
        public double ExternalAngleMax { get; set; }

        /// <summary>   Gets or sets the external angle step. </summary>
        ///
        /// <value> The external angle step. </value>
        public double ExternalAngleStep { get; set; }

        /// <summary>   Gets or sets the gross weight. </summary>
        ///
        /// <value> The gross weight. </value>
        public double GrossWeight { get; set; }

        /// <summary>   Gets or sets the gross weight minimum. </summary>
        ///
        /// <value> The gross weight minimum. </value>
        public double GrossWeightMin { get; set; }

        /// <summary>   Gets or sets the gross weight maximum. </summary>
        ///
        /// <value> The gross weight maximum. </value>
        public double GrossWeightMax { get; set; }

        /// <summary>   Gets or sets the gross weight step. </summary>
        ///
        /// <value> The gross weight step. </value>
        public double GrossWeightStep { get; set; }

        /// <summary>   Gets or sets the length of the internal. </summary>
        ///
        /// <value> The length of the internal. </value>
        public double InternalLength { get; set; }

        /// <summary>   Gets or sets the internal length minimum. </summary>
        ///
        /// <value> The internal length minimum. </value>
        public double InternalLengthMin { get; set; }

        /// <summary>   Gets or sets the internal length maximum. </summary>
        ///
        /// <value> The internal length maximum. </value>
        public double InternalLengthMax { get; set; }

        /// <summary>   Gets or sets the internal length step. </summary>
        ///
        /// <value> The internal length step. </value>
        public double InternalLengthStep { get; set; }

        /// <summary>   Gets or sets the internal breadth. </summary>
        ///
        /// <value> The internal breadth. </value>
        public double InternalBreadth { get; set; }

        /// <summary>   Gets or sets the internal breadth minimum. </summary>
        ///
        /// <value> The internal breadth minimum. </value>
        public double InternalBreadthMin { get; set; }

        /// <summary>   Gets or sets the internal breadth maximum. </summary>
        ///
        /// <value> The internal breadth maximum. </value>
        public double InternalBreadthMax { get; set; }

        /// <summary>   Gets or sets the internal breadth step. </summary>
        ///
        /// <value> The internal breadth step. </value>
        public double InternalBreadthStep { get; set; }

        /// <summary>   Gets or sets the height of the internal. </summary>
        ///
        /// <value> The height of the internal. </value>
        public double InternalHeight { get; set; }

        /// <summary>   Gets or sets the internal height minimum. </summary>
        ///
        /// <value> The internal height minimum. </value>
        public double InternalHeightMin { get; set; }

        /// <summary>   Gets or sets the internal height maximum. </summary>
        ///
        /// <value> The internal height maximum. </value>
        public double InternalHeightMax { get; set; }

        /// <summary>   Gets or sets the internal height step. </summary>
        ///
        /// <value> The internal height step. </value>
        public double InternalHeightStep { get; set; }

        /// <summary>   Gets or sets the internal volume. </summary>
        ///
        /// <value> The internal volume. </value>
        public double InternalVolume { get; set; }

        /// <summary>   Gets or sets the internal volume minimum. </summary>
        ///
        /// <value> The internal volume minimum. </value>
        public double InternalVolumeMin { get; set; }

        /// <summary>   Gets or sets the internal volume maximum. </summary>
        ///
        /// <value> The internal volume maximum. </value>
        public double InternalVolumeMax { get; set; }

        /// <summary>   Gets or sets the internal volume step. </summary>
        ///
        /// <value> The internal volume step. </value>
        public double InternalVolumeStep { get; set; }

        /// <summary>   Gets or sets the internal length to breadth ratio. </summary>
        ///
        /// <value> The internal length to breadth ratio. </value>
        public double InternalLengthToBreadthRatio { get; set; }

        /// <summary>   Gets or sets the internal length to breadth ratio minimum. </summary>
        ///
        /// <value> The internal length to breadth ratio minimum. </value>
        public double InternalLengthToBreadthRatioMin { get; set; }

        /// <summary>   Gets or sets the internal length to breadth ratio maximum. </summary>
        ///
        /// <value> The internal length to breadth ratio maximum. </value>
        public double InternalLengthToBreadthRatioMax { get; set; }

        /// <summary>   Gets or sets the internal length to breadth ratio step. </summary>
        ///
        /// <value> The internal length to breadth ratio step. </value>
        public double InternalLengthToBreadthRatioStep { get; set; }

        /// <summary>   Gets or sets the internal length to height ratio. </summary>
        ///
        /// <value> The internal length to height ratio. </value>
        public double InternalLengthToHeightRatio { get; set; }

        /// <summary>   Gets or sets the internal length to height ratio minimum. </summary>
        ///
        /// <value> The internal length to height ratio minimum. </value>
        public double InternalLengthToHeightRatioMin { get; set; }

        /// <summary>   Gets or sets the internal length to height ratio maximum. </summary>
        ///
        /// <value> The internal length to height ratio maximum. </value>
        public double InternalLengthToHeightRatioMax { get; set; }

        /// <summary>   Gets or sets the internal length to height ratio step. </summary>
        ///
        /// <value> The internal length to height ratio step. </value>
        public double InternalLengthToHeightRatioStep { get; set; }

        /// <summary>   Gets or sets the internal angle. </summary>
        ///
        /// <value> The internal angle. </value>
        public double InternalAngle { get; set; }

        /// <summary>   Gets or sets the internal angle minimum. </summary>
        ///
        /// <value> The internal angle minimum. </value>
        public double InternalAngleMin { get; set; }

        /// <summary>   Gets or sets the internal angle maximum. </summary>
        ///
        /// <value> The internal angle maximum. </value>
        public double InternalAngleMax { get; set; }

        /// <summary>   Gets or sets the internal angle step. </summary>
        ///
        /// <value> The internal angle step. </value>
        public double InternalAngleStep { get; set; }

        /// <summary>   Gets or sets the nett weight. </summary>
        ///
        /// <value> The nett weight. </value>
        public double NettWeight { get; set; }

        /// <summary>   Gets or sets the nett weight minimum. </summary>
        ///
        /// <value> The nett weight minimum. </value>
        public double NettWeightMin { get; set; }

        /// <summary>   Gets or sets the nett weight maximum. </summary>
        ///
        /// <value> The nett weight maximum. </value>
        public double NettWeightMax { get; set; }

        /// <summary>   Gets or sets the nett weight step. </summary>
        ///
        /// <value> The nett weight step. </value>
        public double NettWeightStep { get; set; }

        /// <summary>   Gets or sets the area utilisation minimum. </summary>
        ///
        /// <value> The area utilisation minimum. </value>
        public double AreaUtilisationMin { get; set; }

        /// <summary>   Gets or sets the area utilisation maximum. </summary>
        ///
        /// <value> The area utilisation maximum. </value>
        public double AreaUtilisationMax { get; set; }

        /// <summary>   Gets or sets the volume utilisation minimum. </summary>
        ///
        /// <value> The volume utilisation minimum. </value>
        public double VolumeUtilisationMin { get; set; }

        /// <summary>   Gets or sets the volume utilisation maximum. </summary>
        ///
        /// <value> The volume utilisation maximum. </value>
        public double VolumeUtilisationMax { get; set; }

        /// <summary>   Gets or sets the security. </summary>
        ///
        /// <value> The security. </value>
        public long Security { get; set; }

        /// <summary>   Gets or sets the security weight. </summary>
        ///
        /// <value> The security weight. </value>
        public double SecurityWeight { get; set; }

        /// <summary>   Gets or sets the outer. </summary>
        ///
        /// <value> The outer. </value>
        public long Outer { get; set; }

        /// <summary>   Gets or sets the draw detail. </summary>
        ///
        /// <value> The draw detail. </value>
        public long DrawDetail { get; set; }

        /// <summary>   Gets or sets the length of the minimum block. </summary>
        ///
        /// <value> The length of the minimum block. </value>
        public long MinBlockLength { get; set; }

        /// <summary>   Gets or sets the minimum block breadth. </summary>
        ///
        /// <value> The minimum block breadth. </value>
        public long MinBlockBreadth { get; set; }

        /// <summary>   Gets or sets a value indicating whether the hands should be drawn. </summary>
        ///
        /// <value> True if draw hands, false if not. </value>
        public bool DrawHands { get; set; }

        /// <summary>   Gets or sets the limits. </summary>
        ///
        /// <value> The limits. </value>
        public List<Limit> Limits { get; set; }
    }
}
