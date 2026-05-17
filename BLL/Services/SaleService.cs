using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.DTOs;
using DAL.Repos;
using DAL.EF.Table;

namespace BLL.Services
{
    public class SaleService
    {
        private readonly SaleRepo _saleRepo;
        private readonly Mapper _mapper;

        public SaleService(SaleRepo saleRepo)
        {
            _saleRepo = saleRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public bool Create(SaleDTO obj)
        {
            var data = _mapper.Map<Sale>(obj);
            return _saleRepo.Create(data);
        }

        public List<SaleDTO> Get()
        {
            var data = _saleRepo.Get();
            return _mapper.Map<List<SaleDTO>>(data);
        }

        public SaleDTO Get(int id)
        {
            var data = _saleRepo.Get(id);
            return _mapper.Map<SaleDTO>(data);
        }

        public bool Update(SaleDTO obj)
        {
            var data = _mapper.Map<Sale>(obj);
            return _saleRepo.Update(data);
        }

        public bool Delete(int id)
        {
            return _saleRepo.Delete(id);
        }
    }
}
