using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Project.API.DTO;
using Project.API.Helper;
using Project.API.Models;

namespace PetProject.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;


        public CustomerService(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<APIResponse> CreateCustomer(CustomerDTO customerDTO)
        {
            APIResponse _response = new APIResponse();
            try
            {
                TblCustomer customer = this._mapper.Map<CustomerDTO, TblCustomer>(customerDTO);
                await this._databaseContext.TblCustomers.AddAsync(customer);
                await this._databaseContext.SaveChangesAsync();
                _response.ResponseCode = 200;
                _response.Result = "OK";

            }
            catch (Exception ex)
            {
                _response.ResponseCode = 400;
                _response.Result = "FAIL";
            }
            return _response;

        }
        public async Task<APIResponse> UpdateCustomer(CustomerDTO customerDTO)
        {
            APIResponse _response = new APIResponse();
            try
            {
                var _customer = this._databaseContext.TblCustomers.Where(x => x.Id == customerDTO.Id).FirstOrDefault();
                if (_customer != null)
                {
                    _customer.Name = customerDTO.Name;
                    _customer.Phone = customerDTO.Phone;
                    _customer.Email = customerDTO.Email;
                    _customer.CreditLimit = customerDTO.CreditLimit;
                    await this._databaseContext.SaveChangesAsync();
                    _response.ResponseCode = 200;
                    _response.Result = "OK";
                }
                else
                {
                    _response.ResponseCode = 400;
                    _response.Result = "FAIL";
                }
            }
            catch (Exception ex)
            {
                _response.ResponseCode = 400;
                _response.Result = "FAIL";
            }
            return _response;
        }
        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            CustomerDTO customer = new CustomerDTO();
            var _data = await this._databaseContext.TblCustomers.FindAsync(id);
            if (_data != null)
            {
                customer = this._mapper.Map<TblCustomer, CustomerDTO>(_data);

            }
            return customer;
        }


        public async Task<List<CustomerDTO>> ListCustomers()
        {
            List<CustomerDTO> _response = new List<CustomerDTO>();
            var _data = await this._databaseContext.TblCustomers.ToListAsync();
            if (_data != null)
            {
                _response = this._mapper.Map<List<TblCustomer>, List<CustomerDTO>>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> DeleteCustomer(int id)
        {
            var _response = new APIResponse();
            try
            {
                var _data = this._databaseContext.TblCustomers.Where(item => item.Id == id).FirstOrDefault();
                if (_data != null)
                {
                    this._databaseContext.TblCustomers.Remove(_data);
                    await this._databaseContext.SaveChangesAsync();
                    _response.ResponseCode = 200;
                    _response.Result = "OK";
                }
                else
                {
                    _response.ResponseCode = 404;
                    _response.Message = "Data not found";
                }
            }
            catch (Exception ex)
            {
                _response.ResponseCode = 404;
                _response.Message = "Data not found";
            }
            return _response;
        }
    }

}
