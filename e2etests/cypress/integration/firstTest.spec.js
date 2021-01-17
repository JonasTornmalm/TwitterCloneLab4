describe('My test suite', () => {
	before(() => {
		cy.intercept('GET', 'http://localhost:5300/api/Messaging').as('getMessages')
	})

	after(() => {
		cy.request(
			'DELETE',
			'http://localhost:5300/api/Messaging',
			{ "text": "take out the bins" }
		)
	})

	beforeEach(() => {
		cy.visit("http://localhost:5200/Message");
	})

	it('OK, register and create a message', () => {
		cy.visit("http://localhost:5200/UserLogin");

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
		cy.get('input[cy-data="new-message"]').type('take out the bins');
		cy.get('#SubmitButton').click();

		cy.get('#message-table').find('tbody').find('tr').should('have.length', 1);

		cy.visit('http://localhost:5200/userregister');
		cy.get('#deleteUser').click();
	})
})