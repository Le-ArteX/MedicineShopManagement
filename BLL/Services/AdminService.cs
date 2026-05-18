using AutoMapper;
using BLL.DTOs;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class AdminService
    {
        private readonly ReportRepo _reportRepo;
        private readonly SaleRepo _saleRepo;
        private readonly CustomerRepo _customerRepo;
        private readonly SupplierRepo _supplierRepo;
        private readonly UserRepo _userRepo;
        private readonly Mapper _mapper;

        public AdminService(ReportRepo reportRepo, SaleRepo saleRepo,
            CustomerRepo customerRepo, SupplierRepo supplierRepo, UserRepo userRepo)
        {
            _reportRepo = reportRepo;
            _saleRepo = saleRepo;
            _customerRepo = customerRepo;
            _supplierRepo = supplierRepo;
            _userRepo = userRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public AdminDTO GetDashboardData()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var currentMonth = today.Month;
            var currentYear = today.Year;

            var allSales = _saleRepo.Get();

            var staffUsers = _userRepo.GetByRole("Staff");

            var dashboardData = new AdminDTO
            {
                TotalMedicines = _reportRepo.GetTotalMedicinesCount(),
                TotalSales = _reportRepo.GetTotalSalesCount(),
                TotalPurchases = _reportRepo.GetTotalPurchaseCount(),

                TotalCustomers = _customerRepo.GetAll().Count,
                TotalSuppliers = _supplierRepo.GetAll().Count,
                TotalStaff = staffUsers.Count,

                TodayRevenue = allSales
                                .Where(s => s.SaleDate == today)
                                .Sum(s => (decimal)s.TotalAmount),

                MonthlyRevenue = allSales
                                .Where(s => s.SaleDate.Month == currentMonth
                                && s.SaleDate.Year == currentYear)
                                .Sum(s => (decimal)s.TotalAmount),

                ActiveAlertsCount = _reportRepo.GetLowStockMedicines().Count,

                RecentSales = _mapper.Map<List<SaleDTO>>(
                                allSales.OrderByDescending(s => s.SaleId)
                                        .Take(4)
                                        .ToList()),
                StaffUsers = _mapper.Map<List<UserDTO>>(staffUsers)
            };

            return dashboardData;
        }
    }
}