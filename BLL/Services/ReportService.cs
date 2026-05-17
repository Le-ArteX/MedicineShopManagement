using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTOs;
using DAL.Repos;

namespace BLL.Services
{
    public class ReportService
    {
        private readonly ReportRepo _reportRepo;

        public ReportService(ReportRepo reportRepo)
        {
            _reportRepo = reportRepo;
        }

        public DashboardSummaryDTO GetDashboardSummary()
        {
            var summary = new DashboardSummaryDTO
            {
                TotalSalesRevenue = _reportRepo.GetTotalSalesRevenue(),
                TotalSalesCount = _reportRepo.GetTotalSalesCount(),
                TotalPurchaseCost = _reportRepo.GetTotalPurchaseCost(),
                TotalPurchaseCount = _reportRepo.GetTotalPurchaseCount(),
                TotalMedicines = _reportRepo.GetTotalMedicinesCount(),
                LowStockMedicineCount = _reportRepo.GetLowStockMedicines().Count
            };

            return summary;
        }

        public List<LowStockMedicineDTO> GetLowStockMedicines()
        {
            var medicines = _reportRepo.GetLowStockMedicines();

            return medicines.Select(m => new LowStockMedicineDTO
            {
                MedicineId = m.MedicineId,
                Name = m.Name,
                Brand = m.Brand,
                StockQuantity = m.StockQuantity,
                MinStockLevel = m.MinStockLevel
            }).ToList();
        }
    }
}
