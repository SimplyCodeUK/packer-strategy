/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global BABYLON */
/* global PackIt */

/**
 * Populate the parent of a result
 *
 * @param {JSON} model - The model that the result is from
 * @param {JSON} result - The result in JSON
 *
 * @returns {undefined}
 */
var getParent = function (model, result) {
  var stage;
  for (stage in model.Stages) {
    if (model.Stages[stage].StageLevel === result.ParentLevel) return model.Stages[stage].Results[result.ParentIndex]
  }

  return undefined
}

/**
 * Rotate a result
 *
 * @param {JSON} result - The result in JSON
 * @param {Number} rotation - Rotation mask applied to the result
 *
 * @returns {Object} - Dimensions once result is rotated
 */
var rotateResult = function (result, rotation) {
  var ret = { width: result.ExternalLength, height: result.ExternalHeight, depth: result.ExternalBreadth }
  if (rotation & PackIt.Helpers.Masks.ResultRotation.ParentBreadthToHeight) {
    ret = { width: result.ExternalLength, height: result.ExternalBreadth, depth: result.ExternalHeight }
  } // if

  if (rotation & PackIt.Helpers.Masks.ResultRotation.ParentLengthToHeight) {
    ret = { width: result.ExternalBreadth, height: result.ExternalHeight, depth: result.ExternalLength }
  } // if

  return ret
}

/**
 * Populate the result in the scene
 *
 * @param {BABYLON.Scene} scene - The Babylon scene
 * @param {JSON} model - The model that the result is from
 * @param {JSON} result - The result in JSON
 *
 * @returns {BABYLON.Mesh[]} - A list of meshes
 */
PackIt.drawResult = function (scene, model, result) {
  var meshes = []
  var parent = getParent(model, result)

  if (parent) {
    var layer = result.Layers[0]
    var parentDimensions = rotateResult(parent, layer.Rotation);

    for (var idx = 0; idx < layer.Layers; ++idx) {
      var mesh = new BABYLON.MeshBuilder.CreateBox('box'.concat(idx.toString()), parentDimensions, scene)
      mesh.position = new BABYLON.Vector3(0, 0, parentDimensions.depth * idx)
      mesh.enableEdgesRendering()
      mesh.edgesWidth = 1
      mesh.edgesColor = new BABYLON.Color4(0, 0, 0, 1)
      meshes.push(mesh)
    }
  }
  return meshes
}
