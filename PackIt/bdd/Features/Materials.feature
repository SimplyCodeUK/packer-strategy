Feature: Materials Endpoint

    Background:
        Given the Materials service

    @materials
    Scenario: Get the service version
        When we request the service version
        Then we get the HTTP status code 200
