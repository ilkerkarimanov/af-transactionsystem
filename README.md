# af-transactionsystem
DDD Showcase presentation

- Domain Driven Design example of ATM financial system
- Hexagonal (Clean) Architecture sample

Rich Domain Model with:
1. Ardalis.SharedKernel for base Entity, ValueObject, and Identification nomenclature
2. Ardalis.GuardClause for invariant strengthening of the core domain composition

Persistence with:
1. EF Core Fluent Configuration for relation model mapping
2. Repository and Unit of Work with DbContext interface
   
Test coverage with xUnit.

Business rules and boundary context
1. Create an Account
- Users can create a new account by providing their name, initial balance, and a unique
account number.
- The system should store account details (name, balance, account number).
2. Deposit Money
- Users can deposit a specified amount of money into their account.
- The system should update the account balance accordingly.
3. Withdraw Money
- Users can withdraw a specified amount of money from their account.
- The system should verify that the account has sufficient balance before allowing the
withdrawal.
4. Check Account Balance
- Users can inquire about their current account balance.
- The system should display the account balance.
