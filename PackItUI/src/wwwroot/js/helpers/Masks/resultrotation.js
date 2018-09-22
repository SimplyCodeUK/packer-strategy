/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global PackIt */

/** Values that represent pattern generators. */
PackIt.Helpers.Masks.ResultRotation = Object.freeze(
  {
    /** A mask constant representing outout rotation of height to height. */
    OutputHeightToHeight: 0x0001,

    /** A mask constant representing outout rotation of breadth to height. */
    OutputBreadthToHeight: 0x0002,

    /** A mask constant representing outout rotation of length to height. */
    OutputLengthToHeight: 0x0004,

    /** A mask constant representing parent rotation of height to height. */
    ParentHeightToHeight: 0x0008,

    /** A mask constant representing parent rotation of breadth to height. */
    ParentBreadthToHeight: 0x0010,

    /** A mask constant representing parent rotation of length to height. */
    ParentLengthToHeight: 0x0020,

    /** A mask constant representing product rotation of height to height. */
    ProductHeightToHeight: 0x0040,

    /** A mask constant representing product rotation of breadth to height. */
    ProductBreadthToHeight: 0x0080,

    /** A mask constant representing product rotation of length to height. */
    ProductLengthToHeight: 0x0100,

    /** A mask constant representing along length. */
    AlongLength: 0x0200,

    /** A mask constant representing along breadth. */
    AlongBreadth: 0x0400,
  }
)
