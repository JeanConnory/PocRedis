using PocRedis.Models;

namespace PocRedis.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            return await Task.FromResult(GenerateFakeCustomer());
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var fakeFirst = await Task.FromResult(GenerateFakeCustomer().FirstOrDefault());
            fakeFirst.Id = id;
            return fakeFirst;
        }


        private IEnumerable<Customer> GenerateFakeCustomer()
        {
            return Enumerable.Range(1, 20).Select(index => new Customer
            {
                Id = Guid.NewGuid(),
                Name = $"Customer {index}"
            });
        }
    }
}
