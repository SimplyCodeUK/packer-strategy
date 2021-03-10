/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global BABYLON */

var PackIt;
(function (PackItDrawResult) {
  /**
   * The parent dimension after rotation
   *
   */
  var parentDimensions = { }

  /**
   * Populate the parent of a result
   *
   * @param {JSON} model - The model that the result is from
   * @param {JSON} result - The result in JSON
   *
   * @returns {JSON|null} - the parent
   */
  var getParent = function (model, result) {
    for (var stage in model.Stages) {
      if (model.Stages[stage].StageLevel === result.ParentLevel) {
        return model.Stages[stage].Results[result.ParentIndex]
      }
    }

    return null
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
   * Draw a collation
   *
   * @param {BABYLON.Scene} scene - The Babylon scene
   * @param {Number} idx - The layer index
   * @param {JSON} collation - The collation in JSON
   * @param {Number} height - The height the layer is drawn at
   *
   * @returns {Object} - The collation as a mesh list
   */
  var drawCollation = function (scene, idx, collation, height) {
    var ret = []
    var x = 0
    for (var kdx = 0; kdx < collation.CountX; ++kdx) {
      var y = 0
      for (var ldx = 0; ldx < collation.CountY; ++ldx) {
        var mesh = new BABYLON.MeshBuilder.CreateBox('box'.concat(idx.toString()), parentDimensions, scene)
        mesh.position = new BABYLON.Vector3(x, y, height)
        mesh.enableEdgesRendering()
        mesh.edgesWidth = 1.0
        mesh.edgesColor = new BABYLON.Color4(0, 0, 1, 1)
        ret.push(mesh)
      }
    }

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
  var drawResult = function (scene, model, result) {
    var meshes = []
    var parent = getParent(model, result)

    if (parent) {
      var layer = result.Layers[0]
      parentDimensions = rotateResult(parent, layer.Rotation)

      var height = 0
      for (var idx = 0; idx < layer.Layers; ++idx) {
        for (var collation of layer.Collations) {
          meshes.push(...drawCollation(scene, idx, collation, height))
        }
        height += parentDimensions.depth
      }
    }
  return meshes
  }
  PackIt.drawResult = drawResult
}) (PackIt || (PackIt = {}))
