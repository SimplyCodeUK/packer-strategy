/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global PackIt */

PackIt.Material.Plank = class {
  constructor() {
    this.NoX = 0
    this.NoY = 0
    this.Shape = PackIt.Helpers.Enums.ShapeType.Rectangle
    this.PosX = 0.0
    this.PosY = 0.0
    this.GapX = 0.0
    this.GapY = 0.0
    this.Length = 0.0
    this.Breadth = 0.0
  }
}
