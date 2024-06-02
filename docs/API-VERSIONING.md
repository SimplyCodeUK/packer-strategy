# How to version an API

+ [Acronyms](#acronyms)
+ [Basics](#basics)
+ [When to change versions](#when-to-change-versions)

<a name="acronyms"></a>

## Acronyms

| Acronym | Description                   |
| :------ | :----------                   |
| API     | Application Program Interface |
| HTTP    | Hypertext Transfer Protocol   |
| URI     | Uniform Resource Identifier   |

<a name="basics"></a>

## Basics

APIs are versioned using a **MAJOR** number only. The version of the API appears in it's URI.

For example, this URI structure is used to request version 1 of the 'plans' API:

**https:\/\/packit.org\/v1\/plans**

and this to request version 2:

**https:\/\/packit.org\/v2\/plans**

Follow the Semantic Versioning 2.0.0 standard for **MAJOR** number format, see [here](<http://semver.org/spec/v2.0.0.html>)

<a name="when-to-change-versions"></a>

## When to change versions

The API version number indicates a certain level of backwards-compatibility for the API behaviour.
API clients depend on the expected level of compatibility, and extra care should be taken to maintain their trust.

The following types of changes **DO** require a new API version number:

+ Removed or renamed resource
+ Any **mandatory** change on an existing request
  + e.g. New header required
  + e.g. New data in the body of the request

The following types of changes **DO NOT** require a new API version number:

+ New resource added
+ New HTTP method on an existing resource added
+ Optional additional data is required for a request
+ Request marked as deprecated but still existing (i.e. Requests no longer supported)