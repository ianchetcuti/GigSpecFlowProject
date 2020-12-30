Feature: Task 1 – RESTful API tests
![APITesting](https://www.gig.com/wp-content/themes/gig.com-child/assets/images/logo.svg)

In order to fulfil task 1 of the GiG Assignment
As a SWD in Test
I *want* to assert the **successful registration** the **unsuccessful registration** and **list user** flows

Link to a feature: [Api](https://github.com/ianchetcuti/GigSpecFlowProject/blob/master/SpecFlowGiGAssignment/Features/Api.feature)
***Github Repo***: **[Learn more](https://github.com/ianchetcuti/GigSpecFlowProject)**

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