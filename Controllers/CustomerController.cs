using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Services;
using Project.API.DTO;

namespace PetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("/list-all")]
        public async Task<IActionResult> List()
        {
            var data = await this._customerService.ListCustomers();
            return Ok(data);
        }
        [HttpPost("/create")]
        public async Task<IActionResult> CreateCustomer([FromQuery] CustomerDTO customerDTO)
        {
            var data = await this._customerService.CreateCustomer(customerDTO);
            return Ok(data);
        }
        [HttpPut("/update")]
        public async Task<IActionResult> UpdateCustomer([FromQuery] CustomerDTO customerDTO)
        {
            var data = await this._customerService.UpdateCustomer(customerDTO);
            return Ok(data);
        }
        [HttpDelete("/delete")] 
        public async Task<IActionResult> DeleteCustomer([FromQuery] CustomerDTO customerDTO)
        {
            var data = await this._customerService.DeleteCustomer(customerDTO.Id);
            return Ok(data);
        }
        [HttpGet("/get")]
        public async Task<IActionResult> GetCustomerById([FromQuery] CustomerDTO customerDTO)
        {
            var data = await this._customerService.GetCustomerById(customerDTO.Id);
            return Ok(data);
        }

    }
}
