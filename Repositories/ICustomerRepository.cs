using PocRedis.Models;

namespace PocRedis.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();

        Task<Customer> GetCustomer(Guid id);
    }
}
