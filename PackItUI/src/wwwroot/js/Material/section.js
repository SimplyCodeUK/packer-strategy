/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global PackIt */

PackIt.Material.Section = class {
  constructor() {
    this.Shape = PackIt.Helpers.Enums.ShapeType.Rectangle
    this.SectionType = PackIt.Helpers.Enums.SectionTypes.Minor
    this.Dimension0 = 0.0
    this.Dimension1 = 0.0
    this.Dimension2 = 0.0
    this.Dimension3 = 0.0
    this.Dimension4 = 0.0
    this.Dimension5 = 0.0
    this.Dimension6 = 0.0
    this.Dimension7 = 0.0
    this.Height = 0.0
  }
}
