using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.DTOs;
using DAL.Repos;
using DAL.EF.Table;

namespace BLL.Services
{
    public class PurchaseService
    {
        private readonly PurchaseRepo _purchaseRepo;
        private readonly Mapper _mapper;

        public PurchaseService(PurchaseRepo purchaseRepo)
        {
            _purchaseRepo = purchaseRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public bool Create(PurchaseDTO obj)
        {
            var data = _mapper.Map<Purchase>(obj);
            return _purchaseRepo.Create(data);
        }

        public List<PurchaseDTO> Get()
        {
            var data = _purchaseRepo.Get();
            return _mapper.Map<List<PurchaseDTO>>(data);
        }

        public PurchaseDTO Get(int id)
        {
            var data = _purchaseRepo.Get(id);
            return _mapper.Map<PurchaseDTO>(data);
        }

        public bool Update(PurchaseDTO obj)
        {
            var data = _mapper.Map<Purchase>(obj);
            return _purchaseRepo.Update(data);
        }

        public bool Delete(int id)
        {
            return _purchaseRepo.Delete(id);
        }
    }
}
