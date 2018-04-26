/**
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
'use strict'

/* global THREE */

/**
 * Draw a pack
 *
 * @param {HTMLElement} container - The DOM container to draw in
 * @param {string} pack - The pack as a json string
 *
 * @returns {undefined}
 */
var drawPack = function (container, pack) {
  var model = JSON.parse(pack)
  var result = model.Stages[0].Results[0]
  var scene = new THREE.Scene()
  var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000)

  var renderer = new THREE.WebGLRenderer()
  renderer.setSize(window.innerWidth, window.innerHeight)
  container.appendChild(renderer.domElement)

  var geometry = new THREE.BoxGeometry(result.ExternalLength, result.ExternalBreadth, result.ExternalHeight, 1, 1, 1)
  var material = new THREE.MeshBasicMaterial({ color: 0x00ff00 })
  var cube = new THREE.Mesh(geometry, material)
  scene.add(cube)

  camera.position.z = result.ExternalHeight * 3

  cube.rotation.x = 45
  cube.rotation.y = 45
  cube.rotation.z = 270
  renderer.render(scene, camera)
}
