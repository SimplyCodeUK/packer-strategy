/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global PackIt */

PackIt.Pack.Result = class {
  constructor() {
    this.Used = false
    this.Generator = PackIt.Helpers.Enums.PatternGenerator.Split
    this.SubGenerator = 0
    this.ParentLevel = PackIt.Helpers.Enums.StageLevel.New
    this.ParentIndex = 0
    this.Lift = 0
    this.Form = PackIt.Helpers.Enums.FormType.Box
    this.Closure = 0
    this.ClosureHeight = 0.0
    this.ClosureWeight = 0.0
    this.StackStep = 0.0
    this.Flap = 0.0
    this.InternalLength = 0.0
    this.InternalBreadth = 0.0
    this.InternalHeight = 0.0
    this.InternalVolume = 0.0
    this.NettWeight = 0.0
    this.ExternalLength = 0.0
    this.ExternalBreadth = 0.0
    this.ExternalHeight = 0.0
    this.ExternalVolume = 0.0
    this.GrossWeight = 0.0
    this.LoadCapacity = 0.0
    this.CentreOfGravityX = 0.0
    this.CentreOfGravityY = 0.0
    this.CentreOfGravityZ = 0.0
    this.Caliper = 0.0
    this.CellLength = 0.0
    this.CellBreadth = 0.0
    this.CellHeight = 0.0
    this.BlankArea = 0.0
    this.Density = 0.0
    this.BoardStrength = 0.0
    this.TargetCompression = 0.0
    this.SafetyFactor = 0.0
    this.AreaUtilisation = 0.0
    this.VolumeUtilisation = 0.0
    this.Layers = []
    this.Materials = []
    this.Sections = []
  }
}
