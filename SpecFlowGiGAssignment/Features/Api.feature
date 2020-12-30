Feature: Task 1 – RESTful API tests
![APITesting](https://www.gig.com/wp-content/themes/gig.com-child/assets/images/logo.svg)

	In order to fulfil task 1 of the GiG Assignment
	As a SWD in Test
	I *want* to assert the **successful registration** and **unsuccessful registration** flows.

Link to a feature: [Api](SpecFlowGiGAssignment/Features/Api.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@Registration_Test_1
Scenario: Successful registration
	Given the email is "janet.weaver@reqres.in"
	And the password is "weakpass"
	When the POST Request is sent to "https://reqres.in/api/register"
	Then the response should be HTTP 200 and a token

@Registration_Test_2
Scenario: UnSuccessful registration
	Given the email is "janet.weaver@reqres.in"
	When the POST Request is sent to "https://reqres.in/api/register"
	Then the response should be HTTP 400 and an error

@User_Test_1
Scenario: List users
	When the GET Request is sent to "https://reqres.in/api/users"
	Then the response should be HTTP 200 and a list of users