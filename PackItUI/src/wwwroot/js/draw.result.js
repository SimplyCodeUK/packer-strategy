/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global BABYLON */
/* global PackIt */

/**
 * Populate the result in the scene
 *
 * @param {BABYLON.Scene} scene - The Babylon scene
 * @param {JSON} pack - The pack that the result is from
 * @param {JSON} result - The result in JSON
 *
 * @returns {undefined}
 */
PackIt.drawResult = function (scene, pack, result) {
  return new BABYLON.MeshBuilder.CreateBox('box', { width: result.ExternalLength, height: result.ExternalHeight, depth: result.ExternalBreadth }, scene)
}
