/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global PackIt */

PackIt.Pack.Limit = class {
  constructor() {
    this.Design = false
    this.MaterialType = PackIt.Helpers.Enums.MaterialType.Product
    this.MaterialCode = ''
    this.MaterialCodeMin = ''
    this.DesignType = PackIt.Helpers.Enums.DesignType.Fefco
    this.DesignCode = ''
    this.DesignCodeMin = ''
    this.QualityType = PackIt.Helpers.Enums.QualityType.Min
    this.QualityCode = ''
    this.QualityCodeMin = ''
    this.Usage = PackIt.Helpers.Enums.UsageType.Outer
    this.Inverted = false
    this.LayerCount = 0
    this.LayerStart = 0
    this.LayerStep = 0
    this.QualityCaliper = 0.0
    this.QualityDensity = 0.0
    this.LengthMin = 0.0
    this.LengthMax = 0.0
    this.BreadthMin = 0.0
    this.BreadthMax = 0.0
    this.HeightMin = 0.0
    this.HeightMax = 0.0
    this.CaliperMin = 0.0
    this.CaliperMax = 0.0
    this.PackingGapX = 0.0
    this.PackingGapY = 0.0
    this.PackingGapZ = 0.0
    this.SafetyFactorMin = 0.0
    this.SafetyFactorMax = 0.0
    this.FrontPlacement = 0
    this.BackPlacement = 0
    this.LeftPlacement = 0
    this.RightPlacement = 0
    this.TopPlacement = 0
    this.BottomPlacement = 0
    this.LengthThicknesses = 0
    this.LengthSinkChange = 0
    this.BreadthThicknesses = 0
    this.BreadthSinkChange = 0
    this.HeightThicknesses = 0
    this.HeightSinkChange = 0
    this.CostingType =PackIt.Helpers.Enums.CostType.Unit
  }
}
