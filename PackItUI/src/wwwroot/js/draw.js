/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
"use strict";

/* global BABYLON */
/* global PackIt */

/**
 * Populate the scene
 *
 * @param {BABYLON.Scene} scene - The Babylon scene
 * @param {JSON} model - The model that the result is from
 */
var populateScene = function (scene, model) {
  var idx = 0;
  for (const shape of model.Drawing.Shapes) {
    var parentDimensions = { width: shape.Length, depth: shape.Breadth, height: shape.Height };
    var box = new BABYLON.MeshBuilder.CreateBox("box".concat(idx.toString()), parentDimensions, scene);
    box.position = new BABYLON.Vector3(shape.X, shape.Y, shape.Z);
    box.enableEdgesRendering();
    box.edgesWidth = (shape.Length + shape.Breadth + shape.Height) / 20.0;
    box.edgesColor = new BABYLON.Color4(0, 0, 1, 1);
    ++idx;
  }
};

/**
 * Draw a pack
 *
 * @param {HTMLElement} canvas - The DOM container to draw in
 * @param {string} modelJson - The pack as a json string
 */
var drawPack = function (canvas, modelJson) {
  var model = JSON.parse(modelJson);
  var result = model.Pack.Stages[model.Pack.Stages.length - 1].Results[0];
  var engine = new BABYLON.Engine(canvas, true);

  // Create the scene
  var scene = new BABYLON.Scene(engine);
  var camera = new BABYLON.UniversalCamera("camera", new BABYLON.Vector3(-result.ExternalLength * 1.5, result.ExternalHeight * 1.5, -result.ExternalBreadth), scene);
  camera.setTarget(new BABYLON.Vector3.Zero());
  camera.attachControl(canvas, false);
  var light = new BABYLON.HemisphericLight("light1", new BABYLON.Vector3(0, 1, 0), scene);
  light.diffuse = new BABYLON.Color3(1, 1, 1);

  // Populate the scene
  populateScene(scene, model);

  engine.runRenderLoop(function () {
    scene.render();
  });

  window.addEventListener("resize", function () {
    engine.resize();
  });
};
