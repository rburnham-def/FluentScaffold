Feature: Shopping Cart
	As a User
	I want to be able to add an Items to my shopping cart 
	So that i can purchase them all at once 

@shopping-cart
Scenario: Add multiple items to Shopping Cart
	Given I am Authenticated as the User
	| Field       | Value                                |
	| Name        | Bob                                  |
	| Email       | bob@test.com                         |
	| Password    | supperS3cret                         |
	| DateOfBirth | 1990-03-01                           |
	When I Add an Item to my Shopping Cart
	Then I should see these Items in my Shopping Cart
 
	
	
	
Scenario: Underaged User is Unable to Purchase Age Restricted Items
	Given the Items
	And I am Authenticated as the User
	  | Field       | Value        |
	  | Name        | Bob          |
	  | Email       | bob@test.com |
	  | Password    | supperS3cret |
	  | DateOfBirth | 1990-03-01   |
	When I Add an Item to my Shopping Cart
	
	Then I should see these Items in my Shopping Cart