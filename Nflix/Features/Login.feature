Feature: Login

Background:
	Given the access to the login page

Scenario: Login Successfully
	When a user do the login with "tony@stark.com" and "123456"
	Then the user must be authenticated
	And the name "Tony Stark" must be seen in the logging area

Scenario Outline: Login Failed
	When a user do the login with <email> and <password>
	Then the user must not be authenticated
	And the alert message <message> must be seen

	Examples:
		| email             | password  | message                        |
		| "tony@stark.com"  | "abc123"  | "Usuário e/ou senha inválidos" |
		| "404@yahoo.com"   | "abc123"  | "Usuário e/ou senha inválidos" |
		| ""                | "abcxpto" | "Opps. Cadê o email?"          |
		| "teste@gmail.com" | ""        | "Opps. Cadê a senha?"          |