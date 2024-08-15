Feature: MockApi

A short summary of the feature

#@tag1
Scenario: [Get Auth Token]
	Given auth endpoint its setting up
	When auth endpoint is sent
	Then response has correct token and status
	
#@tag2
Scenario: [Get Booking Ids]
	Given auth endpoint its setting up
	When auth endpoint is sent
	Then response has correct token and status
