/*!
 * @copyright Simply Code Ltd. All rights reserved.
 * @license Licensed under the MIT License.
 * See LICENSE file in the project root for full license information.
 */
"use strict";

/**
 * Find the closest parent node with a matching node name
 *
 * @param {HTMLElement} e - The node
 * @param {string} name - The parent node name to search for
 *
 * @returns {HTMLElement} The parent DOM
 */
function closest (e, name) {
  let parent = e.parentNode;
  while (parent !== null) {
    if (parent.nodeName === name) {
      return parent;
    }
    parent = parent.parentNode;
  }
  return parent;
}

/**
 * Delete a row from a table
 *
 * @param {HTMLElement} e - The delete button pressed on the row to delete
 */
function deleteRow (e) {
  closest(e, "TABLE").deleteRow(closest(e, "TR").rowIndex);
};

/**
 * Add a row from a table
 *
 * @param {HTMLElement} e - The button pressed to add a row
 * @param {string} getUrl - The url for the partial view
 */
function addRow (e, getUrl) {
  let body = closest(e, "TABLE").getElementsByTagName("TBODY")[0];
  let count = body.getElementsByTagName("tr").length;
  let xhr = new XMLHttpRequest();
  xhr.onload = function () {
    if (xhr.readyState === 4 && xhr.status === 200) {
      body.insertAdjacentHTML("beforeend", xhr.responseText);
    }
  };
  xhr.open("POST", getUrl, true);
  xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
  xhr.send(JSON.stringify({ index: count }));
};
