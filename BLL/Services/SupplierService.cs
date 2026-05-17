using AutoMapper;
using BLL.DTOs;
using DAL.Repos;
using System.Collections.Generic;

namespace BLL.Services
{
    public class SupplierService
    {
        private readonly SupplierRepo supplierRepo;
        private Mapper mapper;

        public SupplierService(SupplierRepo supplierRepo)
        {
            this.supplierRepo = supplierRepo;
            this.mapper = MapperConfig.GetMapper();
        }

        public bool Create(SupplierDTO s)
        {
            var supplier = mapper.Map<DAL.EF.Table.Supplier>(s);
            return supplierRepo.Create(supplier);
        }

        public SupplierDTO Get(int id)
        {
            var supplier = supplierRepo.Get(id);
            return mapper.Map<SupplierDTO>(supplier);
        }

        public List<SupplierDTO> GetAll()
        {
            var suppliers = supplierRepo.GetAll();
            return mapper.Map<List<SupplierDTO>>(suppliers);
        }

        public bool Update(SupplierDTO s)
        {
            var supplier = mapper.Map<DAL.EF.Table.Supplier>(s);
            return supplierRepo.Update(supplier);
        }

        public bool Delete(int id)
        {
            return supplierRepo.Delete(id);
        }
    }
}
