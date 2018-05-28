/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global BABYLON */
/* global PackIt */

/**
 * Populate the result as a product in the secen
 *
 * @param {BABYLON.Scene} scene - The Babylon scene
 * @param {JSON} result - The result in JSON
 *
 * @returns {undefined}
 */

PackIt.drawProduct = function (scene, result) {
  return new BABYLON.MeshBuilder.CreateBox('box', { width: result.ExternalLength, height: result.ExternalHeight, depth: result.ExternalBreadth }, scene)
}
