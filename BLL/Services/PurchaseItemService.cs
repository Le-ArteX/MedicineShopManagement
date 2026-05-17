using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.DTOs;
using DAL.Repos;
using DAL.EF.Table;

namespace BLL.Services
{
    public class PurchaseItemService
    {
        private readonly PurchaseItemRepo _purchaseItemRepo;
        private readonly Mapper _mapper;

        public PurchaseItemService(PurchaseItemRepo purchaseItemRepo)
        {
            _purchaseItemRepo = purchaseItemRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public bool Create(PurchaseItemDTO obj)
        {
            var data = _mapper.Map<PurchaseItem>(obj);
            return _purchaseItemRepo.Create(data);
        }

        public List<PurchaseItemDTO> Get()
        {
            var data = _purchaseItemRepo.Get();
            return _mapper.Map<List<PurchaseItemDTO>>(data);
        }

        public PurchaseItemDTO Get(int id)
        {
            var data = _purchaseItemRepo.Get(id);
            return _mapper.Map<PurchaseItemDTO>(data);
        }

        public bool Update(PurchaseItemDTO obj)
        {
            var data = _mapper.Map<PurchaseItem>(obj);
            return _purchaseItemRepo.Update(data);
        }

        public bool Delete(int id)
        {
            return _purchaseItemRepo.Delete(id);
        }
    }
}
