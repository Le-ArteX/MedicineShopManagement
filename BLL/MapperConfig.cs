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
                config.CreateMap<Category, CategoryDTO>().ReverseMap();
                config.CreateMap<Customer, CustomerDTO>().ReverseMap();
                config.CreateMap<Medicine, MedicineDTO>().ReverseMap();
                config.CreateMap<Supplier, SupplierDTO>().ReverseMap();
                config.CreateMap<Purchase, PurchaseDTO>().ReverseMap();
                config.CreateMap<Sale, SaleDTO>().ReverseMap();
                config.CreateMap<PurchaseItem, PurchaseItemDTO>().ReverseMap();
                config.CreateMap<User, UserDTO>().ReverseMap();
                config.CreateMap<Report, ReportDTO>().ReverseMap();

            });

            return mappingConfig;
        }

        public static Mapper GetMapper()
        {
            var config = RegisterMaps();
            return new Mapper(config);
        }
    }

    internal class Report
    {
    }
}
