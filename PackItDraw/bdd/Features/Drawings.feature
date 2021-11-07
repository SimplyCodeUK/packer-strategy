Feature: Drawings Endpoint

    Background:
        Given the Drawings service

    @draw
    Scenario: Get the service version
        When we request the service version
        Then we get the HTTP status code 200
