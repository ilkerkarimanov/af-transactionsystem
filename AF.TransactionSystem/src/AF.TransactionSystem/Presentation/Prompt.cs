using AF.TransactionSystem.Application;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AF.TransactionSystem.Presentation
{
    internal class Prompt
    {
        public static async Task Main()
        {
            int choice;
            while (true)
            {
                Console.WriteLine("====================");
                Console.WriteLine("0. Open Account\n");
                Console.WriteLine("1. Check Balance\n");
                Console.WriteLine("2. Withdraw\n");
                Console.WriteLine("3. Deposit\n");
                Console.WriteLine("4. Transfer\n");
                Console.WriteLine("====================");

                Console.WriteLine("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        await OpenAccount();
                        break;
                    case 1:
                        await CheckAccountBalance();
                        break;
                    case 2:
                        await Withdraw();
                        break;
                    case 3:
                        await Deposit();
                        break;
                    case 4:
                        await Transfer();
                        break;
                }
            }
        }

        private static async Task OpenAccount()
        {
            Console.WriteLine("====================");
            Console.WriteLine("Enter account number: ");
            var accountNumber = Console.ReadLine();
            Console.WriteLine("Enter first name: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter last name: ");
            var lastName = Console.ReadLine();
            Console.WriteLine("Enter opening balance: ");
            var openingBalance = Console.ReadLine();

            var request = new OpenAccountCommand(
                accountNumber,
                firstName,
                lastName,
                openingBalance
                );

            var sp = Bootstrapper.ServiceProvider;
            var validator = sp.GetRequiredService<IValidator<OpenAccountCommand>>();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                await Main();
            }
            else
            {
                try
                {
                    var mediator = sp.GetRequiredService<IMediator>();
                    await mediator.Send(request);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                await Main();
            }
        }

        private static async Task CheckAccountBalance()
        {
            Console.WriteLine("====================");
            Console.WriteLine("Enter account number: ");
            var accountNumber = Console.ReadLine();

            var request = new CheckBalanceQuery(accountNumber);

            var sp = Bootstrapper.ServiceProvider;
            var validator = sp.GetRequiredService<IValidator<CheckBalanceQuery>>();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                await Main();
            }
            else
            {
                try
                {
                    var mediator = sp.GetRequiredService<IMediator>();
                    var requestResult = await mediator.Send(request);
                    Console.WriteLine($"Balance: {requestResult.Amount}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                await Main();
            }
        }

        private static async Task Withdraw()
        {
            Console.WriteLine("====================");
            Console.WriteLine("Enter account number: ");
            var accountNumber = Console.ReadLine();
            Console.WriteLine("Enter amount: ");
            var amount = Console.ReadLine();

            var request = new WithdrawCommand(
                accountNumber,
                amount
                );

            var sp = Bootstrapper.ServiceProvider;
            var validator = sp.GetRequiredService<IValidator<WithdrawCommand>>();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                await Main();
            }
            else
            {
                try
                {
                    var mediator = sp.GetRequiredService<IMediator>();
                    await mediator.Send(request);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                await Main();
            }
        }

        private static async Task Deposit()
        {
            Console.WriteLine("====================");
            Console.WriteLine("Enter account number: ");
            var accountNumber = Console.ReadLine();
            Console.WriteLine("Enter amount: ");
            var amount = Console.ReadLine();

            var request = new DepositCommand(
                accountNumber,
                amount
                );

            var sp = Bootstrapper.ServiceProvider;
            var validator = sp.GetRequiredService<IValidator<DepositCommand>>();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                await Main();
            }
            else
            {
                try
                {
                    var mediator = sp.GetRequiredService<IMediator>();
                    await mediator.Send(request);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                await Main();
            }
        }

        private static async Task Transfer()
        {
            Console.WriteLine("====================");
            Console.WriteLine("Enter from account number: ");
            var fromAccountNumber = Console.ReadLine();
            Console.WriteLine("Enter to account number: ");
            var toAccountNumber = Console.ReadLine();
            Console.WriteLine("Enter amount: ");
            var amount = Console.ReadLine();

            var request = new TransferCommand(
                fromAccountNumber,
                toAccountNumber,
                amount
                );

            var sp = Bootstrapper.ServiceProvider;
            var validator = sp.GetRequiredService<IValidator<TransferCommand>>();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                await Main();
            }
            else
            {
                try
                {
                    var mediator = sp.GetRequiredService<IMediator>();
                    await mediator.Send(request);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                await Main();
            }
        }
    }
}
