Feature: Task 2 - Kafka Tests
	In order to write something here
	As a lazy bastard
	I dont want to write anything

@Test1_-_Happy_Path_1_Car
Scenario: Produce 1 Message Of Type Car
	Given the Brand is a "Toyota" and the model is a "Celica" and the number of doors are 2 and it "is" a sports car
	When the car messages are published
	Then the total consumed messages are 1

@Test2_-_Happy_Path_Multiple_Cars
Scenario: Produce 3 Messages Of Type Car
	Given the Brand is a "Mitsubishi" and the model is a "3000GT" and the number of doors are 2 and it "is" a sports car
	And the Brand is a "Honda" and the model is a "Civic" and the number of doors are 2 and it "isnt" a sports car
	And the Brand is a "Renault" and the model is a "2cv" and the number of doors are 4 and it "isnt" a sports car
	When the car messages are published
	Then the total consumed messages are 3