using DAL;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class RepositoryFactory
    {
        private static IServiceProvider _serviceProvider;

        // Method to set the service provider (called from Program.cs)
        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // Method to create an instance of IRepository
        public static IRepository CreateRepository()
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("Service provider is not configured. Call RepositoryFactory.Configure() in Program.cs.");

            return _serviceProvider.GetRequiredService<IRepository>();
        }
    }
}