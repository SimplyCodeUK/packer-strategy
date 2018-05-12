/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global PackIt */

PackIt.Material.Collation = class {
  constructor() {
    this.PosX = 0.0
    this.PosY = 0.0
    this.GapX = 0.0
    this.GapY = 0.0
    this.CountX = 0
    this.CountY = 0
    this.Even = 0
    this.Nested = 0
    this.AlongBreadth = false
    this.Rotation = 0
  }
}
