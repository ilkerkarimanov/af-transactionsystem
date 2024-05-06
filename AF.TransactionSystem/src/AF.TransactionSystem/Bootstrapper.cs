using AF.TransactionSystem.Domain;
using AF.TransactionSystem.Infrastructure;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AF.TransactionSystem
{
    internal static class Bootstrapper
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        static Bootstrapper()
        {
            var services = new ServiceCollection();

            services.AddLogging(configure =>
            {
                configure.AddConsole();
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddSingleton<IAccountRepository, AccountStore>();

            ServiceProvider =  services.BuildServiceProvider();
        }
    }
}
