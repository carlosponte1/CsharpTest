Feature: Login

A short summary of the feature

#@tag1
Scenario: [scenario Login]
	Given I Wanto to go the Login Page
	When I want to login with the user "<text>"
	Then login success with  "<text>"

	Examples:
	| text			  | 
	| standard_user	  | 
	| locked_out_user | 

