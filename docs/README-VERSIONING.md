# Service, and API versioning

+ [Acronyms](#acronyms)
+ [Overview](#overview)
+ [Use Cases when updating the version of the API and Services](#use-cases-when-updating-the-version-of-the-api-and-services)
+ [Version change examples](#version-change-examples)
  + [Defect fix on the plans service](#defect-fix-on-the-plans-service)
  + [Add functionality to the plans service](#add-functionality-to-the-plans-service)
  + [Change or remove functionality to the plans service](#change-or-remove-functionality-to-the-plans-service)

<a name="acronyms"></a>

## Acronyms

| Acronym | Description                   |
| :------ | :----------                   |
| API     | Application Program Interface |

<a name="overview"></a>

## Overview

A PackIt Platform release consists of a specific versioned set of services,
which expose RESTful APIs. All RESTful APIs are also versioned.

Release notes are published for each release summarising the release changes.

Versioning policies are important internally, to manage software changes and
ensure that correct component versions are deployed together, and externally,
to ensure compatibility for external clients.

This document summarises the relevant versioning policies.

<a name="use-cases-when-updating-the-version-of-the-api-and-services"></a>

## Use Cases when updating the version of the API and Services

| Use Case             | Service Version                   | API Version |
| :--------            | :--------------                   | :---------- |
| Defect Fix           | Patch change to services impacted | No change   |
| Add functionality    | Minor change to services impacted | No change   |
| Remove functionality | Major change to services impacted | Increment   |
| Change functionality | Major change to services impacted | Increment   |

<a name="version-change-examples"></a>

## Version change examples

<a name="defect-fix-on-the-plans-service"></a>

### Defect fix on the plans service

|             | Service Version     | API Version |
| :----       | :--------------     | :---------- |
| Pre change  | Plans service 1.0.0 | v1          |
| Post change | Plans service 1.0.1 | v1          |

<a name="add-functionality-to-the-plans-service"></a>

### Add functionality to the plans service

|             | Service Version     | API Version |
| :----       | :--------------     | :---------- |
| Pre change  | Plans service 1.0.1 | v1          |
| Post change | Plans service 1.1.0 | v1          |

<a name="change-or-remove-functionality-to-the-plans-service"></a>

### Change or remove functionality to the plans service

|             | Service Version     | API Version |
| :----       | :--------------     | :---------- |
| Pre change  | Plans service 1.0.1 | v1          |
| Post change | Plans service 2.0.0 | v2          |
