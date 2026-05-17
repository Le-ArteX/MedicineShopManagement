using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF;
using DAL.EF.Table;

namespace DAL.Repos
{
    public class ReportRepo
    {
        private readonly MedicineShopDbContext db;

        public ReportRepo(MedicineShopDbContext db)
        {
            this.db = db;
        }

        public decimal GetTotalSalesRevenue()
        {
            return db.Sales.Sum(s => (decimal?)s.TotalAmount) ?? 0;
        }

        public int GetTotalSalesCount()
        {
            return db.Sales.Count();
        }

        public decimal GetTotalPurchaseCost()
        {
            return db.Purchases.Sum(p => (decimal?)p.TotalAmount) ?? 0;
        }

        public int GetTotalPurchaseCount()
        {
            return db.Purchases.Count();
        }

        public int GetTotalMedicinesCount()
        {
            return db.Medicines.Count();
        }

        public List<Medicine> GetLowStockMedicines()
        {
            return db.Medicines.Where(m => m.StockQuantity <= m.MinStockLevel).ToList();
        }
    }
}
