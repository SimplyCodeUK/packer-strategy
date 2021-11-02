Feature: Packs Endpoint

    Background:
        Given the Packs service

    @packs
    Scenario: Get the service version
        When we request the service version
        Then we get the HTTP status code 200
