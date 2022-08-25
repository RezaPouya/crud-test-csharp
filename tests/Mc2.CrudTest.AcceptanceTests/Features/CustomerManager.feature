
Feature: Customer Manager
	In order to manage customers 
	As a user 
	I want to be able perform mangment operations on customers

	Background:
		Given system error codes are following
		  | Code | Description                                                |
		  | 101  | Invalid Mobile Number                                      |
		  | 102  | Invalid Email address                                      |
		  | 103  | Invalid Bank Account Number                                |
		  | 201  | Duplicate customer by First-name, Last-name, Date-of-Birth |
		  | 202  | Duplicate customer by Email address                        |
    
	Scenario Outline: Create Read Edit Delete Customer
		Given we have a customer with Firstname of <FirstName>
				And Lastname of <LastName>
				And Date of birth of <DateOfBirth>
				And Email of <Email>
				And Phone number of <PhoneNumber>
				And Bank Account Number of <BankAccountNumber>
		When user create customer with given info
			Then user can lookup all customers and filter by Email of <Email> and get "1" records
		When user edit customer with new email of "new@email.com"
			Then user can lookup all customers and filter by Email of <Email> and get "0" records
			And user can lookup all customers and filter by Email of "new@email.com" and get "1" records
		When user delete customer by Email of "new@email.com"
			Then user can lookup all customers and filter by Email of "new@email.com" and get "0" records
			And user can lookup all customers and filter by Email of <Email> and get "0" records

        Examples:
          | ID | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | 1  | John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |

	Scenario: Validate Phone number on Create 
		Given  we have a customer with these info 
			| FirstName | LastName | Email          | PhoneNumber   | DateOfBirth | BankAccountNumber |
			| John      | Smith    | john@smith.com | +934567	    | 01-JAN-1988 | IR000000000000001 |
		Then when we try to create customer, it should fail 
			And error message should be "Invalid Mobile Number"

			