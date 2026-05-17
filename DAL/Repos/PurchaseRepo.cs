using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF;
using DAL.EF.Table;

namespace DAL.Repos
{
    public class PurchaseRepo
    {
        private readonly MedicineShopDbContext db;

        public PurchaseRepo(MedicineShopDbContext db)
        {
            this.db = db;
        }

        public bool Create(Purchase obj)
        {
            db.Purchases.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            if (ex != null)
            {
                db.Purchases.Remove(ex);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public List<Purchase> Get()
        {
            return db.Purchases.ToList();
        }

        public Purchase Get(int id)
        {
            return db.Purchases.Find(id);
        }

        public bool Update(Purchase obj)
        {
            var ex = Get(obj.PurchaseId);
            if (ex != null)
            {
                db.Entry(ex).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
