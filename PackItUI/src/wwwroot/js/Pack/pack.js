/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global PackIt */

PackIt.Pack.Pack = class {
  constructor() {
    this.PackId = ''
    this.Name = ''
    this.PlanCode = ''
    this.PlanName = ''
    this.MaterialType = PackIt.Helpers.Enums.MaterialType.Bottle
    this.MaterialCode = ''
    this.MaterialName = ''
    this.Costings = []
    this.Stages = []
  }
}
