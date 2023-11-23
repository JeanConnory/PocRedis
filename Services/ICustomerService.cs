using PocRedis.Models;

namespace PocRedis.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomer(Guid id);

        Task<IEnumerable<Customer>> GetCustomers();

        Task<IEnumerable<Customer>> GetExternalCustomers();
    }
}
