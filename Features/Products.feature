Feature: Products

A short summary of the feature

#@tag1
Scenario: [Validate inventory Products]
	Given I Wanto to go the Login Page
	When I want to login with the user "<text>"
	Then the user should see exactly <Items> inventory items
	Examples:
	| text          | Items |
	| standard_user | 6     |


	#@tag1
Scenario: [Problem inventory Products]
	Given I Wanto to go the Login Page
	When I want to login with the user "<text>"
	Then all product images should be different
	Examples:
	| text          |
	| problem_user  |
	#| standard_user |
