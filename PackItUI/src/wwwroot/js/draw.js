/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global BABYLON */
/* global PackIt */

/**
 * Populate the scene
 *
 * @param {BABYLON.Scene} scene - The Babylon scene
 * @param {JSON} model - The model that the result is from
 * @param {JSON} result - The result in JSON
 */
var populateScene = function (scene, model, result) {
  if ((result.Generator !== PackIt.Helpers.Enums.PatternGenerator.Variable) &&
    (result.Level !== PackIt.Helpers.Enums.StageLevel.New)) {
    PackIt.drawResult(scene, model, result)
  }
  else {
    PackIt.drawProduct(scene, result)
  }
}

/**
 * Draw a pack
 *
 * @param {HTMLElement} canvas - The DOM container to draw in
 * @param {string} pack - The pack as a json string
 */
var drawPack = function (canvas, pack) {
  var model = JSON.parse(pack)
  var result = model.Stages[model.Stages.length-1].Results[0]
  var engine = new BABYLON.Engine(canvas, true)

  // Create the scene
  var scene = new BABYLON.Scene(engine)
  var camera = new BABYLON.UniversalCamera('camera', new BABYLON.Vector3(-result.ExternalLength * 1.5, result.ExternalHeight * 1.5, -result.ExternalBreadth), scene)
  camera.setTarget(new BABYLON.Vector3.Zero())
  camera.attachControl(canvas, false)
  var light = new BABYLON.HemisphericLight('light1', new BABYLON.Vector3(0, 1, 0), scene)
  light.diffuse = new BABYLON.Color3(1, 1, 1)

  // Populate the scene
  populateScene(scene, model, result)

  engine.runRenderLoop(function () {
    scene.render()
  })

  window.addEventListener('resize', function () {
    engine.resize()
  })
}
