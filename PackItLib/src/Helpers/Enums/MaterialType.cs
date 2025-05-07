// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItLib.Helpers.Enums
{
    using System.ComponentModel.DataAnnotations;

    /// <summary> Values that represent material types. </summary>
    public enum MaterialType
    {
        /// <summary> An enum constant representing the product option. </summary>
        [Display(ShortName = "Product")]
        Product,

        /// <summary> An enum constant representing the carton option. </summary>
        [Display(ShortName = "Carton")]
        Carton,

        /// <summary> An enum constant representing the board option. </summary>
        [Display(Name = "U Board", ShortName = "UBoard")]
        UBoard,

        /// <summary> An enum constant representing the can option. </summary>
        [Display(ShortName = "Can")]
        Can,

        /// <summary> An enum constant representing the bottle option. </summary>
        [Display(ShortName = "Bottle")]
        Bottle,

        /// <summary> An enum constant representing the tub option. </summary>
        [Display(ShortName = "Tub")]
        Tub,

        /// <summary> An enum constant representing the jar option. </summary>
        [Display(ShortName = "Jar")]
        Jar,

        /// <summary> An enum constant representing the sleeve option. </summary>
        [Display(ShortName = "Sleeve")]
        Sleeve,

        /// <summary> An enum constant representing the wrap around option. </summary>
        [Display(ShortName = "Wraparound")]
        WrapAround,

        /// <summary> An enum constant representing the shrinkwrap option. </summary>
        [Display(ShortName = "Shrinkwrap")]
        Shrinkwrap,

        /// <summary> An enum constant representing the crate option. </summary>
        [Display(ShortName = "Crate")]
        Crate,

        /// <summary> An enum constant representing the container option. </summary>
        [Display(ShortName = "Container")]
        Container,

        /// <summary> An enum constant representing the tray option. </summary>
        [Display(ShortName = "Tray")]
        Tray,

        /// <summary> An enum constant representing the pad option. </summary>
        [Display(ShortName = "Pad")]
        Pad,

        /// <summary> An enum constant representing the slipsheet option. </summary>
        [Display(ShortName = "Slipsheet")]
        Slipsheet,

        /// <summary> An enum constant representing the pallet option. </summary>
        [Display(ShortName = "Pallet")]
        Pallet,

        /// <summary> An enum constant representing the divider option. </summary>
        [Display(ShortName = "Divider")]
        Divider,

        /// <summary> An enum constant representing the liner option. </summary>
        [Display(ShortName = "Liner")]
        Liner,

        /// <summary> An enum constant representing the strapping option. </summary>
        [Display(ShortName = "Strapping")]
        Strapping,

        /// <summary> An enum constant representing the edgeboard option. </summary>
        [Display(ShortName = "Edgeboard")]
        Edgeboard,

        /// <summary> An enum constant representing the collar option. </summary>
        [Display(ShortName = "Collar")]
        Collar,

        /// <summary> An enum constant representing the Capability option. </summary>
        [Display(ShortName = "Cap")]
        Cap,

        /// <summary> An enum constant representing the lid option. </summary>
        [Display(ShortName = "Lid")]
        Lid,

        /// <summary> An enum constant representing the seal option. </summary>
        [Display(ShortName = "Seal")]
        Seal
    }
}
