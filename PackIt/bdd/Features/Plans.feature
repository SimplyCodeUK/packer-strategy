Feature: Plans Endpoint

    Background:
        Given the Plans service

    @plans
    Scenario: Get the service version
        When we request the service version
        Then we get the HTTP status code 200
