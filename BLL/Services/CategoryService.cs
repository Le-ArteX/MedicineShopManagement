using AutoMapper;
using DAL.Repos;
using System.Collections.Generic;

namespace BLL.Services
{
    public class CategoryService
    {
        private readonly CategoryRepo categoryRepo;
        private Mapper mapper; 

        public CategoryService(CategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = MapperConfig.GetMapper();
        }
    
        public bool Create(DTOs.CategoryDTO c)
        {
            var category = mapper.Map<DAL.EF.Table.Category>(c);
            return categoryRepo.Create(category);
        }

        public DTOs.CategoryDTO Get(int id)
        {
            var category = categoryRepo.Get(id);
            return mapper.Map<DTOs.CategoryDTO>(category);
        }

        public List<DTOs.CategoryDTO> GetAll()
        {
            var categories = categoryRepo.GetAll();
            return mapper.Map<List<DTOs.CategoryDTO>>(categories);
        }

        public bool Update(DTOs.CategoryDTO c)
        {
            var category = mapper.Map<DAL.EF.Table.Category>(c);
            return categoryRepo.Update(category);
        }

        public bool Delete(int id)
        {
            return categoryRepo.Delete(id);
        }  
    }
}
