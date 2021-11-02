Feature: Uploads Endpoint

    Background:
        Given the Uploads service

    @uploads
    Scenario: Get the service version
        When we request the service version
        Then we get the HTTP status code 200
