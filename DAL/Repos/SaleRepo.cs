using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF;
using DAL.EF.Table;

namespace DAL.Repos
{
    public class SaleRepo
    {
        private readonly MedicineShopDbContext db;

        public SaleRepo(MedicineShopDbContext db)
        {
            this.db = db;
        }

        public bool Create(Sale obj)
        {
            db.Sales.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            if (ex != null)
            {
                db.Sales.Remove(ex);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public List<Sale> Get()
        {
            return db.Sales.ToList();
        }

        public Sale Get(int id)
        {
            return db.Sales.Find(id);
        }

        public bool Update(Sale obj)
        {
            var ex = Get(obj.SaleId);
            if (ex != null)
            {
                db.Entry(ex).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
