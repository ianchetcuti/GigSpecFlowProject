Feature: Task 2 - Kafka Tests
![StreamTesting](https://www.gig.com/wp-content/themes/gig.com-child/assets/images/logo.svg)

In order to fulfil task 2 of the GiG Assignment
As a SWD in Test
I *want* to be able to **publish kafka messages**, **consume kafka messages** and **assert that they both tally up*

Link to a feature: [Api](https://github.com/ianchetcuti/GigSpecFlowProject/blob/master/SpecFlowGiGAssignment/Features/Kafka.feature)
***Github Repo***: **[Learn more](https://github.com/ianchetcuti/GigSpecFlowProject)**

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