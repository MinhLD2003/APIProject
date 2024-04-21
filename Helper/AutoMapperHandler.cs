using AutoMapper;
using Project.API.DTO;
using Project.API.Models;

namespace Project.API.Helper
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<TblCustomer, CustomerDTO>().ReverseMap();
            

        }
    }
}
