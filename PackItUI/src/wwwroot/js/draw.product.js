/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global BABYLON */

var PackIt;
(function (PackIt) {
  /**
   * Populate the result as a product in the secen
   *
   * @param {BABYLON.Scene} scene - The Babylon scene
   * @param {JSON} result - The result in JSON
   *
   * @returns {BABYLON.Mesh[]} - A list of meshes
   */
  var drawProduct = function (scene, result) {
    var meshes = []

    meshes.push(new BABYLON.MeshBuilder.CreateBox('box', { width: result.ExternalLength, height: result.ExternalHeight, depth: result.ExternalBreadth }, scene))
    return meshes
  }
  PackIt.drawProduct = drawProduct
})(PackIt || (PackIt = {}))
