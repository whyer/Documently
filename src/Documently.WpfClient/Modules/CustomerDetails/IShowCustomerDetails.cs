using System;

namespace Documently.WpfClient.Modules.CustomerDetails
{
    public interface IShowCustomerDetails
    {
        void WithCustomer(string customerId);

        Guid GetCustomerId();
    }
}