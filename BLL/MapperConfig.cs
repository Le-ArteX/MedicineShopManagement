#nullable disable
using AutoMapper;
using BLL.DTOs;
using DAL.EF.Table;

namespace BLL
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Category, CategoryDTO>().ReverseMap(); ;

            });

            return mappingConfig;
        }

        public static Mapper GetMapper()
        {
            var config = RegisterMaps();
            return new Mapper(config);
        }
    }
}
