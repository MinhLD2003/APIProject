using Project.API.DTO;
using Project.API.Helper;
using Project.API.Models;

namespace PetProject.API.Services
{
    public interface ICustomerService
    {
        public Task<List<CustomerDTO>> ListCustomers();
        public Task<CustomerDTO> GetCustomerById(int id);
        public Task<APIResponse>CreateCustomer(CustomerDTO customerDTO);
        public Task<APIResponse> UpdateCustomer(CustomerDTO customerDTO);

        public Task<APIResponse> DeleteCustomer(int id);

    }
}
