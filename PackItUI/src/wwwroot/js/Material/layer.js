/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global PackIt */

PackIt.Material.Layer = class {
  constructor() {
    this.Layers = 0
    this.OutputRotation = false
    this.ParentRotation = false
    this.Rotation = 0
    this.Percent = 0.0
    this.Length = 0.0
    this.Breadth = 0.0
    this.BondRotation = PackIt.Helpers.Enums.BondRotation.Unknown
    this.BondEastWest = 0.0
    this.BondNorthSouth = 0.0
    this.Bond180Degrees = 0.0
    this.Bond90Degrees = 0.0
    this.LineEastWest = 0.0
    this.LineNorthSouth =0.0
    this.Line180Degrees = 0.0
    this.Line90Degrees = 0.0
    this.Count = 0
    this.Collations = []
  }
}
