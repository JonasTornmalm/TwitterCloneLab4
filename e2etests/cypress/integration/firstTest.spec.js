describe('My test suite', () => {
	before(() => {
		cy.intercept('GET', 'http://localhost:8080/api/Messaging').as('getMessages')
		cy.wait(['@getMessages'])
    })

	beforeEach(() => {
		cy.visit("http://localhost:5200/Message");
	})


	it('OK, create a message', () => {
		var input = cy.get('input[cy-data="new-message"]');
		input.type('take out the bins');
		cy.get('#SubmitButton').click();

		if (input.innerhtml !== null) {
			cy.visit("http://localhost:5200/UserLogin");
		}
		else {

		}
		cy.get('#inputMail').type('jonas@mail.se');
		cy.get('#inputPassword').type('Adam123!');
		cy.get('#SubmitButton').click();

		cy.get('.validation-summary-errors').find('ul').should('have.length', 1)
			.then(() => {
				cy.visit("http://localhost:5200/UserRegister/create");
				cy.get('#User_FirstName').type('fake');
				cy.get('#User_LastName').type('fake');
				cy.get('#User_EmailAddress').type('fake@mail.se');
				cy.get('#User_Password').type('Fake123!');
				cy.get('#SubmitButton').click();
			})

		cy.get('#inputMail').type('fake@mail.se');
		cy.get('#inputPassword').type('Fake123!');
		cy.get('#SubmitButton').click();
		

		cy.visit('http://localhost:5200/Message');
		var input = cy.get('input[cy-data="new-message"]').type('Logged in now, take out the bins');
		cy.get('#SubmitButton').click();

		cy.get('#message-table').find('tbody').find('tr').should('have.length', 1);
	})
})