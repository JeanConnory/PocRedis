using PocRedis.Models;
using PocRedis.Repositories;

namespace PocRedis.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly ICustomerRepository _customerRepository;
        private const string CACHE_COLLECTION_KEY = "_allcustomers";

        public CustomerService(ICacheRepository cacheRepository, ICustomerRepository customerRepository)
        {
            _cacheRepository = cacheRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            var customer = await _cacheRepository.GetValue<Customer>(id);

            if (customer is null)
            {
                customer = await _customerRepository.GetCustomer(id);
                await _cacheRepository.SetValue(id, customer);
            }

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await _cacheRepository.GetCollection<Customer>(CACHE_COLLECTION_KEY);

            if (customers is null || !customers.Any())
            {
                customers = await _customerRepository.GetCustomers();
                await _cacheRepository.SetCollection(CACHE_COLLECTION_KEY, customers);
            }

            return customers;
        }

        public async Task<IEnumerable<Customer>> GetExternalCustomers()
        {
            var cacheKey = "_externalcustomers";
            var customers = await _cacheRepository.GetCollection<Customer>(cacheKey);

            if (customers is null || !customers.Any())
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:7202/api/externalCustomer");

                    if(response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        customers = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Customer>>(responseData);
                        await _cacheRepository.SetCollection(cacheKey, customers);
                    }
                }
            }

            return customers;
        }
    }
}
