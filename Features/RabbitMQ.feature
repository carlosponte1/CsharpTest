Feature: RabbitMQ

A short summary of the feature

@tag1
Scenario: RabbitMQ Message
	Given A running and ready container
	When the producer send a message
	Then the consumer recieve a message
