using AutoMapper;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class CustomerService
    {
       
            private readonly CustomerRepo customerRepo;
            private Mapper mapper;

            public CustomerService(CustomerRepo customerRepo)
            {
                this.customerRepo = customerRepo;
                this.mapper = MapperConfig.GetMapper();
            }

            public bool Create(DTOs.CustomerDTO c)
            {
              if(customerRepo.EmailExists(c.Email))
            {
                return false;
            }
            var customer = mapper.Map<DAL.EF.Table.Customer>(c);
                return customerRepo.Create(customer);
            }

            public DTOs.CustomerDTO Get(int id)
            {
                var customer = customerRepo.Get(id);
                return mapper.Map<DTOs.CustomerDTO>(customer);
            }

            public List<DTOs.CustomerDTO> GetAll()
            {
                var customers = customerRepo.GetAll();
                return mapper.Map<List<DTOs.CustomerDTO>>(customers);
            }

            public bool Update(DTOs.CustomerDTO c)
            {
                var customer = mapper.Map<DAL.EF.Table.Customer>(c);
                return customerRepo.Update(customer);
            }

            public bool Delete(int id)
            {
                return customerRepo.Delete(id);
            }
        
    }
}
