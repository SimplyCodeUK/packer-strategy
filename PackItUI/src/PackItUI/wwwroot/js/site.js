﻿// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.

// find the closest parent node with a matching node name
//
// @param e - the node
// @param name - the parent node name to search for
function closest( e, name ) {
  var parent = e.parentNode
  while (parent != null) {
    if (parent.nodeName === name) {
      return parent
    }
    parent = parent.parentNode
  }
  return parent
}

// delete a row from a table
//
// @param e - the button to delete
function deleteRow( e ) {
  closest(e, 'TABLE').deleteRow(closest(e, 'TR').rowIndex)
}

// add a row from a table
//
// @param e      - the button to delete
// @param getUrl - the url for the partial view
function addRow( e, getUrl ) {
  var body = closest(e, 'TABLE').getElementsByTagName('TBODY')[0]
  var xhr = new XMLHttpRequest()
  xhr.open('GET', getUrl, false)
  xhr.send()

  if (xhr.readyState === 4 && xhr.status === 200) {
    body.insertAdjacentHTML('beforeend', xhr.responseText)
  }
}
