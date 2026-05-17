using AutoMapper;
using BLL.DTOs;
using DAL.Repos;
using System.Collections.Generic;

namespace BLL.Services
{
    public class MedicineService
    {
        private readonly MedicineRepo medicineRepo;
        private Mapper mapper;

        public MedicineService(MedicineRepo medicineRepo)
        {
            this.medicineRepo = medicineRepo;
            this.mapper = MapperConfig.GetMapper();
        }

        public bool Create(MedicineDTO m)
        {
            var medicine = mapper.Map<DAL.EF.Table.Medicine>(m);
            return medicineRepo.Create(medicine);
        }

        public MedicineDTO Get(int id)
        {
            var medicine = medicineRepo.Get(id);
            return mapper.Map<MedicineDTO>(medicine);
        }

        public List<MedicineDTO> GetAll()
        {
            var medicines = medicineRepo.GetAll();
            return mapper.Map<List<MedicineDTO>>(medicines);
        }

        public bool Update(MedicineDTO m)
        {
            var medicine = mapper.Map<DAL.EF.Table.Medicine>(m);
            return medicineRepo.Update(medicine);
        }

        public bool Delete(int id)
        {
            return medicineRepo.Delete(id);
        }
    }
}
