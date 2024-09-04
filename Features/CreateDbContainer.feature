Feature: CreateDbContainer

A short summary of the feature

#@tag1
Scenario Outline: Data Base Container
	Given I have a runnig MySQL container
	When I connect to the Database container
	Then The Database should be accesible
